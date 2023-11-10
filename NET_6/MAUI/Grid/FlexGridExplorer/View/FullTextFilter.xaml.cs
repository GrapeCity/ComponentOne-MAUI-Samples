using FlexGridExplorer.Strings;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace FlexGridExplorer
{
    public partial class FullTextFilter : ContentPage
    {
        public FullTextFilter()
        {
            InitializeComponent();

            Title = AppResources.FullTextFilterTitle;
            filter.Placeholder = AppResources.FilterPlaceholderText;
            filter.Keyboard = Keyboard.Plain;
            filter.Text = "Rich";
            matchCaseLabel.Text = AppResources.MatchCaseLabel;
            matchWholeWordLabel.Text = AppResources.MatchWholeWordLabel;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.MinColumnWidth = 85;
        }
    }
}
