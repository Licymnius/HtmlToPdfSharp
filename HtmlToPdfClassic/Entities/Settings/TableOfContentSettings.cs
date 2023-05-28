namespace HtmlToPdfSharp.Entities.Settings
{
    public class TableOfContentSettings
    {
        /// <summary>
        /// Should we use a dotted line when creating a table of content
        /// </summary>
        [Settings("toc.useDottedLines")]
        public bool? UseDottedLines { get; set; }

        /// <summary>
        /// The caption to use when creating a table of content
        /// </summary>
        [Settings("toc.captionText")]
        public string CaptionText { get; set; }

        /// <summary>
        /// Should we create links from the table of content into the actual content
        /// </summary>
        [Settings("toc.forwardLinks")]
        public bool? ForwardLinks { get; set; }

        /// <summary>
        /// Should we link back from the content to this table of content
        /// </summary>
        [Settings("toc.backLinks")]
        public bool? BackLinks { get; set; }

        /// <summary>
        /// The indentation used for every table of content level, e.g. "2em".
        /// </summary>
        [Settings("toc.indentation")]
        public int? Indentation { get; set; }

        /// <summary>
        /// How much should we scale down the font for every toc level? E.g. "0.8"
        /// </summary>
        [Settings("toc.fontScale")]
        public float? FontScale { get; set; }
    }
}
