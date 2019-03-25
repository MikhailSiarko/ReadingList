using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ReadingList.Domain.Exceptions;

namespace ReadingList.Domain.Infrastructure.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehavior(IServiceProvider provider)
        {
            _validator = provider.GetService<IValidator<TRequest>>();
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var context = new ValidationContext<TRequest>(request);

            if (_validator == null) return next();

            var result = _validator.Validate(context);

            if (!result.IsValid)
            {
                throw new DomainValidationException(result.Errors);
            }
            return next();
        }
    }
}