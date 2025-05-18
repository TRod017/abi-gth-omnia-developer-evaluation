using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts
{
    /// <summary>
    /// AutoMapper profile for mapping between application layer models and Web API models
    /// in the GetAllProducts use case.
    /// </summary>
    /// <remarks>
    /// Defines mappings between:
    /// - <see cref="GetAllProductsResult"/> and <see cref="GetAllProductsResponse"/>
    /// - <see cref="GetAllProductsRequest"/> and <see cref="GetAllProductsCommand"/>
    /// </remarks>
    public class GetAllProductsProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GetAllProductsProfile"/> class
        /// and configures mappings for the GetAllProducts operation.
        /// </summary>
        public GetAllProductsProfile()
        {
            // Application → WebApi
            CreateMap<GetAllProductsResult, GetAllProductsResponse>();

            // WebApi → Application
            CreateMap<GetAllProductsRequest, GetAllProductsCommand>();
        }
    }
}
