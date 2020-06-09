using HtmlToPdfSharp.Entities;

namespace HtmlToPdfSharp
{
    public static class UnitTypeExtensions
    {
        public static string GetShortName(this UnitsType unitsType)
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
