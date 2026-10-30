using C1.Document;
using System.Net;
using Strings = FlexViewer.Resources.Resources;

namespace FlexViewer
{
    /// <summary>
    /// Sample page that loads a remote SQL Server Reporting Services (SSRS)
    /// report into a <see cref="C1.Maui.Viewer.FlexViewer"/>.
    /// </summary>
    public partial class SsrsViewer : ContentPage
    {
        private bool _isLoading;

        public SsrsViewer()
        {
            InitializeComponent();
            flexViewer.Loaded += OnFlexViewerLoaded;
        }

        private async void OnFlexViewerLoaded(object? sender, EventArgs e)
        {
            flexViewer.Loaded -= OnFlexViewerLoaded;
            await LoadReportServiceDocumentAsync();
        }

        private async Task LoadReportServiceDocumentAsync()
        {
            if (_isLoading) return;
            _isLoading = true;
            loadingOverlay.SetLoading(true, Strings.LoadingReportServiceText);

            try
            {
                // TODO: Move SSRS credentials to environment variables or secure config.
               var source = new C1SSRSDocumentSource
                {
                    Language = new System.Globalization.CultureInfo("en-US"),
                    Credential = new NetworkCredential(
                        Environment.GetEnvironmentVariable("SSRS_DEMO_USERNAME") ?? "ssrs_demo",
                        Environment.GetEnvironmentVariable("SSRS_DEMO_PASSWORD") ?? "bjqveS5gh89BH1Q",
                        ""
                    )
                };

                source.DocumentLocation = new SSRSReportLocation(
                    "http://ssrs.componentone.com:8000/ReportServer",
                    "/AdventureWorks/Employee Sales Summary Detail"
                );

                await source.GenerateAsync();
                flexViewer.DocumentSource = source;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FlexViewer] SSRS load failed: {ex}");
                await DisplayAlert(Strings.ReportServiceErrorTitle, ex.Message, Strings.OkButton);
            }
            finally
            {
                _isLoading = false;
                loadingOverlay.SetLoading(false);
            }
        }
    }
}
