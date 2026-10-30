using C1.Document;
using Strings = FlexViewer.Resources.Resources;

namespace FlexViewer
{
    /// <summary>
    /// Sample page that loads a local PDF document into a <see cref="C1.Maui.Viewer.FlexViewer"/>.
    /// </summary>
    public partial class PdfViewer : ContentPage
    {
        private bool _isLoading;

        public PdfViewer()
        {
            InitializeComponent();
            flexViewer.Loaded += OnFlexViewerLoaded;
        }

        private async void OnFlexViewerLoaded(object? sender, EventArgs e)
        {
            flexViewer.Loaded -= OnFlexViewerLoaded;
            await LoadDefaultPdfAsync();
        }

        private async Task LoadDefaultPdfAsync()
        {
            if (_isLoading) return;
            _isLoading = true;
            loadingOverlay.SetLoading(true, Strings.LoadingPdfText);

            try
            {
                var dest = await ViewerAssets.EnsureAssetCopiedAsync(ViewerAssets.DefaultPdfFileName);

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
                await DisplayAlert(Strings.PdfLoadErrorTitle, ex.Message, Strings.OkButton);
            }
            finally
            {
                _isLoading = false;
                loadingOverlay.SetLoading(false);
            }
        }
    }
}
