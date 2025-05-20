using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

namespace Ambev.DeveloperEvaluation.WebApi.Mappings.Carts;

/// <summary>
/// AutoMapper profile for mapping between application layer models and Web API models
/// in the GetAllCarts use case.
/// </summary>
/// <remarks>
/// Defines mappings between:
/// - <see cref="GetAllCartsResult"/> and <see cref="GetAllCartsResponse"/>
/// - <see cref="GetAllCartsRequest"/> and <see cref="GetAllCartsCommand"/>
/// </remarks>
public class GetAllSalesProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllSalesProfile"/> class
    /// and configures mappings for the GetAllCarts operation.
    /// </summary>
    public GetAllSalesProfile()
    {
        // Application → WebApi
        CreateMap<GetAllCartsResult, GetAllCartsResponse>();

        // WebApi → Application
        CreateMap<GetAllCartsRequest, GetAllCartsCommand>();
    }
}
