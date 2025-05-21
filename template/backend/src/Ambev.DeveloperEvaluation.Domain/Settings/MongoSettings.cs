namespace Ambev.DeveloperEvaluation.Common.Settings;

/// <summary>
/// Represents the MongoDB connection settings configured in appsettings.
/// </summary>
public class MongoSettings
{
    /// <summary>
    /// Gets or sets the MongoDB connection string.
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the name of the MongoDB database.
    /// </summary>
    public string Database { get; set; } = string.Empty;
}
