using System.Text.RegularExpressions;
using HtmlToPdfSharp.Entities;

namespace HtmlToPdfSharp
{
    /// <summary>
    /// Extensions for different types
    /// </summary>
    internal static class UnitTypeExtensions
    {
        /// <summary>
        /// Getting measure units short name
        /// </summary>
        /// <param name="unitsType">Unit type</param>
        /// <returns>Units shortName</returns>
        internal static string GetShortName(this UnitsType unitsType)
        {
            switch (unitsType)
            {
                case UnitsType.Centimeters:
                    return "cm";
                case UnitsType.Millimeters:
                    return "mm";
                case UnitsType.Inches:
                    return "in";
                default:
                    return "";
            }
        }

        /// <summary>
        /// Detecting whether string is an HTML content or not
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>Whether string is an HTML content or not</returns>
        internal static bool IsHtml(this string value)
        {
            return Regex.IsMatch(value, @"<!doctype html>|(<html\b[^>]*>|<body\b[^>]*>|<x-[^>]+>)+");
        }
    }
}
