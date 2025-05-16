namespace Ambev.DeveloperEvaluation.Application.Users.UpdateUser;

/// <summary>
/// Represents the response model returned after successfully updating a user.
/// </summary>
/// <remarks>
/// This DTO contains the unique identifier of the updated user, typically used
/// to confirm the update operation and reference the user in subsequent actions.
/// </remarks>
public class UpdateUserResult
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated user.
    /// </summary>
    public Guid Id { get; set; }
}
