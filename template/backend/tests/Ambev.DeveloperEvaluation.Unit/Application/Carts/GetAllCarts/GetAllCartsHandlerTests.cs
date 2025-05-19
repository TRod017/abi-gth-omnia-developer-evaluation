using AutoMapper;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;
using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.Common;
using Ambev.DeveloperEvaluation.Unit.Application.TestData.Cart;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts.GetAllCarts;

/// <summary>
/// Unit tests for the <see cref="GetAllCartsHandler"/> class.
/// Validates behavior when handling <see cref="GetAllCartsCommand"/> requests including pagination, mapping, and logging.
/// Ensures that the handler correctly interacts with the repository, maps data, and handles exceptions.
/// </summary>
public class GetAllCartsHandlerTests
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<GetAllCartsHandler> _logger;
    private readonly GetAllCartsHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetAllCartsHandlerTests"/> class.
    /// Sets up mocks for repository, mapper, and logger, and initializes the handler instance.
    /// </summary>
    public GetAllCartsHandlerTests()
    {
        _repository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _logger = Substitute.For<ILogger<GetAllCartsHandler>>();
        _handler = new GetAllCartsHandler(_repository, _mapper, _logger);
    }

    /// <summary>
    /// Tests that given a valid command, the handler returns a paginated and mapped result.
    /// </summary>
    [Fact(DisplayName = "Given valid command When handling Then should return paginated mapped result", Skip = "TODO: ajustar depois")]
    public async Task Handle_ValidCommand_ReturnsPaginatedMappedResult()
    {
        // Arrange
        var command = new GetAllCartsCommand { Page = 1, Size = 2 };
        var cartsList = CartHandlerTestData.GenerateCarts(2);
        var carts = cartsList.AsQueryable();

        _repository.Query().Returns(carts);

        var paginatedCarts = await PaginatedList<Cart>.CreateAsync(carts, command.Page, command.Size, CancellationToken.None);

        var mappedResults = paginatedCarts.Select(c => new GetAllCartsResult
        {
            Id = c.Id,
            UserId = c.UserId,
            Status = c.Status,
            CreatedAt = c.CreatedAt,
            UpdatedAt = c.UpdatedAt
        }).ToList();

        _mapper.Map<List<GetAllCartsResult>>(paginatedCarts).Returns(mappedResults);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.TotalCount.Should().Be(paginatedCarts.TotalCount);
        result.Should().HaveCount(mappedResults.Count);

        _repository.Received(1).Query();
        _mapper.Received(1).Map<List<GetAllCartsResult>>(paginatedCarts);
    }

    /// <summary>
    /// Tests that when the repository throws an exception,
    /// the handler logs the error and rethrows the exception.
    /// </summary>
    [Fact(DisplayName = "When repository throws exception Then handler logs and rethrows", Skip = "TODO: ajustar depois")]
    public async Task Handle_RepositoryThrows_LogsAndThrows()
    {
        // Arrange
        var command = new GetAllCartsCommand { Page = 1, Size = 10 };
        var exception = new Exception("Database failure");
        _repository.Query().Returns(x => throw exception);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<Exception>().WithMessage("Database failure");
        _logger.Received().Log(
            LogLevel.Error,
            Arg.Any<EventId>(),
            Arg.Any<object>(),
            exception,
            Arg.Any<Func<object, Exception?, string>>());
    }
}
