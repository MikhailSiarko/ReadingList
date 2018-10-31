using System;
using System.Linq;
using System.Linq.Expressions;

namespace ReadingList.Application.Infrastructure.Filters
{
    public class FilterExpression<TExpressionFuncType>
    {
        #region Static boolean expressions

        public static FilterExpression<TExpressionFuncType> True = Create(param => true);

        public static FilterExpression<TExpressionFuncType> False = Create(param => false);

        #endregion
        
        private readonly Expression<Func<TExpressionFuncType, bool>> _internalExpression;

        public FilterExpression(Expression<Func<TExpressionFuncType, bool>> expression)
        {
            _internalExpression = expression;
        }

        public static FilterExpression<TExpressionFuncType> Create(Expression<Func<TExpressionFuncType, bool>> expression)
        {
            return new FilterExpression<TExpressionFuncType>(expression);
        }

        public Expression<Func<TExpressionFuncType, bool>> ToExpressionFunc()
        {
            return _internalExpression;
        }

        #region And

        public FilterExpression<TExpressionFuncType> And(Expression<Func<TExpressionFuncType, bool>> expression)
        {
            return Create(Compose(expression, Expression.AndAlso));
        }

        public FilterExpression<TExpressionFuncType> And(FilterExpression<TExpressionFuncType> expression)
        {
            return And(expression._internalExpression);
        }

        #endregion

        #region Or

        public FilterExpression<TExpressionFuncType> Or(Expression<Func<TExpressionFuncType, bool>> expression)
        {
            return Create(Compose(expression, Expression.OrElse));
        }

        public FilterExpression<TExpressionFuncType> Or(FilterExpression<TExpressionFuncType> expression)
        {
            return Or(expression._internalExpression);
        }

        #endregion

        public FilterExpression<TExpressionFuncType> Not()
        {
            var negated = Expression.Not(_internalExpression.Body);
            return Create(Expression.Lambda<Func<TExpressionFuncType, bool>>(negated, _internalExpression.Parameters));
        }

        #region Implicit conversion to and from Expression<Func<TExpressionFuncType, bool>>
        
        public static implicit operator FilterExpression<TExpressionFuncType>(Expression<Func<TExpressionFuncType, bool>> expression)
        {
            return Create(expression);
        }

        public static implicit operator Expression<Func<TExpressionFuncType, bool>>(FilterExpression<TExpressionFuncType> expression)
        {
            return expression._internalExpression;
        }

        #endregion

        #region Operator Overloads

        public static FilterExpression<TExpressionFuncType> operator !(FilterExpression<TExpressionFuncType> expression)
        {
            return expression.Not();
        }

        public static FilterExpression<TExpressionFuncType> operator &(FilterExpression<TExpressionFuncType> first, FilterExpression<TExpressionFuncType> second)
        {
            return first.And(second);
        }

        public static FilterExpression<TExpressionFuncType> operator |(FilterExpression<TExpressionFuncType> first, FilterExpression<TExpressionFuncType> second)
        {
            return first.Or(second);
        }

        //Both should return false so that short-circuiting operators (logical operators || and &&) will work as expected
        public static bool operator true(FilterExpression<TExpressionFuncType> first) { return false; }
        public static bool operator false(FilterExpression<TExpressionFuncType> first) { return false; }

        #endregion

        private Expression<T> Compose<T>(Expression<T> second, Func<Expression, Expression, Expression> merge)
        {
            var map = _internalExpression.Parameters
                .Select((f, i) => new { f, s = second.Parameters[i] })
                .ToDictionary(p => p.s, p => p.f);

            var secondBody = ParameterRebinder.ReplaceParameters(map, second.Body);

            return Expression.Lambda<T>(merge(_internalExpression.Body, secondBody), _internalExpression.Parameters);
        }
    }
}
