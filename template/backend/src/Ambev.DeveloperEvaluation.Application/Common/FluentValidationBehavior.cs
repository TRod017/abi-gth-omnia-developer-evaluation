using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Common.Behaviors;

/// <summary>
/// Pipeline behavior for integrating FluentValidation into MediatR.
/// Automatically validates incoming requests using the registered validator, if available.
/// </summary>
/// <typeparam name="TRequest">The request type.</typeparam>
/// <typeparam name="TResponse">The response type.</typeparam>
public class FluentValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IValidator<TRequest>? _validator;

    /// <summary>
    /// Initializes a new instance of the <see cref="FluentValidationBehavior{TRequest, TResponse}"/> class.
    /// </summary>
    /// <param name="validator">An optional FluentValidation validator for the request.</param>
    public FluentValidationBehavior(IValidator<TRequest>? validator = null)
    {
        _validator = validator;
    }

    /// <summary>
    /// Handles the validation of the request, throwing a ValidationException if invalid.
    /// </summary>
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validator != null)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
        }

        return await next();
    }
}
