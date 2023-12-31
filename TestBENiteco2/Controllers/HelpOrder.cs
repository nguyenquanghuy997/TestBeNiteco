using System.Linq.Expressions;
using DynamicExpresso;
using TestBENiteco2.Enums;

namespace TestBENiteco2.Controllers;

public static class HelpOrder
{
    public static IOrderedQueryable<T> OrderByWithDynamic<T>(this IQueryable<T> src, string fieldName,
        Expression<Func<T, object>> defaultOrError,
        SortedDirection sortedDirection)
    {
        Func<Expression<Func<T, object>>, SortedDirection?, IOrderedQueryable<T>> getOrdered =
            (expression, direction) => direction is SortedDirection.Ascending
                ? src.OrderBy(expression)
                : src.OrderByDescending(expression);
        if (string.IsNullOrEmpty(fieldName)) return getOrdered.Invoke(defaultOrError, sortedDirection);
        try
        {
            var interpreter = new Interpreter(InterpreterOptions.DefaultCaseInsensitive);
            var expressionStr = $"x.{fieldName}";
            var expressionFilter = interpreter.ParseAsExpression<Func<T, object>>(expressionStr, "x");
            return getOrdered.Invoke(expressionFilter, sortedDirection);
        }
        catch (Exception)
        {
            return getOrdered.Invoke(defaultOrError, sortedDirection);
        }
    }
}