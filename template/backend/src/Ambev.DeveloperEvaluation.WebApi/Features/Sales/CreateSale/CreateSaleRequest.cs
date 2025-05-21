public class CreateSaleRequest
{
    /// <summary>
    /// Gets or sets the unique identifier of the cart that originated this Sale.
    /// </summary>
    public Guid CartId { get; set; }

    /// <summary>
    /// Gets or sets the name of the branch (filial) where the sale occurred.
    /// </summary>
    public string Branch { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets a value indicating whether the sale has been cancelled.
    /// </summary>
    public bool IsCancelled { get; set; } = false;
}
