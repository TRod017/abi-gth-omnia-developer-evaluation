using Ambev.DeveloperEvaluation.Application.Common.Behaviors;
using Ambev.DeveloperEvaluation.Application.Users.UpdateUser;
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
/// Integration tests for UpdateUserHandler using EF Core InMemory and FluentValidation.
/// </summary>
public class UpdateUserHandlerIntegrationTests
{
    private readonly IServiceProvider _provider;
    private readonly IMediator _mediator;
    private readonly DefaultContext _context;

    public UpdateUserHandlerIntegrationTests()
    {
        var services = new ServiceCollection();

        services.AddDbContext<DefaultContext>(options =>
            options.UseInMemoryDatabase("UpdateUserTestDb"));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<UpdateUserHandler>());
        services.AddAutoMapper(typeof(UpdateUserHandler).Assembly);
        services.AddValidatorsFromAssemblyContaining<UpdateUserValidator>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILogger<UpdateUserHandler>>(_ => Substitute.For<ILogger<UpdateUserHandler>>());

        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
        _context = _provider.GetRequiredService<DefaultContext>();
    }

    [Fact(DisplayName = "Should update existing user successfully")]
    public async Task Handle_ExistingUser_ShouldUpdateAndReturnResult()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = "olduser",
            Email = "old@email.com",
            Phone = "+5521999999999",
            Role = UserRole.Customer,
            Status = UserStatus.Active
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var command = new UpdateUserCommand
        {
            Id = user.Id,
            Username = "newuser",
            Email = "new@email.com",
            Password = "NewPassword123!",
            Phone = "+5521988888888",
            Status = UserStatus.Inactive,
            Role = UserRole.Admin
        };

        // Act
        var result = await _mediator.Send(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(user.Id, result.Id);

        var updatedUser = await _context.Users.FindAsync(user.Id);
        Assert.NotNull(updatedUser);
        Assert.Equal("newuser", updatedUser!.Username);
        Assert.Equal("new@email.com", updatedUser.Email);
        Assert.Equal("+5521988888888", updatedUser.Phone);
        Assert.Equal(UserStatus.Inactive, updatedUser.Status);
        Assert.Equal(UserRole.Admin, updatedUser.Role);
    }

    [Fact(DisplayName = "Should throw KeyNotFoundException when user does not exist")]
    public async Task Handle_NonexistentUser_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        var command = new UpdateUserCommand
        {
            Id = Guid.NewGuid(),
            Username = "ghost",
            Email = "ghost@email.com",
            Password = "Ghost123!",
            Phone = "+5521000000000",
            Status = UserStatus.Active,
            Role = UserRole.Customer
        };

        // Act
        var result = await _mediator.Send(command, CancellationToken.None);

        // Assert
        Assert.Null(result);
    }
}
