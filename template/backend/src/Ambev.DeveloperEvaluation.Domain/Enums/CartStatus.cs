namespace Ambev.DeveloperEvaluation.Domain.Enums;

/// <summary>
/// Represents the current status of a shopping cart.
/// </summary>
public enum CartStatus
{
    /// <summary>
    /// Undefined status — should not be used.
    /// </summary>
    Unknown = 0,

    /// <summary>
    /// Cart is open and can be modified.
    /// </summary>
    Open = 1,

    /// <summary>
    /// Cart has been confirmed (e.g., ready for checkout).
    /// </summary>
    Confirmed = 2
}
