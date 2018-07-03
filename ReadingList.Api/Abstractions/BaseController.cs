using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Domain.Queries;
using ReadingList.Resources;

namespace ReadingList.Api.Abstractions
{
    public class BaseController : Controller
    {
        private readonly IMediator _mediator;

        private void ValidateMediator()
        {
            if (_mediator == null)
                throw new InvalidOperationException(ExceptionMessages.MediatorNotInitialized);
        }

        protected BaseController()
        {
            _mediator = MediatorContainer.GetMediator();
        }

        protected async Task ExecuteAsync(ICommand command)
        {
            ValidateMediator();
            await _mediator.Send(command);
        }

        protected async Task<TResult> AskAsync<TResult>(IQuery<TResult> query)
        {
            ValidateMediator();
            return await _mediator.Send(query);
        }
        
        protected void Execute(ICommand command)
        {
            ValidateMediator();
            _mediator.Send(command).RunSync();
        }

        protected TResult Ask<TResult>(IQuery<TResult> query)
        {
            ValidateMediator();
            return _mediator.Send(query).RunSync();
        }
    }
}