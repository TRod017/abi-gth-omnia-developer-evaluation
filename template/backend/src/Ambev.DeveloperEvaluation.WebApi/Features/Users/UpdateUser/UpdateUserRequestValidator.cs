using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Validator for <see cref="UpdateUserRequest"/> that defines validation rules for incoming API payloads.
/// </summary>
/// <remarks>
/// Ensures that:
/// - <c>Id</c> is not empty
/// - <c>Email</c> is not empty and has a valid format
/// - <c>Username</c> and <c>Password</c> are not empty
/// - <c>Phone</c> follows the international format
/// - <c>Status</c> and <c>Role</c> are not empty
/// </remarks>
public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateUserRequestValidator"/> class
    /// and sets up validation rules for user updates via the API.
    /// </summary>
    public UpdateUserRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("A valid email must be provided.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.")
            .MinimumLength(3).WithMessage("Username must be at least 3 characters long.")
            .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters.");

        RuleFor(x => x.Phone)
            .Matches(@"^\+[1-9]\d{10,14}$")
            .WithMessage("Phone number must be in international format starting with '+' and 11 to 15 digits.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.");

        RuleFor(x => x.Role)
            .NotEmpty().WithMessage("Role is required.");
    }
}
