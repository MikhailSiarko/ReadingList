using System;
using System.Linq.Expressions;

namespace ReadingList.Domain.Infrastructure.Filters
{
    public static class FilterExpressionBuilder
    {
        public static FilterExpression<T> And<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.ToEasyPredicate().And(second);
        }

        public static FilterExpression<T> Or<T>(this Expression<Func<T, bool>> first, Expression<Func<T, bool>> second)
        {
            return first.ToEasyPredicate().Or(second);
        }

        public static FilterExpression<T> Not<T>(this Expression<Func<T, bool>> expression)
        {
            return expression.ToEasyPredicate().Not();
        }

        public static FilterExpression<T> ToEasyPredicate<T>(this Expression<Func<T, bool>> expression)
        {
            return FilterExpression<T>.Create(expression);
        }
    }
}
