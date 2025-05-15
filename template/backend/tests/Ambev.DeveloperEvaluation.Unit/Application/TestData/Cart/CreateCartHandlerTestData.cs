using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;

public static class CreateCartHandlerTestData
{
    private static readonly Faker<CreateCartItemCommand> itemFaker = new Faker<CreateCartItemCommand>()
        .RuleFor(i => i.ProductId, f => Guid.NewGuid())
        .RuleFor(i => i.Quantity, f => f.Random.Number(1, 5));

    private static readonly Faker<CreateCartCommand> faker = new Faker<CreateCartCommand>()
        .RuleFor(c => c.UserId, f => Guid.NewGuid())
        .RuleFor(c => c.Items, f => itemFaker.Generate(3));

    public static CreateCartCommand GenerateValidCommand() => faker.Generate();
}
