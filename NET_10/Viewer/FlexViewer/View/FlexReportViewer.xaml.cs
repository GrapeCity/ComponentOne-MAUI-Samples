using C1.Report;
using Strings = FlexViewer.Resources.Resources;

namespace FlexViewer
{
    /// <summary>
    /// Sample page that lets the user pick a FlexReport from the bundled report
    /// definition and renders it in a <see cref="C1.Maui.Viewer.FlexViewer"/>.
    /// </summary>
    public partial class FlexReportViewer : ContentPage
    {
        private bool _isLoading;
        private string? _pendingReportName;

        public FlexReportViewer()
        {
            InitializeComponent();
            flexViewer.Loaded += OnFlexViewerLoaded;
        }

        private async void OnFlexViewerLoaded(object? sender, EventArgs e)
        {
            flexViewer.Loaded -= OnFlexViewerLoaded;
            await LoadReportListAsync();
        }

        private async Task LoadReportListAsync()
        {
            loadingOverlay.SetLoading(true, Strings.LoadingReportText);

            try
            {
                var reportNames = await Task.Run(async () =>
                {
                    var path = await ViewerAssets.EnsureAssetCopiedAsync(ViewerAssets.FlexReportFileName);
                    return FlexReport.GetReportList(path);
                });

                reportPicker.ItemsSource = reportNames;

                if (reportNames?.Length > 0)
                    reportPicker.SelectedIndex = 0; // triggers ReportPicker_SelectedIndexChanged, which loads it
                else
                    loadingOverlay.SetLoading(false);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FlexViewer] Report list load failed: {ex}");
                await DisplayAlert(Strings.ReportLoadErrorTitle, ex.Message, Strings.OkButton);
                loadingOverlay.SetLoading(false);
            }
        }

        private async void ReportPicker_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (reportPicker.SelectedItem is not string reportName)
                return;

            await LoadReportAsync(reportName);
        }

        /// <summary>
        /// Loads and renders the given report. If a load is already in flight,
        /// the request is remembered and re-run once the current load finishes,
        /// so a selection made mid-load is never silently dropped.
        /// </summary>
        private async Task LoadReportAsync(string reportName)
        {
            if (_isLoading)
            {
                _pendingReportName = reportName;
                return;
            }

            _isLoading = true;
            loadingOverlay.SetLoading(true, Strings.LoadingReportText);

            try
            {
                var report = await Task.Run(async () =>
                {
                    var flxrPathTask = ViewerAssets.EnsureAssetCopiedAsync(ViewerAssets.FlexReportFileName);
                    var dbPathTask = ViewerAssets.EnsureAssetCopiedAsync(ViewerAssets.SqliteDbFileName);
                    await Task.WhenAll(flxrPathTask, dbPathTask);
                    var flxrPath = flxrPathTask.Result;
                    var dbPath = dbPathTask.Result;

                    // One-time substitution of the SQLite DB path into the report XML.
                    var flxrXml = await File.ReadAllTextAsync(flxrPath);
                    if (flxrXml.Contains("{DB_PATH}"))
                        await File.WriteAllTextAsync(flxrPath, flxrXml.Replace("{DB_PATH}", dbPath));

                    var flexReport = new FlexReport();
                    flexReport.Load(flxrPath, reportName);   // synchronous — must be off main thread
                    await flexReport.GenerateAsync();
                    return flexReport;
                });

                flexViewer.DocumentSource = report;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FlexViewer] FlexReport load failed: {ex}");
                await DisplayAlert(Strings.ReportLoadErrorTitle, ex.Message, Strings.OkButton);
            }
            finally
            {
                _isLoading = false;
                loadingOverlay.SetLoading(false);

                if (_pendingReportName != null)
                {
                    var nextReportName = _pendingReportName;
                    _pendingReportName = null;
                    await LoadReportAsync(nextReportName);
                }
            }
        }
    }
}
