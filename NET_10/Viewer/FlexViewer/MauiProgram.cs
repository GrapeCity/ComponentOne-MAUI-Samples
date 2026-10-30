using Microsoft.Extensions.Logging;
using C1.Maui.Viewer;
using CommunityToolkit.Maui;

namespace FlexViewer
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            // Test hook — pick any of: ja-JP, ko-KR, zh-CN
            //var testCulture = new System.Globalization.CultureInfo("ko-KR");
            //System.Globalization.CultureInfo.CurrentUICulture = testCulture;
            //System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = testCulture;

            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .RegisterInputControls()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("materialdesignicons-webfont.ttf", "MDI");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
