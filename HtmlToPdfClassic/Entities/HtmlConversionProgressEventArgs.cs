using System;

namespace HtmlToPdfSharp.Entities
{
    public class HtmlConversionProgressEventArgs : EventArgs
    {
        public int Percentage { get; set; }
    }
}
