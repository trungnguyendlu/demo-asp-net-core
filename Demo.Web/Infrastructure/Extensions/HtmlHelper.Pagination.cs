using Demo.Infrastructure.Utils;
using Demo.Model;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Linq.Expressions;

namespace Demo.Infrastructure.Extensions
{
    public static class HtmlHelperPagination
	{
        public static IHtmlContent PaginationFor<TModel, TValue>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            var model = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider).Model as PaginationModel;
            if (model != null)
            {
	            if (model.ShowPaging)
	            {
					model.CalculatePaging();    
	            }
                model.PaginationId = htmlHelper.NameFor(expression);
				
                return htmlHelper.EditorFor(expression, "_Pagination");
            }

            return new HtmlString(string.Empty);
        }

        public static IHtmlContent FePaginationFor<TModel, TValue>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            var model = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider).Model as PaginationModel;
            if (model != null)
            {
                if (model.ShowPaging)
                {
                    model.CalculatePaging();
                }
                model.PaginationId = htmlHelper.NameFor(expression);

                return htmlHelper.EditorFor(expression, $"_{Common.WebSetting.ThemeId}_FePagination");
            }

            return new HtmlString(string.Empty);
        }

        public static IHtmlContent BlogPaginationFor<TModel, TValue>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            var model = ExpressionMetadataProvider.FromLambdaExpression(expression, htmlHelper.ViewData, htmlHelper.MetadataProvider).Model as PaginationModel;
            if (model != null)
            {
                if (model.ShowPaging)
                {
                    model.CalculatePaging();
                }
                model.PaginationId = htmlHelper.NameFor(expression);

                return htmlHelper.EditorFor(expression, "_BlogPagination");
            }

            return new HtmlString(string.Empty);
        }
    }
}