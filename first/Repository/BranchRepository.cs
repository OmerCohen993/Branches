using first.View_Models;
using Umbraco.Cms.Core.IO;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Strings;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace first.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly IUmbracoContextFactory _umbrcoContextFactory;
        private readonly IContentService _contentService;
        private readonly IPublishedSnapshotAccessor _publishedSnapshotAccessor;

        public BranchRepository(IUmbracoContextFactory umbrcoContextFactory, IContentService contentService,
        IPublishedSnapshotAccessor publishedSnapshotAccessor)
        {
            _umbrcoContextFactory = umbrcoContextFactory;
            _contentService = contentService;
            _publishedSnapshotAccessor = publishedSnapshotAccessor;
        }

        public List<Branch> GetBranches()
        {
            var branches = GetBranceshRootPage();

            var branchesResult = new List<Branch>();

            if (branches is Branches branchRoot)
            {
                branchesResult = branchRoot.Children<Branch>()?.ToList() ?? new List<Branch>();
            }

            return branchesResult;
        }
        public Branch GetBranch(int Id)
        {
            using (var cref = _umbrcoContextFactory.EnsureUmbracoContext())
            {
                var branch = cref.UmbracoContext.Content;
                return (Branch)branch.GetById(Id);
            }
        }
        public Branch CreateBranch(BranchCreateItem branch)
        {

            var brannchRoot = GetBranceshRootPage();

            var branchContent = _contentService.Create(branch.BranchName, brannchRoot.Key, Branch.ModelTypeAlias);

            var branchNameAlias = Branch.GetModelPropertyType(_publishedSnapshotAccessor, x => x.BranchName).Alias;
            var branchAddressAlias = Branch.GetModelPropertyType(_publishedSnapshotAccessor, x => x.BranchAddress).Alias;


            branchContent.SetValue(branchNameAlias, branch.BranchName);
            branchContent.SetValue(branchAddressAlias, branch.BranchAddress);


            _contentService.SaveAndPublish(branchContent);

            return GetBranch(branchContent.Id);
        }
        public Branch UpdateBranch(int Id, BranchUpdateItem branch)
        {
            var branchContent = _contentService.GetById(Id);

            var branchNameAlias = Branch.GetModelPropertyType(_publishedSnapshotAccessor, x => x.BranchName).Alias;
            var branchAddressAlias = Branch.GetModelPropertyType(_publishedSnapshotAccessor, x => x.BranchAddress).Alias;

            if (!string.IsNullOrEmpty(branch.BranchName))
                branchContent.SetValue(branchNameAlias, branch.BranchName);

            if (!string.IsNullOrEmpty(branch.BranchAddress))
                branchContent.SetValue(branchAddressAlias, branch.BranchAddress);

            _contentService.SaveAndPublish(branchContent);

            return GetBranch(Id);
        }
        public bool DeleteBranch(int Id)
        {
            var branch = _contentService.GetById(Id);

            if (branch != null)
            {
                var result = _contentService.Delete(branch);
                return result.Success;
            }

            return false;
        }
        private Branches? GetBranceshRootPage()
        {
            using var cref = _umbrcoContextFactory.EnsureUmbracoContext();

            var rootNode = cref?.UmbracoContext?.Content?.GetAtRoot().
            FirstOrDefault(X => X.ContentType.Alias == Company.ModelTypeAlias);

            return rootNode?.Descendant<Branches>();
        }

    }
}