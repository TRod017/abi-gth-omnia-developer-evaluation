using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Application.Carts.CreateCart.CartItem;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;

/// <summary>
/// Provides test data generation for CreateCartCommand and CreateCartItemCommand,
/// usado especialmente em testes de validação.
/// </summary>
public static class CreateCartCommandTestData
{
    private static readonly Faker<CreateCartItemCommand> itemFaker = new Faker<CreateCartItemCommand>()
        .RuleFor(i => i.ProductId, f => f.Random.Guid())
        .RuleFor(i => i.Quantity, f => f.Random.Int(1, 10))
        .RuleFor(i => i.UnitPrice, f => f.Random.Decimal(1, 1000));

    private static readonly Faker<CreateCartCommand> commandFaker = new Faker<CreateCartCommand>()
        .RuleFor(c => c.UserId, f => f.Random.Guid())
        .RuleFor(c => c.Status, f => Ambev.DeveloperEvaluation.Domain.Enums.CartStatus.Open)
        .RuleFor(c => c.Items, f => itemFaker.Generate(3));

    /// <summary>
    /// Gera um comando válido para uso nos testes de validação.
    /// </summary>
    public static CreateCartCommand GenerateValidCommand() => commandFaker.Generate();

    /// <summary>
    /// Gera um comando inválido com UserId vazio para teste.
    /// </summary>
    public static CreateCartCommand GenerateInvalidUserIdCommand()
    {
        var cmd = GenerateValidCommand();
        cmd.UserId = default;
        return cmd;
    }

    /// <summary>
    /// Gera um comando inválido com lista de itens vazia.
    /// </summary>
    public static CreateCartCommand GenerateEmptyItemsCommand()
    {
        var cmd = GenerateValidCommand();
        cmd.Items = new List<CreateCartItemCommand>();
        return cmd;
    }

    /// <summary>
    /// Gera um comando inválido com lista de itens nula.
    /// </summary>
    public static CreateCartCommand GenerateNullItemsCommand()
    {
        var cmd = GenerateValidCommand();
        cmd.Items = null!;
        return cmd;
    }

    /// <summary>
    /// Gera um comando com itens inválidos para teste.
    /// </summary>
    public static CreateCartCommand GenerateCommandWithInvalidItems()
    {
        var cmd = GenerateValidCommand();
        cmd.Items = new List<CreateCartItemCommand>
        {
            new CreateCartItemCommand { ProductId = default, Quantity = 0, UnitPrice = -10m }
        };
        return cmd;
    }
}
