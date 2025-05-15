using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;

/// <summary>
/// Handler for processing DeleteProductCommand requests.
/// </summary>
public class DeleteProductHandler : IRequestHandler<DeleteProductCommand, bool>
{
    private readonly IProductRepository _productRepository;

    /// <summary>
    /// Initializes a new instance of the DeleteProductHandler class.
    /// </summary>
    /// <param name="productRepository">The product repository instance.</param>
    public DeleteProductHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    /// <summary>
    /// Handles the DeleteProductCommand request.
    /// </summary>
    /// <param name="command">The command containing the product ID to delete.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>True if deletion was successful; otherwise, false.</returns>
    public async Task<bool> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new DeleteProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return await _productRepository.DeleteAsync(command.Id, cancellationToken);
    }
}
