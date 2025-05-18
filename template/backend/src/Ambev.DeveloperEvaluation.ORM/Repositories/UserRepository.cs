using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.ORM.Repositories;

/// <summary>
/// Implementation of <see cref="IUserRepository"/> using Entity Framework Core.
/// </summary>
public class UserRepository : IUserRepository
{
    private readonly DefaultContext _context;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public UserRepository(DefaultContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Creates a new user in the database.
    /// </summary>
    /// <param name="user">The user entity to create.</param>
    /// <param name="cancellationToken">Token to cancel the async operation.</param>
    /// <returns>The created user entity.</returns>
    public async Task<User> CreateAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return user;
    }

    /// <summary>
    /// Retrieves a user by their unique identifier.
    /// </summary>
    /// <param name="id">Unique identifier of the user.</param>
    /// <param name="cancellationToken">Token to cancel the async operation.</param>
    /// <returns>The user entity if found; otherwise, null.</returns>
    public async Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
    }

    /// <summary>
    /// Retrieves a user by their email address.
    /// </summary>
    /// <param name="email">Email address to search for.</param>
    /// <param name="cancellationToken">Token to cancel the async operation.</param>
    /// <returns>The user entity if found; otherwise, null.</returns>
    public async Task<User?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    /// <summary>
    /// Retrieves all users from the database.
    /// </summary>
    /// <param name="cancellationToken">Token to cancel the async operation.</param>
    /// <returns>A read-only collection of all users.</returns>
    public async Task<IReadOnlyCollection<User>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    /// <summary>
    /// Updates an existing user in the database.
    /// </summary>
    /// <param name="user">The user entity with updated information.</param>
    /// <param name="cancellationToken">Token to cancel the async operation.</param>
    /// <returns>The updated user entity.</returns>
    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken = default)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id, cancellationToken);

        if (existingUser == null)
            throw new KeyNotFoundException($"User with ID {user.Id} not found.");

        existingUser.Email = user.Email;
        existingUser.Username = user.Username;
        existingUser.Password = user.Password;
        existingUser.Phone = user.Phone;
        existingUser.Status = user.Status;
        existingUser.Role = user.Role;

        _context.Users.Update(existingUser);
        await _context.SaveChangesAsync(cancellationToken);

        return existingUser;
    }

    /// <summary>
    /// Deletes a user from the database.
    /// </summary>
    /// <param name="id">Unique identifier of the user to delete.</param>
    /// <param name="cancellationToken">Token to cancel the async operation.</param>
    /// <returns>True if the user was deleted; false if not found.</returns>
    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var user = await GetByIdAsync(id, cancellationToken);
        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync(cancellationToken);
        return true;
    }

    /// <summary>
    /// Returns an <see cref="IQueryable{User}"/> to allow querying users with filters, sorting, and pagination.
    /// </summary>
    /// <returns>An <see cref="IQueryable{User}"/> representing the user collection.</returns>
    public IQueryable<User> Query()
    {
        return _context.Users.AsNoTracking();
    }
}
