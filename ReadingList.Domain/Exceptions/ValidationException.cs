using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using ReadingList.Resources;

namespace ReadingList.Domain.Exceptions
{
    public class ValidationException : ApplicationException
    {
        public override string Message { get; }

        public ValidationException(IEnumerable<ValidationFailure> failures)
        {
            var failureString = string.Join("\r\n", failures
                .Select(e => $"- {e.ErrorMessage}"));
            Message = ExceptionMessages.ValidationExceptionMessage + "\r\n" + failureString;
        }
    }
}