namespace Ambev.DeveloperEvaluation.Domain.Specifications;

public interface IAsyncSpecification<T>
{
    Task<bool> IsSatisfiedByAsync(T entity, CancellationToken cancellationToken = default);
}
