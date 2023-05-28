namespace HtmlToPdfSharp.Entities.Settings
{
    /// <summary>
    /// Page orientation
    /// </summary>
    public enum PageOrientation { Landscape, Portrait }

    /// <summary>
    /// Color mode of the output file
    /// </summary>
    public enum ColorMode { Color, GrayScale }
    
    public class PdfGlobalSettings
    {
        /// <summary>
        /// Size of the margins
        /// </summary>
        [SubClassSettings]
        public Margins Margins { get; set; } = new Margins();

        /// <summary>
        /// The paper size of the output document, e.g. "A4"
        /// </summary>
        [Settings("size.pageSize")]
        public string PageSize { get; set; }

        /// <summary>
        /// The with of the output document
        /// </summary>
        [Settings("size.width", SpecifyUnitsType = true)]
        public float? Width { get; set; }

        /// <summary>
        /// The height of the output document, e.g. "12in".
        /// </summary>
        [Settings("size.height", SpecifyUnitsType = true)]
        public float? Height { get; set; }

        /// <summary>
        /// The orientation of the output document, must be either "Landscape" or "Portrait"
        /// </summary>
        [Settings("orientation")]
        public PageOrientation? PageOrientation { get; set; }

        /// <summary>
        /// Should the output be printed in color or gray scale, must be either "Color" or "Grayscale"
        /// </summary>
        [Settings("colorMode")]
        public ColorMode? ColorMode { get; set; }

        /// <summary>
        /// Most likely has no effect.
        /// </summary>
        [Settings("resolution")]
        public int? Resolution { get; set; }

        /// <summary>
        /// What dpi should we use when printing, e.g. "80".
        /// </summary>
        [Settings("dpi")]
        public int? Dpi { get; set; }

        /// <summary>
        /// A number that is added to all page numbers when printing headers, footers and table of content.
        /// </summary>
        [Settings("pageOffset")]
        public int? PageOffset { get; set; }

        /// <summary>
        /// How many copies should we print?. e.g. "2"
        /// </summary>
        [Settings("copies")]
        public int? Copies { get; set; }

        /// <summary>
        /// Should the copies be collated
        /// </summary>
        [Settings("collate")]
        public bool? Collate { get; set; }

        /// <summary>
        /// Should a outline (table of content in the sidebar) be generated and put into the PDF
        /// </summary>
        [Settings("outline")]
        public bool? Outline { get; set; }

        /// <summary>
        /// The maximal depth of the outline, e.g. "4"
        /// </summary>
        [Settings("outlineDepth")]
        public int? OutlineDepth { get; set; }

        /// <summary>
        ///  If not set to the empty string a XML representation of the outline is dumped to this file
        /// </summary>
        [Settings("dumpOutline")]
        public string DumpOutline { get; set; }

        /// <summary>
        /// The path of the output file, if "-" output is sent to stdout, if empty the output is stored in a buffer.
        /// </summary>
        [Settings("out")]
        public string OutputFile { get; set; }

        /// <summary>
        /// The title of the PDF document
        /// </summary>
        [Settings("documentTitle")]
        public string DocumentTitle { get; set; }

        /// <summary>
        /// Should we use loss less compression when creating the pdf file? Must be either "true" or "false"
        /// </summary>
        [Settings("useCompression")]
        public bool? UseCompression { get; set; }

        /// <summary>
        /// The maximal DPI to use for images in the pdf document
        /// </summary>
        [Settings("imageDPI")]
        public int? ImageDpi { get; set; }

        /// <summary>
        /// The jpeg compression factor to use when producing the pdf document, e.g. "92"
        /// </summary>
        [Settings("imageQuality")]
        public int? ImageQuality { get; set; }
    }
}
