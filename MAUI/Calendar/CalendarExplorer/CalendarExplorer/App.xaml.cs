using Microsoft.Maui.Controls;

namespace CalendarExplorer;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        MainPage = new AppShell();
    }
}