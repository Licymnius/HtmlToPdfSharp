using System;

namespace HtmlToPdfSharp.Entities
{
    public class HtmlConversionException : Exception
    {
        public HtmlConversionException(string message) : base(message)
        {
        }
    }
}
