using System;
using FluentValidation;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Api.Extensions
{
    public static class FluentValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> NotEmptyWithMessage<T, TProperty>(
            this IRuleBuilder<T, TProperty> builder, Func<T, string> propertyNameProvider)
        {
            return builder.NotEmpty().WithMessage(arg => ValidationMessages.CannotBeEmpty.F(propertyNameProvider(arg)));
        }
    }
}
