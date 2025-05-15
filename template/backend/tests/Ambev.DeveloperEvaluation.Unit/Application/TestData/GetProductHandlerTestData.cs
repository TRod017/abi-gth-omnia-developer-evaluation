using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides methods to generate test data for GetProductHandler tests.
/// </summary>
public static class GetProductHandlerTestData
{
    private static readonly Faker<GetProductCommand> faker = new Faker<GetProductCommand>()
        .RuleFor(p => p.Id, f => f.Random.Guid());

    public static GetProductCommand GenerateValidCommand() => faker.Generate();
}
