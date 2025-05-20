using Ambev.DeveloperEvaluation.Application.Common.Behaviors;
using Ambev.DeveloperEvaluation.Application.Users.DeleteUser;
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
/// Integration tests for the <see cref="DeleteUserHandler"/> class.
/// Uses EF Core InMemory database and FluentValidation pipeline behavior to test
/// full execution flow, including persistence and error handling.
/// </summary>
public class DeleteUserHandlerIntegrationTests
{
    private readonly IServiceProvider _provider;
    private readonly IMediator _mediator;
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes test dependencies including DbContext, MediatR, validators,
    /// pipeline behavior, repository, and logging infrastructure.
    /// </summary>
    public DeleteUserHandlerIntegrationTests()
    {
        var services = new ServiceCollection();

        services.AddDbContext<DefaultContext>(options =>
            options.UseInMemoryDatabase("DeleteUserTestDb"));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<DeleteUserHandler>());
        services.AddAutoMapper(typeof(DeleteUserHandler).Assembly);
        services.AddValidatorsFromAssemblyContaining<DeleteUserValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILogger<DeleteUserHandler>>(_ => Substitute.For<ILogger<DeleteUserHandler>>());

        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
        _context = _provider.GetRequiredService<DefaultContext>();
    }

    /// <summary>
    /// Tests that a valid <see cref="DeleteUserCommand"/> deletes the user from the database
    /// and returns a successful result.
    /// </summary>
    [Fact(DisplayName = "Should delete existing user successfully")]
    public async Task Handle_ExistingUser_ShouldReturnSuccess()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "delete-me",
            Email = "delete@user.com",
            Phone = "21999999999",
            Role = UserRole.Customer,
            Status = UserStatus.Active
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var command = new DeleteUserCommand(user.Id);

        // Act
        var result = await _mediator.Send(command);

        // Assert
        Assert.True(result.Success);
        var deletedUser = await _context.Users.FindAsync(user.Id);
        Assert.Null(deletedUser);
    }

    /// <summary>
    /// Tests that when a non-existent user ID is provided,
    /// the handler throws a <see cref="KeyNotFoundException"/>.
    /// </summary>
    [Fact(DisplayName = "Should throw KeyNotFoundException when user does not exist")]
    public async Task Handle_NonexistentUser_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var nonexistentId = Guid.NewGuid();
        var command = new DeleteUserCommand(nonexistentId);

        // Act & Assert
        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _mediator.Send(command, CancellationToken.None));
    }
}
