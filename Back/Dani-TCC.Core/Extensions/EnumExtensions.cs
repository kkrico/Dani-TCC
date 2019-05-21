using System.ComponentModel;
using System.Reflection;

namespace Dani_TCC.Core.Extensions
{
    public static class EnumExtensions
    {
        public static string Description<T>(this T source) where T:struct
        {
            FieldInfo fi = source.GetType().GetField(source.ToString());

            var attributes = (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0) return attributes[0].Description;
            else return source.ToString();
        }
    }
}