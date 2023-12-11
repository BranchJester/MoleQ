using System;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace MoleQ.Extensions;

public static class EnumExtensions
{
    public static string ToPrettyString(this Enum enumValue)
    {
        // Regular expression to split at uppercase letters and numbers
        var pattern = @"(?<!^)(?=[A-Z0-9])";

        // Getting the name of the enum and applying the regex split
        var parts = Regex.Split(enumValue.ToString(), pattern);

        // Returning the parts as a single string, joined by a space
        return string.Join(" ", parts);
    }

    public static string ToEnumStyle(this string prettyString)
    {
        // Removing all spaces from the string
        return prettyString.Replace(" ", "");
    }


    public static string GetDescription(this Enum value)
    {
        var fieldInfo = value.GetType().GetField(value.ToString());
        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }
}