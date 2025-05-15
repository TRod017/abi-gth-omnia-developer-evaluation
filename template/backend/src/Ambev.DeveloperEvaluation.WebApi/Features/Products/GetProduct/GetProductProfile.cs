using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;

/// <summary>
/// Profile for GetProduct API ↔ Application mappings.
/// </summary>
public class GetProductProfile : Profile
{
    public GetProductProfile()
    {
        CreateMap<GetProductResult, GetProductResponse>();
    }
}
