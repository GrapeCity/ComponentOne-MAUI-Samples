﻿using C1.Maui.Calendar;

namespace CalendarExplorer;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .RegisterCalendarControls()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("cour.ttf", "Courier New");
                fonts.AddFont("ShantellSans-Regular.ttf", "ShantellSans");
            });

        return builder.Build();
    }
}