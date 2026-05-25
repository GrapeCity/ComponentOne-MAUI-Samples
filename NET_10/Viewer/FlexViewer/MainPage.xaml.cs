using C1.Document;
using C1.Maui.Viewer;
using C1.Report;
using CommunityToolkit.Maui.Core;
using System.Net;

namespace FlexViewer
{
    public partial class MainPage : ContentPage
    {
        private string[]? _cachedReportList;
        private bool _isLoading;

        public MainPage()
        {
            InitializeComponent();
            flexViewer.Loaded += OnFlexViewerLoaded;
        }

        private async void OnFlexViewerLoaded(object? sender, EventArgs e)
        {
            flexViewer.Loaded -= OnFlexViewerLoaded;
            StyleViewerToolbar();
            await LoadDefaultPdfAsync();
        }

        private static async Task<string> EnsureAssetCopiedAsync(string logicalName)
        {
            var dest = Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, logicalName);

            if (!File.Exists(dest))
            {
                using var src = await FileSystem.OpenAppPackageFileAsync(logicalName);
                using var dst = File.Create(dest);
                await src.CopyToAsync(dst);
            }

            return dest;
        }

        private static Task<string> GetSqliteDbPathAsync()
            => EnsureAssetCopiedAsync("C1NWind.db");

        private static Task<string> GetFlexReportPathAsync()
            => EnsureAssetCopiedAsync("FlexCommonTasks.flxr");

        /// <summary>
        /// Shows or hides the full-screen loading overlay, safe to call from any thread.
        /// </summary>
        private void SetLoading(bool isLoading, string message = "Loading…")
        {
            void Apply()
            {
                LoadingLabel.Text = message;
                LoadingOverlay.IsVisible = isLoading;
            }

            if (MainThread.IsMainThread)
                Apply();
            else
                MainThread.BeginInvokeOnMainThread(Apply);
        }

        private void StyleViewerToolbar()
        {
            var isDark = Application.Current?.RequestedTheme == AppTheme.Dark;

            // StatusBackground and StatusForeground live on FlexViewer, not Pane.
            if (isDark)
            {
                flexViewer.StatusBackground = new SolidColorBrush(Color.FromArgb("#1A1A28"));
                flexViewer.StatusForeground  = new SolidColorBrush(Color.FromArgb("#CCCCEE"));
            }
            else
            {
                flexViewer.StatusBackground = new SolidColorBrush(Colors.White);
                flexViewer.StatusForeground  = new SolidColorBrush(Color.FromArgb("#333333"));
            }
        }
        private async Task LoadDefaultPdfAsync()
        {
            if (_isLoading) return;
            _isLoading = true;
            SetLoading(true, "Loading PDF…");

            try
            {
                var dest = await EnsureAssetCopiedAsync("DefaultDocument.pdf");

                if (new FileInfo(dest).Length == 0)
                    throw new IOException($"Copied file is empty: '{dest}'. Check MauiAsset packaging.");

                // LoadFromFile parses the whole PDF synchronously — runs on a background
                // thread to keep the UI thread free and prevent ANR on Android.
                var doc = await Task.Run(() =>
                {
                    var source = new C1PdfDocumentSource();
                    source.LoadFromFile(dest);
                    return source;
                });

                flexViewer.DocumentSource = doc;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FlexViewer] PDF load failed: {ex}");
                await DisplayAlert("PDF Load Error", ex.Message, "OK");
            }
            finally
            {
                _isLoading = false;
                SetLoading(false);
            }
        }

        private void PdfItem_Tapped(object? sender, TappedEventArgs e) => PdfButton_Clicked(sender!, e);

        private async void PdfButton_Clicked(object sender, EventArgs e)
        {
            PopupOverlay.IsVisible = false;
            FlexReportExpander.IsExpanded = false;
            await LoadDefaultPdfAsync();
        }

        private async void Expander_ExpandedChanged(object? sender, ExpandedChangedEventArgs e)
        {
            if (!e.IsExpanded || FlexReportList.ItemsSource != null)
                return;

            if (_cachedReportList == null)
            {
                // GetReportList + file I/O — keep off main thread
                _cachedReportList = await Task.Run(async () =>
                {
                    var path = await GetFlexReportPathAsync();
                    return FlexReport.GetReportList(path);
                });
            }

            // ItemsSource and SelectedItem must be set on the main thread.
            // The await above may have continued on a thread-pool thread,
            // so dispatch explicitly.
            MainThread.BeginInvokeOnMainThread(() =>
            {
                FlexReportList.ItemsSource = _cachedReportList;
                FlexReportList.SelectedItem = _cachedReportList.FirstOrDefault();
            });
        }

        private async void FlexReportList_SelectionChanged(object sender, EventArgs e)
        {
            if (FlexReportList.SelectedItem is not string reportName)
                return;

            if (_isLoading) return;
            _isLoading = true;
            PopupOverlay.IsVisible = false;
            FlexReportExpander.IsExpanded = false;
            SetLoading(true, "Loading report…");

            try
            {
                var report = await Task.Run(async () =>
                {
                    var flxrPath = await GetFlexReportPathAsync();
                    var dbPath = await GetSqliteDbPathAsync();

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
                await DisplayAlert("Report Load Error", ex.Message, "OK");
            }
            finally
            {
                _isLoading = false;
                SetLoading(false);
            }
        }

        private void ReportServiceItem_Tapped(object? sender, TappedEventArgs e) => ReportServiceMenu_Clicked(sender!, e);

        private async void ReportServiceMenu_Clicked(object sender, EventArgs e)
        {
            if (_isLoading) return;
            _isLoading = true;
            PopupOverlay.IsVisible = false;
            FlexReportExpander.IsExpanded = false;
            SetLoading(true, "Loading report service…");

            try
            {
                // TODO: Move SSRS credentials to environment variables or secure config.
                var source = new C1SSRSDocumentSource
                {
                    Language = new System.Globalization.CultureInfo("en-US"),
                    Credential = new NetworkCredential("ssrs_demo", "bjqveS5gh89BH1Q", "")
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
                await DisplayAlert("Report Service Error", ex.Message, "OK");
            }
            finally
            {
                _isLoading = false;
                SetLoading(false);
            }
        }

        private async void HeaderBrand_Tapped(object? sender, TappedEventArgs e)
        {
            await Launcher.Default.OpenAsync(new Uri("https://developer.mescius.com/componentone/maui-ui-controls"));
        }

        private void MenuPopupButton_Clicked(object sender, EventArgs e)
        {
            PopupSectionLabel.Text = "OPEN DOCUMENT";
            PopupOverlay.IsVisible = true;
        }

        private void OverlayBackground_Tapped(object sender, TappedEventArgs e)
        {
            PopupOverlay.IsVisible = false;
            FlexReportExpander.IsExpanded = false;
        }
    }
}
