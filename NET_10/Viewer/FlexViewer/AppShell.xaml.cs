namespace FlexViewer
{
    /// <summary>
    /// App shell hosting the Getting Started, PDF, Report and Report Service
    /// sample pages behind a left-hand navigation flyout.
    /// </summary>
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

#if WINDOWS
            FlyoutBehavior = FlyoutBehavior.Locked;
#endif
        }

        private async void FlyoutHeaderBrand_Tapped(object? sender, TappedEventArgs e)
        {
            try
            {
                await Launcher.Default.OpenAsync(new Uri("https://developer.mescius.com/componentone/maui-ui-controls"));
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[FlexViewer] Failed to open product page: {ex}");
            }
        }
    }
}
