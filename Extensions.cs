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
    }
}
