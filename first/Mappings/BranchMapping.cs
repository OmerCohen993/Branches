using first.View_Models;
using Umbraco.Cms.Core.Mapping;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace first.Mappings
{
    public class BranchMapping : IMapDefinition
    {
        public void DefineMaps(IUmbracoMapper mapper)
        {
            mapper.Define<Branch, BranchApiResponseItem>((source, context) => new BranchApiResponseItem(), Map);
        }

        private void Map(Branch source, BranchApiResponseItem target, MapperContext mapperContext)
        {
            target.Id = source.Id;
            target.BranchAddress = source?.BranchAddress ?? string.Empty;
            target.BranchName = source?.BranchName ?? string.Empty;
        }
    }
}