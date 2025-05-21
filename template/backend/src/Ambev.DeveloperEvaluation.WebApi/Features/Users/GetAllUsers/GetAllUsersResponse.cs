namespace Ambev.DeveloperEvaluation.WebApi.Features.Users.GetAllUsers;

/// <summary>
/// Represents the response model returned by the API when retrieving all users.
/// </summary>
/// <remarks>
/// Contains summarized information for each user, including identification,
/// email, username, phone, status, and role.
/// </remarks>
public class GetAllUsersResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Gets or sets the email address of the user.
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the username.
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the phone number.
    /// </summary>
    public string Phone { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the current status of the user.
    /// </summary>
    public string Status { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the role of the user in the system.
    /// </summary>
    public string Role { get; set; } = string.Empty;
}
