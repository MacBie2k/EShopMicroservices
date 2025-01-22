namespace Catalog.API.Features.Products.CreateProduct;

public record CreateProductCommand(
    string Name,
    List<string> Categories,
    string Description,
    decimal Price,
    string ImageFile
) : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required").MaximumLength(50).WithMessage("Name must not exceed 50 characters");
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price is required and must be greater than 0");
        RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
    }
}

internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        
        var product = new Product()
        {
            Name = command.Name,
            Description = command.Description,
            Price = command.Price,
            Categories = command.Categories,
            ImageFile = command.ImageFile,
        };
        
        session.Store(product);
        await session.SaveChangesAsync(cancellationToken);
        
        
        return new CreateProductResult(product.Id);
    }
}