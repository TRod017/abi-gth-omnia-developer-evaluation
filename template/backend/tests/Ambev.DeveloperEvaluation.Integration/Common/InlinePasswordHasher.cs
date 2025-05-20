using Ambev.DeveloperEvaluation.Common.Security;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Ambev.DeveloperEvaluation.Integration.Common;

/// <summary>
/// Inline implementation of <see cref="IPasswordHasher"/> using ASP.NET Core Identity's PasswordHasher.
/// </summary>
/// <remarks>
/// Used in integration tests to simulate password hashing and verification 
/// without relying on a real identity system or external dependencies.
/// </remarks>
public class InlinePasswordHasher : IPasswordHasher
{
    private readonly PasswordHasher<User> _inner;

    public InlinePasswordHasher()
    {
        _inner = new PasswordHasher<User>();
    }

    public string HashPassword(string password)
    {
        return _inner.HashPassword(null!, password);
    }

    public bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        return _inner.VerifyHashedPassword(null!, hashedPassword, providedPassword)
            == PasswordVerificationResult.Success;
    }
}
