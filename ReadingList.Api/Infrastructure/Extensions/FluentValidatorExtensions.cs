using System;
using FluentValidation;
using FluentValidation.Internal;
using ReadingList.Domain.Infrastructure.Extensions;
using ReadingList.Resources;

namespace ReadingList.Api.Infrastructure.Extensions
{
    public static class FluentValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> NotEmptyWithMessage<T, TProperty>(
            this IRuleBuilder<T, TProperty> builder, Func<T, string> propertyNameProvider)
        {
            return builder.NotEmpty().WithMessage(arg => ValidationMessages.CannotBeEmpty.F(propertyNameProvider(arg)));
        }
        
        public static IRuleBuilderOptions<T, TProperty> NotEqualToDefault<T, TProperty>(this IRuleBuilder<T, TProperty> builder) where TProperty : struct
        {
            var defaultValue = default(TProperty);

            return builder.NotEqual(defaultValue).WithMessage(x =>
                ValidationMessages.CannotBeDefault.F(((RuleBuilder<T, TProperty>) builder).Rule.Member.Name.SplitPascalCase(),
                    defaultValue.ToString()));
        }
    }
}
