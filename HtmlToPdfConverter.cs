using System;
using System.IO;
using System.Linq;
using System.Reflection;
using HtmlToPdfSharp.Entities;
using HtmlToPdfSharp.Entities.Settings;
using wk_html_to_pdf_wrapper;

namespace HtmlToPdfSharp
{
    /// <summary>
    /// Html to pdf converter
    /// </summary>
    public class HtmlToPdfConverter : IDisposable
    {
        /// <summary>
        /// WkHtmlToPdf wrapper
        /// </summary>
        private readonly wkhtmltopdf_wrapper wkHtmlToPdfWrapper = new wkhtmltopdf_wrapper();

        /// <summary>
        /// Conversion progress percentage changed, integer parameter returns progress percentage
        /// </summary>
        public event EventHandler<int> ProgressChanged;
        
        /// <summary>
        /// Conversion finished, integer parameter returns conversion result
        /// </summary>
        public event EventHandler<int> Finished;

        /// <summary>
        /// Conversion phase changed
        /// </summary>
        public event EventHandler<string> PhaseChanged;

        /// <summary>
        /// Pdf conversion global settings
        /// </summary>
        public PdfGlobalSettings PdfGlobalSettings { get; } = new PdfGlobalSettings();

        /// <summary>
        /// Pdf conversion object settings
        /// </summary>
        public PdfObjectSettings PdfObjectSettings { get; } = new PdfObjectSettings();

        /// <summary> 
        /// Web page specific settings
        /// </summary>
        public WebSettings WebSettings { get; } = new WebSettings();

        /// <summary>
        /// Page specific settings related to loading content
        /// </summary>
        public LoadSettings LoadSettings { get; } = new LoadSettings();

        /// <summary>
        /// Header specific settings
        /// </summary>
        public HeaderFooterSettings HeaderSettings { get; } = new HeaderFooterSettings();

        /// <summary>
        /// Footer specific settings
        /// </summary>
        public HeaderFooterSettings FooterSettings { get; } = new HeaderFooterSettings();
        
        /// <summary>
        /// Warnings occured during last HTML conversion
        /// </summary>
        public string ConversionWarnings { get; set; }

        /// <summary>
        /// Measure units type
        /// </summary>
        public UnitsType UnitsType { get; set; } = UnitsType.Millimeters;

