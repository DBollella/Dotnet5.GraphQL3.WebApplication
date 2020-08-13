using System;
using Dotnet5.GraphQL.Store.Domain.Entities.Products;
using Dotnet5.GraphQL.Store.Domain.Entities.Reviews;
using Dotnet5.GraphQL.Store.Services;
using Dotnet5.GraphQL.Store.WebAPI.GraphQL.Types.Reviews;
using GraphQL.DataLoader;
using GraphQL.Types;

namespace Dotnet5.GraphQL.Store.WebAPI.GraphQL.Types.Products.Kayaks
{
    public class KayakGraphType : ObjectGraphType<Kayak>
    {
        public KayakGraphType(IReviewService reviewService, IDataLoaderContextAccessor dataLoaderContextAccessor)
        {
            Name = "Kayak";

            Field(x => x.KayakType, type: typeof(KayakTypeEnumGraphType));
            Field(x => x.AmountOfPerson);

            Field(x => x.Id, type: typeof(GuidGraphType));
            Field(x => x.Description);
            Field(x => x.IntroduceAt);
            Field(x => x.Name).Description("The name of the product.");
            Field(x => x.PhotoUrl);
            Field(x => x.Price);
            Field(x => x.ProductType, type: typeof(ProductTypeGraphType));
            Field(x => x.Rating);
            Field(x => x.Stock);
            Field<ProductOptionEnumGraphType>("Option");

            FieldAsync<ListGraphType<ReviewGraphType>>("reviews",
                resolve: async context
                    => await dataLoaderContextAccessor.Context
                        .GetOrAddCollectionBatchLoader<Guid, Review>("GetLookupByProductIdsAsync", reviewService.GetLookupByProductIdsAsync)
                        .LoadAsync(context.Source.Id));

            Interface<ProductInterfaceGraphType>();
            IsTypeOf = o => o is Product;
        }
    }
}