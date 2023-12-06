namespace HtmlToPdfSharp.Entities.Settings
{
    /// <summary>
    /// Header and footer specific settings
    /// </summary>
    public class HeaderFooterSettings
    {
        /// <summary>
        /// The font size to use for the header, e.g. "13"
        /// </summary>
        [Settings("header.fontSize")]
        public int? FontSize { get; set; }

        /// <summary>
        /// The name of the font to use for the header.e.g. "times"
        /// </summary>
        [Settings("header.fontName")]
        public string FontName { get; set; }

        /// <summary>
        /// The string to print in the left part of the header, note that some sequences are replaced in this string, see the wkhtmltopdf manual.
        /// </summary>
        [Settings("header.left")]
        public string LeftText { get; set; }

        /// <summary>
        /// The text to print in the center part of the header
        /// </summary>
        [Settings("header.center")]
        public string CenterText { get; set; }

        /// <summary>
        /// The text to print in the right part of the header
        /// </summary>
        [Settings("header.right")]
        public string Right { get; set; }

        /// <summary>
        /// Whether a line should be printed under the header
        /// </summary>
        [Settings("header.line")]
        public bool? Line { get; set; }

        /// <summary>
        /// The amount of space to put between the header and the content, e.g. "1.8".
        /// Be aware that if this is too large the header will be printed outside the pdf document.
        /// This can be corrected with the margin.top setting.
        /// </summary>
        [Settings("header.spacing")]
        public float? Spacing { get; set; }

        /// <summary>
        /// Url for a HTML document to use for the header
        /// </summary>
        [Settings("header.htmlUrl")]
        public string HtmlUrl { get; set; }
    }
}
