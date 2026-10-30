using Strings = FlexViewer.Resources.Resources;

namespace FlexViewer
{
    /// <summary>
    /// Full-bounds busy indicator (backdrop + spinner + message) shown while a
    /// sample page loads its document. Reused by the PDF, Report and Report
    /// Service pages.
    /// </summary>
    public partial class LoadingOverlay : ContentView
    {
        public LoadingOverlay() => InitializeComponent();

        /// <summary>
        /// Shows or hides the overlay, safe to call from any thread.
        /// </summary>
        public void SetLoading(bool isLoading, string? message = null)
        {
            void Apply()
            {
                if (isLoading)
                    MessageLabel.Text = message ?? Strings.LoadingText;

                Root.IsVisible = isLoading;
            }

            if (MainThread.IsMainThread)
                Apply();
            else
                MainThread.BeginInvokeOnMainThread(Apply);
        }
    }
}
