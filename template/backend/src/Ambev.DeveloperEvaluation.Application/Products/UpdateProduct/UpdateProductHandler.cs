using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Handler for processing UpdateProductCommand requests.
/// </summary>
public class UpdateProductHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the UpdateProductHandler class.
    /// </summary>
    /// <param name="productRepository">The product repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the UpdateProductCommand request.
    /// </summary>
    /// <param name="command">The command containing the updated product data.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The updated product details.</returns>
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdateProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var existingProduct = await _productRepository.GetByIdAsync(command.Id, cancellationToken);
        if (existingProduct == null)
            throw new KeyNotFoundException($"Product with ID {command.Id} was not found.");

        var updatedProduct = _mapper.Map<Product>(command);

        var result = await _productRepository.UpdateAsync(updatedProduct, cancellationToken);

        return _mapper.Map<UpdateProductResult>(result);
    }
}
