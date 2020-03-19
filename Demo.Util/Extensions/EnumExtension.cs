using System;
using System.ComponentModel;
using System.Reflection;

namespace Demo.Util
{
    public static class EnumExtension
    {

        public static string GetDescription(this object obj)
        {
            try
            {
                var fieldInfo = obj.GetType().GetField(obj.ToString());

                var attr = fieldInfo.GetCustomAttribute<DescriptionAttribute>();

                var description = !string.IsNullOrWhiteSpace(attr?.Description)
                    ? attr.Description
                    : obj.ToString();

                return description;
            }
            catch (NullReferenceException)
            {
                return string.Empty;
            }
        }

        public static string GetEnumDescription(this Enum obj)
        {
            try
            {
                var field = obj.GetType().GetField(obj.ToString());
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();

                return !string.IsNullOrWhiteSpace(attribute?.Description)
                    ? attribute.Description
                    : obj.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}