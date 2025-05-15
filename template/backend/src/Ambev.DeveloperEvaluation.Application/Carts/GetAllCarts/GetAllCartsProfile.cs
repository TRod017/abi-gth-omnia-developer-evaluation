using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

/// <summary>
/// AutoMapper profile for mapping Cart to GetAllCartsResult.
/// </summary>
public class GetAllCartsProfile : Profile
{
    public GetAllCartsProfile()
    {
        CreateMap<Cart, GetAllCartsResult>();
    }
}
