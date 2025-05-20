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

        // Mapping from CartItem to SaleItem
        CreateMap<CartItem, Ambev.DeveloperEvaluation.Domain.Entities.SaleItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.SaleId, opt => opt.Ignore())
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.TotalWithDiscount, opt => opt.MapFrom(src => src.TotalWithDiscount))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

        // Mapping from Cart to Sale
        CreateMap<Cart, Sale>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.CartId, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.TotalWithDiscount, opt => opt.MapFrom(src => src.TotalWithDiscount))
            .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(_ => false))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

        // Mapping from SaleItem to CreateSaleItemResult
        CreateMap<Ambev.DeveloperEvaluation.Domain.Entities.SaleItem, CreateSaleItemResult>()
            .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
            .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.Discount, opt => opt.MapFrom(src => src.Discount))
            .ForMember(dest => dest.TotalWithDiscount, opt => opt.MapFrom(src => src.TotalWithDiscount));

        // Mapping from Sale to CreateSaleResult
        CreateMap<Sale, CreateSaleResult>()
            .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
            .ForMember(dest => dest.TotalWithDiscount, opt => opt.MapFrom(src => src.TotalWithDiscount))
            .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(src => src.IsCancelled))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));
    }
}
