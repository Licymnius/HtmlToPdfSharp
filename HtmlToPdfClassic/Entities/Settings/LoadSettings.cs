namespace HtmlToPdfSharp.Entities.Settings
{
    public enum LoadErrorHandling { Abort, Skip, Ignore }

    /// <summary>
    /// Page specific settings related to loading content
    /// </summary>
    public class LoadSettings
    {
        /// <summary>
        /// The user name to use when logging into a website, E.g. "bart"
        /// </summary>
        [Settings("load.username")]
        public string UserName { get; set; }

        /// <summary>
        /// The password to used when logging into a website, E.g. "elbarto"
        /// </summary>
        [Settings("load.password")]
        public string Password { get; set; }

        /// <summary>
        /// The mount of time in milliseconds to wait after a page has done loading until it is actually printed.E.g. "1200"
        /// We will wait this amount of time or until, javascript calls window.print()
        /// </summary>
        [Settings("load.jsdelay")]
        public int? JsDelay { get; set; }

        /// <summary>
        /// How much should we zoom in on the content? E.g. "2.2"
        /// </summary>
        [Settings("load.zoomFactor")]
        public float? ZoomFactor { get; set; }

        /// <summary>
        /// Should the custom headers be sent all elements loaded instead of only the main page
        /// </summary>
        [Settings("load.repertCustomHeaders")]
        public float? RepertCustomHeaders { get; set; }

        /// <summary>
        /// Disallow local and piped files to access other local files
        /// </summary>
        [Settings("load.blockLocalFileAccess")]
        public bool? BlockLocalFileAccess { get; set; }

        /// <summary>
        /// Stop slow running javascript
        /// </summary>
        [Settings("load.stopSlowScript")]
        public bool? StopSlowScript { get; set; }

        /// <summary>
        /// Forward javascript warnings and errors to the warning callback
        /// </summary>
        [Settings("load.debugJavascript")]
        public bool? DebugJavascript { get; set; }

        /// <summary>
        /// How should we handle objects that fail to load
        /// </summary>
        [Settings("load.loadErrorHandling")]
        public LoadErrorHandling? LoadErrorHandling { get; set; }

        /// <summary>
        /// String describing what proxy to use when loading the object
        /// </summary>
        [Settings("load.proxy")]
        public string Proxy { get; set; }

        /// <summary>
        /// Path of file used to load and store cookies.
        /// </summary>
        [Settings("load.cookieJar")]
        public string CookieJar { get; set; }
    }
}
