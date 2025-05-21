using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;

/// <summary>
/// Provides methods to generate test data for GetCartHandler tests.
/// </summary>
public static class GetCartHandlerTestData
{
    private static readonly Faker<GetCartCommand> faker = new Faker<GetCartCommand>()
        .RuleFor(q => q.Id, f => f.Random.Guid());

    public static GetCartCommand GenerateValidQuery() => faker.Generate();
}
