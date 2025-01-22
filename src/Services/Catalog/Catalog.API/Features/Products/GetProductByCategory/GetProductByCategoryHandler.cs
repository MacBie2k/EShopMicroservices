namespace Catalog.API.Features.Products.GetProductByCategory;

public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;

public record GetProductByCategoryResult(IEnumerable<Product> Products);

internal class GetProductByCategoryQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
{
    public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var products = await session.Query<Product>().Where(x => x.Categories.Contains(query.Category)).ToListAsync(cancellationToken);
            return new GetProductByCategoryResult(products);
        }
        catch (Exception e)
        {
            return new GetProductByCategoryResult(new List<Product>());
        }
    }
}