        /// <summary>
        /// Start conversion process
        /// </summary>
        /// <param name="file">Input file path or Url or the content of the file in string</param>
        /// <param name="outputFile">Output file</param>
        public void Convert(string file, string outputFile)
        {
            if (!file.IsHtml())
            {
                PdfObjectSettings.Page = file;
                PerformConvert(outputFile);
                return;
            }

            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".html");
            try
            {
                File.WriteAllText(tempFile, file);
                PdfObjectSettings.Page = tempFile;
                PerformConvert(outputFile);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        /// <summary>
        /// Start conversion process
        /// </summary>
        /// <param name="file">Input file path or Url or the content of the file in string</param>
        /// <param name="stream">Output stream</param>
        public void Convert(string file, Stream stream)
        {
            if (stream == null)
                throw new HtmlConversionException("Output stream cannot be null");
            
            var outputFile = Path.GetTempFileName();
            PdfGlobalSettings.OutputFile = outputFile;
            try
            {
                Convert(file, outputFile);
                using (var outputStream = File.OpenRead(outputFile))
                    outputStream.CopyTo(stream);
            }
            finally
            {
                if (File.Exists(outputFile))
                    File.Delete(outputFile);
            }
        }

        /// <summary>
        /// Start conversion process
        /// </summary>
        /// <param name="inputStream">Html stream</param>
        /// <param name="stream">Output stream</param>
        public void Convert(Stream inputStream, Stream stream)
        {
            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".html");
            try
            {
                using (var fileStream = File.OpenWrite(tempFile))
                {
                    inputStream.Seek(0, SeekOrigin.Begin);
                    inputStream.CopyTo(fileStream);
                }

                Convert(tempFile, stream);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        /// <summary>
        /// Start conversion process
        /// </summary>
        /// <param name="inputStream">Html stream</param>
        /// <param name="outputFile">Output file</param>
        public void Convert(Stream inputStream, string outputFile)
        {
            var tempFile = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".html");
            try
            {
                using (var fileStream = File.OpenWrite(tempFile))
                {
                    inputStream.Seek(0, SeekOrigin.Begin);
                    inputStream.CopyTo(fileStream);
                }

                Convert(tempFile, outputFile);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        /// <summary>
        /// Deinitialization of the wkhtmltopdf, use it only just before your application closing, cause the restriction of the wkhtmltopdf
        /// </summary>
        public static void DeInit()
        {
            wkhtmltopdf_wrapper.deinit_wkhtmltopdf();
        }

        /// <summary>
        /// Inner conversion
        /// </summary>
        /// <param name="outputFile">Output file</param>
        private void PerformConvert(string outputFile)
        {
            ConversionWarnings = null;
            string errorMessage = null;
            var result = 0;
            PdfGlobalSettings.OutputFile = outputFile;
            wkHtmlToPdfWrapper.ProgressEvent += percentage => ProgressChanged?.Invoke(this, percentage);
            wkHtmlToPdfWrapper.PhaseEvent += phaseDescription => PhaseChanged?.Invoke(this, phaseDescription);
            wkHtmlToPdfWrapper.ErrorEvent += errorText => errorMessage = errorText;
            wkHtmlToPdfWrapper.FinishedEvent += resultCode =>
            {
                result = resultCode;
                Finished?.Invoke(this, resultCode);
            };
            wkHtmlToPdfWrapper.WarningEvent += warningsMessage => ConversionWarnings = warningsMessage;

            SetObjectSettings(PdfGlobalSettings, true);
            SetObjectSettings(PdfObjectSettings, false);
            SetObjectSettings(WebSettings, false);
            SetObjectSettings(LoadSettings, false);
            SetObjectSettings(HeaderSettings, false);
            SetObjectSettings(FooterSettings, false);
            wkHtmlToPdfWrapper.convert();

            if (result == 0)
                throw new HtmlConversionException(errorMessage);
        }

        /// <summary>
        /// Fill settings
        /// </summary>
        /// <param name="settings">Settings collection</param>
        /// <param name="globalSettings">Global settings, if not object settings</param>
        private void SetObjectSettings<T>(T settings, bool globalSettings)
        {
            var type = settings.GetType();
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(prop => prop.CanRead))
            {
                var attributes = propertyInfo.GetCustomAttributes().ToArray();
                if (attributes.Any(attr => attr is SubClassSettingsAttribute))
                    SetObjectSettings(propertyInfo.GetValue(settings), globalSettings);

                var attribute = attributes.OfType<SettingsAttribute>().FirstOrDefault();
                if (attribute == null)
                    continue;

                var value = propertyInfo.GetValue(settings);
                if (value == null)
                    continue;

                string textValue;
                switch (value)
                {
                    case bool boolValue:
                        textValue = boolValue ? "true" : "false";
                        break;
                    case Enum enumValue:
                        textValue = enumValue.ToString("G");
                        break;
                    default:
                        textValue = value.ToString();
                        break;
                }

                if (globalSettings)
                    wkHtmlToPdfWrapper.set_global_settings(attribute.Name, $"{textValue}{(attribute.SpecifyUnitsType ? UnitsType.GetShortName() : null)}");
                else
                    wkHtmlToPdfWrapper.set_object_settings(attribute.Name, $"{textValue}{(attribute.SpecifyUnitsType ? UnitsType.GetShortName() : null)}");
            }
        }

        /// <summary>
        /// Disposing converter
        /// </summary>
        public void Dispose()
        {
            wkHtmlToPdfWrapper.Dispose();
        }
    }
}
