namespace HtmlToPdfSharp.Entities.Settings
{
    public class WebSettings
    {
        /// <summary>
        /// Should we print the background
        /// </summary>
        [Settings("web.background")]
        public bool? Background { get; set; }

        /// <summary>
        /// Should we load images
        /// </summary>
        [Settings("web.loadImages")]
        public bool? LoadImages { get; set; }

        /// <summary>
        /// Should we enable javascript
        /// </summary>
        [Settings("web.enableJavascript")]
        public bool? EnableJavascript { get; set; }

        /// <summary>
        /// Should we enable intelligent shrinking to fit more content on one page. Has no effect for wkhtmltoimage
        /// </summary>
        [Settings("web.enableIntelligentShrinking")]
        public bool? EnableIntelligentShrinking { get; set; }

        /// <summary>
        /// The minimum font size allowed.E.g. "9"
        /// </summary>
        [Settings("web.minimumFontSize")]
        public int? MinimumFontSize { get; set; }

        /// <summary>
        /// Should the content be printed using the print media type instead of the screen media type. Has no effect for wkhtmltoimage.
        /// </summary>
        [Settings("web.printMediaType")]
        public bool? PrintMediaType { get; set; }

        /// <summary>
        /// What encoding should we guess content is using if they do not specify it properly E.g. "utf-8"
        /// </summary>
        [Settings("web.defaultEncoding")]
        public string DefaultEncoding { get; set; }

        /// <summary>
        /// Url or path to a user specified style sheet.
        /// </summary>
        [Settings("web.userStyleSheet")]
        public string UserStyleSheet { get; set; }

        /// <summary>
        /// Should we enable NS plugins. Enabling this will have limited success.
        /// </summary>
        [Settings("web.enablePlugins")]
        public bool? EnablePlugins { get; set; }
    }
}
