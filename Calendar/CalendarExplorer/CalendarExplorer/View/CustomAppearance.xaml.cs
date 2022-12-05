using C1.Maui.Calendar;
using CalendarExplorer.Resources;
using Microsoft.Maui.Controls.Xaml;
using System;

namespace CalendarExplorer
{
    public partial class CustomAppearance
    {
        public CustomAppearance()
        {
            InitializeComponent();
            Title = AppResources.CustomAppearanceTitle;

            calendar.ViewModeAnimation.AnimationMode = CalendarViewModeAnimationMode.ZoomOutIn;
            calendar.ViewModeAnimation.ScaleFactor = 1.1;
            calendar.ViewModeAnimation.Duration = TimeSpan.FromMilliseconds(150);

            calendar.NavigateAnimation.Duration = TimeSpan.FromMilliseconds(150);
        }

    }
}
