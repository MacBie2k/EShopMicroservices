namespace Catalog.API.Features.Products.DeleteProduct;

public record DeleteProductResponse(bool IsSuccess);

public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {  
                var command = new DeleteProductCommand(id);
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
            .WithName("DeleteProduct")
            .WithSummary("Deletes product")
            .WithDescription("Deletes product")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .Produces(StatusCodes.Status400BadRequest);
    }
}