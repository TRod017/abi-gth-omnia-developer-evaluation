using Ambev.DeveloperEvaluation.Application.Products.DeleteProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Product;

/// <summary>
/// Provides test data for DeleteProductHandler unit tests.
/// </summary>
public static class DeleteProductHandlerTestData
{
    private static readonly Faker<DeleteProductCommand> faker = new Faker<DeleteProductCommand>()
        .RuleFor(p => p.Id, f => f.Random.Guid());

    public static DeleteProductCommand GenerateValidCommand() => faker.Generate();
}
