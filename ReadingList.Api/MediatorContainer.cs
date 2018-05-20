using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ReadingList.Api
{
    public static class MediatorContainer
    {
        private static IMediator _mediator;

        public static void InitializeMediator(IServiceProvider provider)
        {
            _mediator = provider.GetService<IMediator>();
        }

        public static IMediator GetMediator() => _mediator;
    }
}