using Demo.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace Demo.Infrastructure.Extensions
{
    public static class SelectListExtension
    {
        public static SelectList ToSelectList(this object sources, object selectedValue = null)
        {
            string dataText = string.Empty, dataValue = string.Empty;
            var sourceToEnumerable = SourceToEnumerable(sources, ref dataText, ref dataValue);

            if (sourceToEnumerable != null)
            {
                return new SelectList(sourceToEnumerable, dataValue, dataText, selectedValue);
            }

            var items = new List<string>();

            if (selectedValue != null)
            {
                items.Add(selectedValue.ToString());
            }

            return new SelectList(items, selectedValue);
        }

        public static SelectList ToSelectList(this object sources, string dataValueField, string dataTextField, object selectedValue = null)
        {
            string dataText = string.Empty, dataValue = string.Empty;
            var sourceToEnumerable = SourceToEnumerable(sources, ref dataText, ref dataValue);

            if (sourceToEnumerable != null)
            {
                return new SelectList(sourceToEnumerable, dataValueField, dataTextField, selectedValue);
            }

            var items = new List<string>();

            if (selectedValue != null)
            {
                items.Add(selectedValue.ToString());
            }

            return new SelectList(items, selectedValue);
        }
        
        private static IEnumerable SourceToEnumerable(object sources, ref string dataTextField, ref  string dataValueField)
        {
            if (sources != null)
            {
                dataTextField = "Name"; dataValueField = "Id";

                if (sources is List<Reference>)
                {
                    return sources as List<Reference>;
                }
                
                //if (sources is List<TextValue>)
                //{
                //    dataTextField = "Text"; dataValueField = "Value";
                //    return sources as List<TextValue>;
                //}

                if (sources is List<string>)
                {
                    dataTextField = ""; dataValueField = "";
                    return sources as List<string>;
                }

                if (sources is List<int>)
                {
                    dataTextField = "";
                    dataValueField = "";
                    return sources as List<int>;
                }

                if (sources is List<decimal>)
                {
                    dataTextField = "";
                    dataValueField = "";
                    return sources as List<decimal>;
                }

                if (sources is Dictionary<string, string>)
                {
                    dataTextField = "Value"; dataValueField = "Key";
                    return sources as Dictionary<string, string>;
                }
			}

            return null;
        }
    }
}