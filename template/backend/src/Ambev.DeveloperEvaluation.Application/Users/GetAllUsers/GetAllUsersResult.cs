namespace Ambev.DeveloperEvaluation.Application.Users.GetAllUsers;

/// <summary>
/// Represents the response model returned for each user in the GetAllUsers operation.
/// </summary>
/// <remarks>
/// This DTO is used to expose selected user fields in user listing endpoints,
/// including identification, email, username, phone, status, and role.
/// </remarks>
public class GetAllUsersResult
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
