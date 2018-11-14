using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore.Internal;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public override string Message { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            var failureString = failures
                .Select(e => $"- {e.ErrorMessage}")
                .Join("\r\n");
            Message = ExceptionMessages.ValidationExceptionMessage +  "\r\n" + failureString;
        }
    }
}