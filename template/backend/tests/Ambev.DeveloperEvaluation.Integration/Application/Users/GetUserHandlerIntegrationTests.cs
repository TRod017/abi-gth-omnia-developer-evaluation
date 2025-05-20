using Ambev.DeveloperEvaluation.Application.Common.Behaviors;
using Ambev.DeveloperEvaluation.Application.Users.GetUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Application.Users;

/// <summary>
/// Integration tests for the <see cref="GetUserHandler"/> class.
/// Uses EF Core InMemory database and FluentValidation pipeline behavior to verify correct retrieval behavior.
/// </summary>
public class GetUserHandlerIntegrationTests
{
    private readonly IServiceProvider _provider;
    private readonly IMediator _mediator;
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes the integration test environment with dependencies including EF Core, MediatR,
    /// validators, pipeline behavior, and repositories.
    /// </summary>
    public GetUserHandlerIntegrationTests()
    {
        var services = new ServiceCollection();

        services.AddDbContext<DefaultContext>(options =>
            options.UseInMemoryDatabase("GetUserTestDb"));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetUserHandler>());
        services.AddAutoMapper(typeof(GetUserHandler).Assembly);
        services.AddValidatorsFromAssemblyContaining<GetUserValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILogger<GetUserHandler>>(_ => Substitute.For<ILogger<GetUserHandler>>());

        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
        _context = _provider.GetRequiredService<DefaultContext>();
    }

    /// <summary>
    /// Tests that a valid <see cref="GetUserCommand"/> returns the correct user from the database.
    /// </summary>
    [Fact(DisplayName = "Should return user when ID is valid")]
    public async Task Handle_ValidId_ShouldReturnUser()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "get-user",
            Email = "get@user.com",
            Phone = "21999999999",
            Role = UserRole.Admin,
            Status = UserStatus.Active
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var command = new GetUserCommand(user.Id);

        // Act
        var result = await _mediator.Send(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);
        Assert.Equal(user.Username, result.Name);
    }

    /// <summary>
    /// Tests that a non-existent user ID triggers a <see cref="KeyNotFoundException"/>.
    /// </summary>
    [Fact(DisplayName = "Should throw KeyNotFoundException when user not found")]
    public async Task Handle_InvalidId_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var command = new GetUserCommand(Guid.NewGuid());

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _mediator.Send(command, CancellationToken.None));
    }
}
