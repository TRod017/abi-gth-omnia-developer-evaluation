using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale.SaleItem;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// AutoMapper profile for <see cref="CreateSaleCommand"/> and <see cref="CreateSaleResult"/> mappings.
/// </summary>
/// <remarks>
/// This profile defines the mapping rules between the Sale entity used to generate a sale
/// and the domain entity <see cref="Sale"/>, as well as the mapping from the entity to the result model.
/// </remarks>
public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        // Mapping from CreateSaleItemCommand (input DTO) to SaleItem (entity)
        CreateMap<CreateSaleItemCommand, Ambev.DeveloperEvaluation.Domain.Entities.SaleItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SaleId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductName, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Mapping from CreateSaleCommand to Sale (input ? entity)
        CreateMap<CreateSaleCommand, Sale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore());

        // Mapping from SaleItem (entity) to CreateSaleItemResult (output DTO)
        CreateMap<Ambev.DeveloperEvaluation.Domain.Entities.SaleItem, CreateSaleItemResult>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.TotalWithDiscount, opt => opt.MapFrom(src => src.TotalWithDiscount));

        // Mapping from Sale (entity) to CreateSaleResult (output DTO)
        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

    }
}
