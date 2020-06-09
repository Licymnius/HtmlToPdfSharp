using System;
using System.IO;
using System.Linq;
using System.Reflection;
using HtmlToPdfSharp.Entities;
using wk_html_to_pdf_wrapper;

namespace HtmlToPdfSharp
{
    public class HtmlToPdfConverter : IDisposable
    {
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

        public PdfGlobalSettings PdfGlobalSettings { get; } = new PdfGlobalSettings();

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

        public UnitsType UnitsType { get; set; } = UnitsType.Millimeters;
        
        /// <summary>
        /// Html to pdf converter
        /// </summary>
        /// <param name="fileName">The URL or path of the input file</param>
        public HtmlToPdfConverter(string fileName)
        {
            wkHtmlToPdfWrapper.set_object_settings("page", fileName);
        }

        public HtmlToPdfConverter(Stream stream)
        {
        }

        /// <summary>
        /// Start conversion process
        /// </summary>
        /// <param name="outputFile">Output file</param>
        public void Convert(string outputFile)
        {
            ConversionWarnings = null;
            string errorMessage = null;
            var result = 0;
            wkHtmlToPdfWrapper.set_global_settings("out", outputFile);
            wkHtmlToPdfWrapper.ProgressEvent += percentage => ProgressChanged?.Invoke(this, percentage);
            wkHtmlToPdfWrapper.PhaseEvent += phaseDescription => PhaseChanged?.Invoke(this, phaseDescription);
            wkHtmlToPdfWrapper.ErrorEvent += errorText => errorMessage = errorText;
            wkHtmlToPdfWrapper.FinishedEvent += resultCode =>
            {
                result = resultCode;
                Finished?.Invoke(this, resultCode);
            };
            wkHtmlToPdfWrapper.WarningEvent += warningsMessage => ConversionWarnings = warningsMessage;
            PdfGlobalSettings.Margins.Top = 10;

            SetObjectSettings(PdfGlobalSettings);
            SetObjectSettings(PdfObjectSettings);
            SetObjectSettings(WebSettings);
            SetObjectSettings(LoadSettings);
            SetObjectSettings(HeaderSettings);
            SetObjectSettings(FooterSettings);
            wkHtmlToPdfWrapper.convert();

            if (result == 0)
                throw new HtmlConversionException(errorMessage);
        }

        private void SetObjectSettings<T>(T settings)
        {
            var type = settings.GetType();
            foreach (var propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public).Where(prop => prop.CanRead))
            {
                var attributes = propertyInfo.GetCustomAttributes().ToArray();
                if (attributes.Any(attr => attr is SubClassSettingsAttribute))
                    SetObjectSettings(propertyInfo.GetValue(settings));

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

                wkHtmlToPdfWrapper.set_object_settings(attribute.Name, $"{textValue}{(attribute.SpecifyUnitsType ? UnitsType.GetShortName() : null)}");
            }
        }

        public void Dispose()
        {
            wkHtmlToPdfWrapper.Dispose();
        }
    }
}
