using Ambev.DeveloperEvaluation.Application.Common.Behaviors;
using Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;
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
/// Integration tests for <see cref="GetAllUsersHandler"/> using EF Core InMemory
/// and FluentValidation pipeline.
/// </summary>
public class GetAllUsersHandlerIntegrationTests
{
    private readonly IServiceProvider _provider;
    private readonly IMediator _mediator;
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes the test class with service registrations and dependencies.
    /// </summary>
    public GetAllUsersHandlerIntegrationTests()
    {
        var services = new ServiceCollection();

        services.AddDbContext<DefaultContext>(options =>
            options.UseInMemoryDatabase("GetAllUsersTestDb"));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<GetAllUsersHandler>());
        services.AddAutoMapper(typeof(GetAllUsersHandler).Assembly);
        services.AddValidatorsFromAssemblyContaining<GetAllUsersHandler>();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehavior<,>));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ILogger<GetAllUsersHandler>>(_ => Substitute.For<ILogger<GetAllUsersHandler>>());

        _provider = services.BuildServiceProvider();
        _mediator = _provider.GetRequiredService<IMediator>();
        _context = _provider.GetRequiredService<DefaultContext>();
    }

    /// <summary>
    /// Ensures the handler returns a paginated list when users exist in the database.
    /// </summary>
    [Fact(DisplayName = "Should return paginated users when users exist")]
    public async Task Handle_WithExistingUsers_ShouldReturnPaginatedList()
    {
        // Arrange
        _context.Users.AddRange(new[]
        {
            new User { Id = Guid.NewGuid(), Username = "user1", Email = "user1@test.com", Role = UserRole.Customer, Status = UserStatus.Active },
            new User { Id = Guid.NewGuid(), Username = "user2", Email = "user2@test.com", Role = UserRole.Admin, Status = UserStatus.Active }
        });
        await _context.SaveChangesAsync();

        var command = new GetAllUsersCommand { Page = 1, Size = 10 };

        // Act
        var result = await _mediator.Send(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.TotalCount);
        Assert.Equal(1, result.CurrentPage);
        Assert.Equal(10, result.PageSize);
        Assert.Equal(2, result.Count);
    }

    /// <summary>
    /// Ensures the handler returns an empty list when no users exist.
    /// </summary>
    [Fact(DisplayName = "Should return empty paginated list when no users exist")]
    public async Task Handle_WithNoUsers_ShouldReturnEmptyList()
    {
        _context.Users.RemoveRange(_context.Users);
        await _context.SaveChangesAsync();

        // Arrange
        var command = new GetAllUsersCommand { Page = 1, Size = 10 };

        // Act
        var result = await _mediator.Send(command);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(0, result.TotalCount);
        Assert.Empty(result);
    }
}
