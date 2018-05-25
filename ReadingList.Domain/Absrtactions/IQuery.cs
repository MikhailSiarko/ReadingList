﻿using MediatR;
using ReadingList.Domain.Implementations;

namespace ReadingList.Domain.Absrtactions
{
    public interface IQuery<TResult> : IRequest<QueryResult<TResult>>
    { 
    }
}