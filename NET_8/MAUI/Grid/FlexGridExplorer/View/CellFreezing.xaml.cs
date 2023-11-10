using C1.Maui.Grid;
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
            Dictionary<int, string> dct = new Dictionary<int, string>();
            foreach (var country in Customer.GetCountries())
            {
                dct[dct.Count] = country.Value;
            }
            grid.Columns["CountryID"].DataMap = new GridDataMap { ItemsSource = dct, SelectedValuePath = "Key", DisplayMemberPath = "Value" };
            grid.Columns["CountryID"].AllowMerging = true;
            grid.MinColumnWidth = 85;
        }
    }
}
