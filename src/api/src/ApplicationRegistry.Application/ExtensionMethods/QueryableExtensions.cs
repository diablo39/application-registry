using ApplicationRegistry.CQRS.Abstraction;
using System;
using System.Linq.Expressions;

namespace System.Linq
{
    static class QueryableExtensions
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> query, string orderByExpression, bool? desc)
        {
            if (string.IsNullOrEmpty(orderByExpression))
                return query;

            string propertyName, orderByMethod;
            string[] strs = orderByExpression.Split(' ');
            propertyName = strs[0];

            if (desc ?? false)
                orderByMethod = "OrderByDescending";
            else
                orderByMethod = "OrderBy";

            ParameterExpression pe = Expression.Parameter(query.ElementType);
            MemberExpression me = Expression.Property(pe, propertyName);

            MethodCallExpression orderByCall = Expression.Call(typeof(Queryable), orderByMethod, new Type[] { query.ElementType, me.Type }, query.Expression
                , Expression.Quote(Expression.Lambda(me, pe)));

            return query.Provider.CreateQuery(orderByCall) as IQueryable<T>;
        }

        public  static IQueryable<T> SortAndPage<T,K>(this IQueryable<T> dbQuery, K query)
            where K: ListQueryParameters
        {
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                dbQuery = dbQuery.OrderBy(query.SortBy, query.SortDesc);
            }

            if (query.ItemsPerPage > 0)
                dbQuery = dbQuery.Skip(query.ItemsPerPage * (query.Page - 1)).Take(query.ItemsPerPage);
            return dbQuery;
        }
    }
}
