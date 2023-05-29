using System;
using Wizzi.Helpers;

namespace Wizzi.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum e)
        {
            return StringEnum.GetStringValue(e);
        }

        public static T GetEnum<T>(this string na, string value)
        {
            return (T)StringEnum.Parse(typeof(T), value);
        }
    }
}
