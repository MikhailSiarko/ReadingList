using System;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ReadingList.Domain.Commands;
using ReadingList.Domain.Infrastructure.Helpers;
using ReadingList.Domain.Queries;

namespace ReadingList.Api.Abstractions
{
    public class BaseController : Controller
    {
        private readonly IMediator _mediator;

        protected BaseController()
        {
            _mediator = MediatorContainer.GetMediator();
        }

        protected async Task<CommandResult> ExecuteAsync(ICommand command)
        {
            if (_mediator == null)
                throw new NullReferenceException();
            return await _mediator.Send(command);
        }

        protected async Task<QueryResult<TResult>> AskAsync<TResult>(IQuery<TResult> query)
        {
            if(_mediator == null)
                throw new NullReferenceException();
            return await _mediator.Send(query);
        }
        
        protected CommandResult Execute(ICommand command)
        {
            if (_mediator == null)
                throw new NullReferenceException();
            return AsyncHelpers.RunSync(() => _mediator.Send(command));
        }

        protected QueryResult<TResult> Ask<TResult>(IQuery<TResult> query)
        {
            if(_mediator == null)
                throw new NullReferenceException();
            return AsyncHelpers.RunSync(() => _mediator.Send(query));
        }
    }
}