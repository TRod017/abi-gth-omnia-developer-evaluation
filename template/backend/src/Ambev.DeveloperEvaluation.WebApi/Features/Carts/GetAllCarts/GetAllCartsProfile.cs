using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

/// <summary>
/// AutoMapper profile for mapping between application layer models and Web API response models
/// in the GetAllCarts use case.
/// </summary>
/// <remarks>
/// Defines the mapping from <see cref="GetAllCartsResult"/> to <see cref="GetAllCartsResponse"/>.
/// </remarks>
public class GetAllCartsProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllCartsProfile"/> class
    /// and configures mappings for the GetAllCarts operation.
    /// </summary>
    public GetAllCartsProfile()
    {
        CreateMap<GetAllCartsResult, GetAllCartsResponse>();
    }
}
