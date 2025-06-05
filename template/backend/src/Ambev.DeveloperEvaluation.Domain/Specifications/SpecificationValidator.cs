
namespace Ambev.DeveloperEvaluation.Domain.Specifications;

/// <summary>
/// Helper for validating domain entities using a set of specifications with associated error messages.
/// </summary>
public static class SpecificationValidator
{
    /// <summary>
    /// Validates the given entity against a list of specifications and throws <see cref="DomainException"/> 
    /// if any specification is not satisfied.
    /// </summary>
    /// <typeparam name="T">The type of entity to validate.</typeparam>
    /// <param name="entity">The entity instance to validate.</param>
    /// <param name="rules">
    /// An array of tuples, each containing a specification and its associated error message.
    /// </param>
    /// <exception cref="DomainException">Thrown when a specification is not satisfied.</exception>
    public static void Validate<T>(T entity, params (ISpecification<T> spec, string errorMessage)[] rules)
    {
        foreach (var (spec, errorMessage) in rules)
        {
            if (!spec.IsSatisfiedBy(entity))
                throw new DomainException(errorMessage);
        }
    }
}
