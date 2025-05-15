using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

/// <summary>
/// Handler for processing GetAllProductsCommand requests.
/// </summary>
public class GetAllProductsHandler : IRequestHandler<GetAllProductsCommand, IEnumerable<GetAllProductsResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the GetAllProductsHandler class.
    /// </summary>
    /// <param name="productRepository">The product repository instance.</param>
    /// <param name="mapper">The AutoMapper instance.</param>
    public GetAllProductsHandler(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Handles the GetAllProductsCommand request.
    /// </summary>
    /// <param name="request">The command to retrieve all products.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A collection of all available products.</returns>
    public async Task<IEnumerable<GetAllProductsResult>> Handle(GetAllProductsCommand request, CancellationToken cancellationToken)
    {
        var products = await _productRepository.GetAllAsync(cancellationToken);
        return _mapper.Map<IEnumerable<GetAllProductsResult>>(products);
    }
}
