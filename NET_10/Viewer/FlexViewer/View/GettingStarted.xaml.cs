namespace FlexViewer
{
    /// <summary>
    /// Welcome page introducing the FlexViewer control and linking to the
    /// PDF, Report and Report Service sample pages.
    /// </summary>
    public partial class GettingStarted : ContentPage
    {
        public GettingStarted() => InitializeComponent();

        private async void PdfBadge_Tapped(object? sender, TappedEventArgs e)
            => await NavigateToAsync(nameof(PdfViewer));

        private async void ReportBadge_Tapped(object? sender, TappedEventArgs e)
            => await NavigateToAsync(nameof(FlexReportViewer));

        private async void SsrsBadge_Tapped(object? sender, TappedEventArgs e)
            => await NavigateToAsync(nameof(SsrsViewer));

        private static async Task NavigateToAsync(string route)
        {
            try
            {
                await Shell.Current.GoToAsync($"//{route}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FlexViewer] Navigation to '{route}' failed: {ex}");
            }
        }
    }
}
