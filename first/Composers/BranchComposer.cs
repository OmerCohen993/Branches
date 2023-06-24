using first.Mappings;
using first.Repository;
using Umbraco.Cms.Core.Composing;
using Umbraco.Cms.Core.Mapping;

namespace first.Composers
{
    public class BranchComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddScoped<IBranchRepository, BranchRepository>();

            builder.WithCollectionBuilder<MapDefinitionCollectionBuilder>()
            .Add<BranchMapping>();
        }
    }
}