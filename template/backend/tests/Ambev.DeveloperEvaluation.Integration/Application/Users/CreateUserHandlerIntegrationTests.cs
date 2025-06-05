using Ambev.DeveloperEvaluation.Application.Common.Behaviors;
using Ambev.DeveloperEvaluation.Application.Users.CreateUser;
using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Integration.Common;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application.Users;

/// <summary>
/// Integration tests for CreateUserHandler using real EF Core InMemory database
/// and FluentValidation pipeline behavior.
/// </summary>
public class CreateUserHandlerIntegrationTests
{
    private readonly IServiceProvider _provider;
    private readonly IMediator _mediator;

    public CreateUserHandlerIntegrationTests()
    {
        var services = new ServiceCollection();

        // Register EF Core InMemory DbContext
        services.AddDbContext<DefaultContext>(options =>
            options.UseInMemoryDatabase("IntegrationTestDb"));

        // Register MediatR
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateUserHandler>());

        // Register AutoMapper
        services.AddAutoMapper(typeof(CreateUserHandler).Assembly);

        // Register FluentValidation
        services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

        // Register pipeline behavior
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

        // Register dependencies
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped(_ => Substitute.For<ILogger<CreateUserHandler>>());

        // Register shared InlinePasswordHasher
        services.AddScoped<IPasswordHasher, InlinePasswordHasher>();

        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
    }

    [Fact(DisplayName = "Should create user successfully when command is valid")]
    public async Task Handle_ValidCommand_ShouldCreateUser()
    {
        var command = new CreateUserCommand
        {
            Username = "testuser",
            Password = "StrongPassword123!",
            Email = "test@email.com",
            Phone = "+5521999999999",
            Status = UserStatus.Active,
            Role = UserRole.Customer
        };

        var result = await _mediator.Send(command, CancellationToken.None);

        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact(DisplayName = "Should fail validation when command is invalid")]
    public async Task Handle_InvalidCommand_ShouldThrowValidationException()
    {
        var command = new CreateUserCommand(); // Invalid

        await Assert.ThrowsAsync<ValidationException>(() =>
            _mediator.Send(command, CancellationToken.None));
    }
}
