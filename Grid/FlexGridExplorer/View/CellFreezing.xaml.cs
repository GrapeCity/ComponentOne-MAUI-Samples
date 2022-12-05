using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;

namespace FlexGridExplorer
{
    public partial class CellFreezing : ContentPage
    {
        public CellFreezing()
        {
            InitializeComponent();

            Title = AppResources.CellFreezingTitle;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.Columns["Country"].AllowMerging = true;
            grid.MinColumnWidth = 85;
        }
    }
}
