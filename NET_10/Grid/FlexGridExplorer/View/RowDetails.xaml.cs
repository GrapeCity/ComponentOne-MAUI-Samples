using C1.Maui.Core;
using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;

namespace FlexGridExplorer
{
    public partial class RowDetails : ContentPage
    {
        public RowDetails()
        {
            InitializeComponent();

            Title = AppResources.RowDetailsTitle;
            lblMode.Text = AppResources.DetailsVisibiltyMode;

            showItemsPicker.Items.Add(GridDetailVisibilityMode.ExpandSingle.ToString());
            showItemsPicker.Items.Add(GridDetailVisibilityMode.ExpandMultiple.ToString());
            showItemsPicker.Items.Add(GridDetailVisibilityMode.Selection.ToString());
            showItemsPicker.SelectedIndex = 1;
            showItemsPicker.SelectedIndexChanged += (s, e) =>
              {
                  switch (showItemsPicker.SelectedIndex)
                  {
                      case 0:
                          details.DetailVisibilityMode = GridDetailVisibilityMode.ExpandSingle;
                          break;
                      case 1:
                          details.DetailVisibilityMode = GridDetailVisibilityMode.ExpandMultiple;
                          break;
                      case 2:
                          details.DetailVisibilityMode = GridDetailVisibilityMode.Selection;
                          break;
                  }
              };

            var data = Customer.GetCustomerList(1000);
            grid.ItemsSource = data;
            grid.MinColumnWidth = 85;
        }

        private async void OnDetailLoading(object sender, GridDetailLoadingEventArgs<object> e)
        {
            var deferal = e.GetDeferral();
            try
            {
                //Simulates doing something
                await System.Threading.Tasks.Task.Delay(2000);
            }
            finally
            {
                deferal.Complete();
            }
        }
    }
}
