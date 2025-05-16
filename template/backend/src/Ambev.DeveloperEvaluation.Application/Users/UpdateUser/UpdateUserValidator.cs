using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Validator for <see cref="UpdateUserCommand"/> that defines validation rules for updating a user.
/// </summary>
/// <remarks>
/// Ensures that all required fields for a user update are present and valid, including:
/// - User ID must not be empty
/// - Email and username must not be empty
/// - Phone must be in valid format
/// - Status and role must not be default/unknown values
/// </remarks>
public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserValidator"/> class
    /// and sets up validation rules for user updates.
    /// </summary>
    public UpdateUserValidator()
    {
        RuleFor(u => u.Id)
            .NotEmpty()
            .WithMessage("User ID must be provided.");

        RuleFor(u => u.Email)
            .NotEmpty()
            .EmailAddress()
            .WithMessage("A valid email must be provided.");

        RuleFor(u => u.Username)
            .NotEmpty()
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

        RuleFor(u => u.Password)
            .NotEmpty()
            .MinimumLength(6)
            .WithMessage("Password must be at least 6 characters long.");

        RuleFor(u => u.Phone)
            .Matches(@"^\+[1-9]\d{10,14}$")
            .WithMessage("Phone number must start with '+' followed by 11 to 15 digits.");

        RuleFor(u => u.Status)
            .IsInEnum()
            .NotEqual(UserStatus.Unknown)
            .WithMessage("User status must be a valid value.");

        RuleFor(u => u.Role)
            .IsInEnum()
            .NotEqual(UserRole.None)
            .WithMessage("User role must be a valid value.");
    }
}
