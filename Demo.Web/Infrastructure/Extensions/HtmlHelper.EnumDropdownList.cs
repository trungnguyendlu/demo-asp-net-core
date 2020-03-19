using Demo.Util;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Demo.Infrastructure.Extensions
{
    public static class HtmlHelperEnumDropdownList
    {
        public static IHtmlContent EnumDropDownFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
        {
            return EnumDropDownFor(htmlHelper, expression, null, null, null, null);
        }

        public static IHtmlContent EnumDropDownFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
        {
            return EnumDropDownFor(htmlHelper, expression, htmlAttributes, null, null, null);
        }
        
        public static IHtmlContent EnumDropDownFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, List<TEnum> enumValuesToExclude)
        {
            return EnumDropDownFor(htmlHelper, expression, null, null, null, enumValuesToExclude);
        }

        public static IHtmlContent EnumDropDownFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, List<TEnum> enumValuesToExclude, string optionLabel, object htmlAttributes)
        {
            return EnumDropDownFor(htmlHelper, expression, htmlAttributes, null, optionLabel, enumValuesToExclude);
        }

        public static IHtmlContent EnumDropDownFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, List<TEnum> enumValuesToInclude, string optionLabel)
        {
            return EnumDropDownFor(htmlHelper, expression, null, enumValuesToInclude, optionLabel, null);
        }

        public static IHtmlContent EnumDropDownFor<TModel, TEnum>(this IHtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes, List<TEnum> enumValuesToInclude, string optionLabel, List<TEnum> enumValueToExclude)
        {
            var enumType = Nullable.GetUnderlyingType(typeof(TEnum)) ?? typeof(TEnum);

            IEnumerable<TEnum> enumValues = Enum.GetValues(enumType).Cast<TEnum>();

            if (enumValueToExclude != null)
            {
                enumValues = enumValues.Except(enumValueToExclude);
            }

            if (enumValuesToInclude != null)
            {
                enumValues = enumValues.Where(enumValuesToInclude.Contains);
            }

            var items = enumValues
                .Select(e => new SelectListItem
                {
                    Text = e.GetDescription(),
                    Value = e.ToString(),
                    Selected = e.Equals(htmlHelper.ViewData.Model)
                }).ToList();

            if (optionLabel != null)
            {
                //items.Insert(0, new SelectListItem { Text = optionLabel });
                //old code
                //new[] { new SelectListItem { Text = optionLabel } }.Concat(items);
            }

            return htmlHelper.DropDownListFor(expression, items, optionLabel, htmlAttributes);
        }
    }
}