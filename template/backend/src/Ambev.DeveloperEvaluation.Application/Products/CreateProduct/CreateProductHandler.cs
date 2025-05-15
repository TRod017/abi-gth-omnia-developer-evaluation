using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Handler for processing CreateProductCommand requests.
/// </summary>
public class CreateProductHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the CreateProductHandler class.
    /// </summary>
    /// <param name="productRepository">The product repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the CreateProductCommand request.
    /// </summary>
    /// <param name="command">The command containing the product details to create.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The created product result.</returns>
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreateProductValidator();
        var validationResult = await validator.ValidateAsync(command, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var product = _mapper.Map<Product>(command);

        var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
        var result = _mapper.Map<CreateProductResult>(createdProduct);
        return result;
    }
}
