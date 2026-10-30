using C1.Maui.Core;

namespace FlexViewer.Resources
{
    /// <summary>
    /// Strongly-typed accessor for the localized strings in Resources.resx.
    /// </summary>
    public class Resources : ResourceBase<Resources>
    {
        private static System.Globalization.CultureInfo _resourceCulture;

        /// <summary>
        /// Overrides the current thread's CurrentUICulture property for all
        /// resource lookups using this strongly typed resource class.
        /// </summary>
        public static System.Globalization.CultureInfo Culture
        {
            get => _resourceCulture;
            set => _resourceCulture = value;
        }

        public static string GettingStartedTitle => ResourceManager.GetString("GettingStartedTitle", _resourceCulture);
        public static string PdfViewerTitle => ResourceManager.GetString("PdfViewerTitle", _resourceCulture);
        public static string FlexReportViewerTitle => ResourceManager.GetString("FlexReportViewerTitle", _resourceCulture);
        public static string SsrsViewerTitle => ResourceManager.GetString("SsrsViewerTitle", _resourceCulture);

        public static string WelcomeHeading => ResourceManager.GetString("WelcomeHeading", _resourceCulture);
        public static string WelcomeDescription => ResourceManager.GetString("WelcomeDescription", _resourceCulture);
        public static string ExploreSectionTitle => ResourceManager.GetString("ExploreSectionTitle", _resourceCulture);
        public static string PdfViewerHint => ResourceManager.GetString("PdfViewerHint", _resourceCulture);

        public static string PdfFormatLabel => ResourceManager.GetString("PdfFormatLabel", _resourceCulture);
        public static string FlexReportSectionTitle => ResourceManager.GetString("FlexReportSectionTitle", _resourceCulture);
        public static string ChooseReportHint => ResourceManager.GetString("ChooseReportHint", _resourceCulture);
        public static string SelectReportLabel => ResourceManager.GetString("SelectReportLabel", _resourceCulture);
        public static string ReportServiceSectionTitle => ResourceManager.GetString("ReportServiceSectionTitle", _resourceCulture);
        public static string SsrsRemoteReportHint => ResourceManager.GetString("SsrsRemoteReportHint", _resourceCulture);

        public static string LoadingText => ResourceManager.GetString("LoadingText", _resourceCulture);
        public static string LoadingPdfText => ResourceManager.GetString("LoadingPdfText", _resourceCulture);
        public static string LoadingReportText => ResourceManager.GetString("LoadingReportText", _resourceCulture);
        public static string LoadingReportServiceText => ResourceManager.GetString("LoadingReportServiceText", _resourceCulture);

        public static string PdfLoadErrorTitle => ResourceManager.GetString("PdfLoadErrorTitle", _resourceCulture);
        public static string ReportLoadErrorTitle => ResourceManager.GetString("ReportLoadErrorTitle", _resourceCulture);
        public static string ReportServiceErrorTitle => ResourceManager.GetString("ReportServiceErrorTitle", _resourceCulture);
        public static string OkButton => ResourceManager.GetString("OkButton", _resourceCulture);
    }
}
