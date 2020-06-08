using System;

namespace HtmlToPdfSharp.Entities
{
    public enum UnitsType { None, Centimeters, Millimeters, Inches }

    public class SettingsAttribute : Attribute
    {
        public string Name { get; set; }

        public bool SpecifyUnitsType { get; set; }

        public SettingsAttribute(string name)
        {
            Name = name;
        }
    }
}
