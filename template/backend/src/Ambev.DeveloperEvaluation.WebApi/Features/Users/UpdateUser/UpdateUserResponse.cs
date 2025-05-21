namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Represents the response returned by the API after successfully updating a user.
/// </summary>
/// <remarks>
/// Contains the unique identifier of the updated user, allowing clients
/// to confirm the update or use the ID for subsequent operations.
/// </remarks>
public class UpdateUserResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the updated user.
    /// </summary>
    public Guid Id { get; set; }
}
