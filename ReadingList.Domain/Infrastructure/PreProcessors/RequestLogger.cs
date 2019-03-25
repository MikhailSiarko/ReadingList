using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Internal;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace ReadingList.Domain.Infrastructure.PreProcessors
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger<TRequest> _logger;

        public RequestLogger(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("{Time} - Incoming Request: {Name}", DateTime.Now.ToLocalTime(),
                typeof(TRequest).Name.SplitPascalCase());

            return Task.CompletedTask;
        }
    }
}