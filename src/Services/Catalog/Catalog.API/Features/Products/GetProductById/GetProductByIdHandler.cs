

namespace Catalog.API.Features.Products.GetProductById;

public record GetProductByIdQuery(Guid ProductId) : IQuery<GetProductResult>;
public record GetProductResult(Product Product);

internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductResult>
{
    public async Task<GetProductResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        var product = await session.Query<Product>().Where(x => x.Id == query.ProductId)
            .FirstOrDefaultAsync(cancellationToken);

        if (product == null)
        {
            throw new ProductNotFoundException(query.ProductId);
        }
            
        return new GetProductResult(product);
    }
}