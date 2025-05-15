using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Carts.GetAllCarts;

/// <summary>
/// Profile for mapping between GetAllCarts Application and API.
/// </summary>
public class GetAllCartsProfile : Profile
{
    public GetAllCartsProfile()
    {
        CreateMap<GetAllCartsResult, GetAllCartsResponse>();
    }
}
