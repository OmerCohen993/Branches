using first.View_Models;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace first.Repository
{
    public interface IBranchRepository
    {
        Branch GetBranch(int Id);
        List<Branch> GetBranches();
        Branch CreateBranch(BranchCreateItem branch);
        Branch UpdateBranch(int Id, BranchUpdateItem branch);
        bool DeleteBranch(int Id);
    }
}