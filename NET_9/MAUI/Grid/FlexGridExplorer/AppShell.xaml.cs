namespace FlexGridExplorer;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

#if WINDOWS
        FlyoutBehavior = FlyoutBehavior.Locked;
#endif
    }
}
