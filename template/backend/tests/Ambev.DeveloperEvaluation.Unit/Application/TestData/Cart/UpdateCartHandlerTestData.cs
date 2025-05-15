using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart.CartItems;
using Ambev.DeveloperEvaluation.Application.Carts.UpdateCart;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;
public static class UpdateCartHandlerTestData
{
    private static readonly Faker<UpdateCartItemCommand> itemFaker = new Faker<UpdateCartItemCommand>()
        .RuleFor(i => i.ProductId, f => Guid.NewGuid())
        .RuleFor(i => i.Quantity, f => f.Random.Number(1, 5));

    private static readonly Faker<UpdateCartCommand> faker = new Faker<UpdateCartCommand>()
        .RuleFor(c => c.Id, f => Guid.NewGuid())
        .RuleFor(c => c.UserId, f => Guid.NewGuid())
        .RuleFor(c => c.Items, f => itemFaker.Generate(2));

    public static UpdateCartCommand GenerateValidCommand() => faker.Generate();
}
