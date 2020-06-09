namespace HtmlToPdfSharp.Entities.Settings
{
    public class PdfObjectSettings
    {
        /// <summary>
        /// Table of content settings
        /// </summary>
        [SubClassSettings]
        public TableOfContentSettings TableOfContentSettings { get; } = new TableOfContentSettings();
        
        /// <summary>
        /// The URL or path of the web page to convert, if "-" input is read from stdin
        /// </summary>
        [Settings("page")]
        public string Page { get; set; }

        /// <summary>
        /// Should external links in the HTML document be converted into external pdf links
        /// </summary>
        [Settings("useExternalLinks")]
        public bool? UseExternalLinks { get; set; }

        /// <summary>
        /// Should internal links in the HTML document be converted into pdf references
        /// </summary>
        [Settings("useLocalLinks")]
        public bool? UseLocalLinks { get; set; }
        
        /// <summary>
        /// Should we turn HTML forms into PDF forms
        /// </summary>
        [Settings("produceForms")]
        public bool? ProduceForms { get; set; }

        /// <summary>
        /// Should the sections from this document be included in the outline and table of content
        /// </summary>
        [Settings("includeInOutline")]
        public bool? IncludeInOutline { get; set; }

        /// <summary>
        /// Should we count the pages of this document, in the counter used for TOC, headers and footers
        /// </summary>
        [Settings("pagesCount")]
        public bool? PagesCount { get; set; }

        /// <summary>
        /// If not empty this object is a table of content object, "page" is ignored and this xsl style sheet is used to convert the outline XML into a table of content
        /// </summary>
        [Settings("tocXsl")]
        public string TocXsl { get; set; }
    }
}
