using C1.Document;
using C1.Report;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls.PlatformConfiguration;
using Newtonsoft.Json;
using System.Data.Common;
using System.Data.OleDb;
using System.Net;
using System.Reflection;


namespace FlexViewer
{
    public partial class MainPage : ContentPage
    {
        private string[]? _cachedReportList;

        public MainPage()
        {
            InitializeComponent();
            _ = LoadDefaultPdfAsync();
        }

        private static async Task<string> EnsureAssetCopiedAsync(string logicalName)
        {
            var dest = Path.Combine(FileSystem.AppDataDirectory, logicalName);

            if (!File.Exists(dest))
            {
                using var src = await FileSystem.OpenAppPackageFileAsync(logicalName);
                using var dst = File.Create(dest);
                await src.CopyToAsync(dst);
            }

            return dest;
        }

        private async Task LoadDefaultPdfAsync()
        {
            const string assetName = "DefaultDocument.pdf";
            var dest = await EnsureAssetCopiedAsync(assetName);

            if (new FileInfo(dest).Length == 0)
                throw new IOException($"Copied file is empty at '{dest}'. Check MauiAsset packaging.");

            var doc = new C1PdfDocumentSource();
            doc.LoadFromFile(dest);

            flexViewer.DocumentSource = doc;
        }

        private async void PdfButton_Clicked(object sender, EventArgs e)
        {
            await LoadDefaultPdfAsync();
        }

        private static async Task<string> GetSqliteDbPathAsync()
        {
            return await EnsureAssetCopiedAsync("C1NWind.db");
        }

        private static async Task<string> GetFlexReportPathAsync()
        {
#if IOS || MACCATALYST || ANDROID
            const string logicalName = "FlexCommonTasksSQLite.flxr";
#else
        const string logicalName = "FlexCommonTasks.flxr";
#endif
            return await EnsureAssetCopiedAsync(logicalName);
        }

        private async void FlexReportList_SelectionChanged(object sender, EventArgs e)
        {
            if (FlexReportList.SelectedItem == null)
                return;

            string reportName = FlexReportList.SelectedItem.ToString();

            var document = await Task.Run(async () =>
            {
                var flxrPath = await GetFlexReportPathAsync();

#if IOS || MACCATALYST || ANDROID
                var dbPath = await GetSqliteDbPathAsync();
#endif

                var flexReport = new FlexReport();
                flexReport.Load(flxrPath, reportName);

#if IOS || MACCATALYST || ANDROID
                var mainDataSource = flexReport.DataSources["Main"];
                if (mainDataSource != null && !string.IsNullOrEmpty(mainDataSource.ConnectionString))
                {
                    mainDataSource.ConnectionString =
                        mainDataSource.ConnectionString.Replace("{DB_PATH}", dbPath);
                }
#endif

                flexReport.Generate();
                return flexReport;
            });

            MainThread.BeginInvokeOnMainThread(() =>
            {
                flexViewer.DocumentSource = document;
            });
        }

        private async void Expander_ExpandedChanged(object? sender, ExpandedChangedEventArgs e)
        {
            if (!e.IsExpanded || FlexReportList.ItemsSource != null)
                return;

            if (_cachedReportList == null)
            {
                _cachedReportList = await Task.Run(async () =>
                {
                    var path = await GetFlexReportPathAsync();
                    return FlexReport.GetReportList(path);
                });
            }

            MainThread.BeginInvokeOnMainThread(() =>
            {
                FlexReportList.ItemsSource = _cachedReportList;
                FlexReportList.SelectedItem = _cachedReportList.FirstOrDefault();
            });
        }


        private void ReportServiceMenu_Clicked(object sender, EventArgs e)
        {
            var c1SsrsDocumentSource1 = new C1SSRSDocumentSource
            {
                Language = new System.Globalization.CultureInfo("en-US"),
                Credential = new NetworkCredential("ssrs_demo", "bjqveS5gh89BH1Q", "")
            };

            c1SsrsDocumentSource1.DocumentLocation = new SSRSReportLocation( "http://ssrs.componentone.com:8000/ReportServer", "/AdventureWorks/Employee Sales Summary Detail");
            c1SsrsDocumentSource1.Generate();
            flexViewer.DocumentSource = c1SsrsDocumentSource1;
            PopupOverlay.IsVisible = false;
            FlexReportExpander.IsExpanded = false;
        }


        private async void HeaderMenuButton_Clicked(object sender, EventArgs e)
        {
            var uri = new Uri("https://developer.mescius.com/componentone/maui-ui-controls");
            await Launcher.Default.OpenAsync(uri);
        }

        private void MenuPopupButton_Clicked(object sender, EventArgs e)
        {
            PopupOverlay.IsVisible = true;
        }

        private void OverlayBackground_Tapped(object sender, TappedEventArgs e)
        {
            PopupOverlay.IsVisible = false;
            FlexReportExpander.IsExpanded = false;
        }
    }
}


