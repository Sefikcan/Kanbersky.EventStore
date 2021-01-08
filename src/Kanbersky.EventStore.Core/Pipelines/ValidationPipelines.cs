using FluentValidation;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Kanbersky.EventStore.Core.Pipelines
{
    public class ValidationPipelines<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidatorFactory _validatorFactory;

        public ValidationPipelines(IValidatorFactory validatorFactory)
        {
            _validatorFactory = validatorFactory;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var validator = _validatorFactory.GetValidator(request.GetType());
            var result = validator?.Validate(new ValidationContext<TRequest>(request));

            if (result != null && !result.IsValid)
            {
                throw new ValidationException(result.Errors); // TODO: Validation exception
            }

            return await next();
        }
    }
}
