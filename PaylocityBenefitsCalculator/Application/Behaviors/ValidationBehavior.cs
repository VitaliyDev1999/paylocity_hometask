using FluentValidation;
using MediatR;

namespace Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    //private readonly IValidator<TRequest> _validator;

    //public ValidationBehavior(IValidator<TRequest> validator)
    //{
    //    _validator = validator;
    //}

    //public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    //{
    //    // Validate the request using FluentValidation or your chosen validation method.
    //    var validationResult = _validator.Validate(request);

    //    if (validationResult.IsValid)
    //    {
    //        return await next();
    //    }

    //    throw new ValidationException(validationResult.Errors);
    //}
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        // Check if a validator exists for the request.
        var validator = _validators.FirstOrDefault(v => v.CanValidateInstancesOfType(request.GetType()));

        if (validator != null)
        {
            // If a validator exists, apply the validation.
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                return await next();
            }

            throw new ValidationException(validationResult.Errors);
        }
        else
        {
            // If no validator exists, skip the validation and proceed to the handler.
            return await next();
        }
    }
}
