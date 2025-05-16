namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.UpdateUser;

/// <summary>
/// Represents the request payload used to update an existing user via the API.
/// </summary>
/// <remarks>
/// Contains the updated user information, including identifier, email,
/// username, password, phone number, status, and role.
/// </remarks>
public class UpdateUserRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the user to be updated.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the updated email address of the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated username of the user.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated password of the user.
    /// </summary>
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated phone number of the user.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated status of the user.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the updated role of the user.
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
