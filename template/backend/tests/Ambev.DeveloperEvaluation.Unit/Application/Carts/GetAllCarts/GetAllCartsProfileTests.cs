using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Profiles;

/// <summary>
/// Unit tests for the <see cref="GetAllCartsProfile"/> AutoMapper profile.
/// Validates that mapping configuration is valid and mapping works as expected.
/// Uses TestData for creating Cart entities consistent with project standards.
/// </summary>
public class GetAllCartsProfileTests
{
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes AutoMapper configuration and asserts it is valid.
    /// </summary>
    public GetAllCartsProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<GetAllCartsProfile>();
        });

        config.AssertConfigurationIsValid();

        _mapper = config.CreateMapper();
    }

    /// <summary>
    /// Tests mapping from Cart entity to GetAllCartsResult DTO.
    /// </summary>
    [Fact(DisplayName = "Should map Cart entity to GetAllCartsResult DTO correctly")]
    public void Cart_To_GetAllCartsResult_Mapping_IsValid()
    {
        var cart = CartTestData.GenerateValidCart();

        var result = _mapper.Map<GetAllCartsResult>(cart);

        Assert.Equal(cart.Id, result.Id);
        Assert.Equal(cart.UserId, result.UserId);
        Assert.Equal(cart.Status, result.Status);
        Assert.Equal(cart.CreatedAt, result.CreatedAt);
        Assert.Equal(cart.UpdatedAt, result.UpdatedAt);
    }
}
