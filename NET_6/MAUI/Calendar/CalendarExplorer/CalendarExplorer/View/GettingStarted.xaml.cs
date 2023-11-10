using CalendarExplorer.Resources;
using Microsoft.Maui.Controls.Xaml;

namespace CalendarExplorer
{
    public partial class GettingStarted
    {
        public GettingStarted()
        {
            InitializeComponent();
            Title = AppResources.GettingStartedTitle;
            calendar.BoldedDates = new List<DateTime>() { DateTime.Today.AddDays(3)};
        }
    }
}
