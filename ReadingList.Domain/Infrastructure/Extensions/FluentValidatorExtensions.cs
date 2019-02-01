using System;
using FluentValidation;
using FluentValidation.Internal;
using ReadingList.Resources;

namespace ReadingList.Domain.Infrastructure.Extensions
{
    public static class FluentValidatorExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> NotEmptyOrNullWithMessage<T, TProperty>(
            this IRuleBuilder<T, TProperty> builder, Func<T, string> propertyNameProvider)
        {
            return builder.NotEmpty().NotNull().WithMessage(arg => ValidationMessages.CannotBeEmptyOrNull.F(propertyNameProvider(arg)));
        }

        public static IRuleBuilderOptions<T, TProperty> NotEqualToDefault<T, TProperty>(
            this IRuleBuilder<T, TProperty> builder) where TProperty : struct
        {
            var defaultValue = default(TProperty);

            return builder.NotEqual(defaultValue).WithMessage(x =>
                ValidationMessages.CannotBeDefault.F(
                    ((RuleBuilder<T, TProperty>) builder).Rule.Member.Name.SplitPascalCase(),
                    defaultValue.ToString()));
        }
    }
}