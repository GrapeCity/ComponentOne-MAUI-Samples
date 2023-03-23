using C1.Maui.Calendar;
using CalendarExplorer.Resources;
using Microsoft.Maui.Controls.Xaml;
using System;

namespace CalendarExplorer
{
    public partial class CustomSelection
    {
        public CustomSelection()
        {
            InitializeComponent();
            Title = AppResources.CustomSelectionTitle;
        }

        private void OnSelectionChanging(object sender, CalendarSelectionChangingEventArgs e)
        {
            foreach (var date in e.SelectedDates.ToArray())
            {
                if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
                    e.SelectedDates.Remove(date);
            }
        }

        public string CustomSelectionMessage
        {
            get
            {
                return AppResources.CustomSelectionMessage;
            }
        }
    }
}
