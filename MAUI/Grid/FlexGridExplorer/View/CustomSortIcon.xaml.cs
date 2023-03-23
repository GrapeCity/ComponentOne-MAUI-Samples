using C1.DataCollection;
using C1.Maui.Core;
using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;
using System;

namespace FlexGridExplorer
{
    public partial class CustomSortIcon : ContentPage
    {
        public CustomSortIcon()
        {
            InitializeComponent();
            Title = AppResources.CustomSortIconTitle;

            sortIconPosition.Title = AppResources.SortIconPosition;
            foreach (var value in Enum.GetValues(typeof(GridSortIconPosition)))
            {
                sortIconPosition.Items.Add(value.ToString());
            }
            sortIconPosition.SelectedIndex = 1;

            sortIconTemplate.Title = AppResources.SortIconTemplate;
            foreach (var value in new string[] { "Custom 1", "Custom 2", nameof(C1IconTemplate.TriangleUp), nameof(C1IconTemplate.TriangleNorth), nameof(C1IconTemplate.ChevronUp), nameof(C1IconTemplate.ArrowUp) })
            {
                sortIconTemplate.Items.Add(value);
            }
            sortIconTemplate.SelectedIndex = 0;

            LoadData();
        }

        private async void LoadData()
        {
            var cv = new C1DataCollection<Customer>(Customer.GetCustomerList(100));
            await cv.SortAsync(new SortDescription("FirstName", SortDirection.Ascending), new SortDescription("LastName", SortDirection.Descending));
            grid.ItemsSource = cv;
        }

        private void SortIconPositionChanged(object sender, EventArgs e)
        {
            grid.SortIconPosition = (GridSortIconPosition)Enum.Parse(typeof(GridSortIconPosition), sortIconPosition.Items[sortIconPosition.SelectedIndex]);
        }

        private void SortIconTemplateChanged(object sender, EventArgs e)
        {
            switch (sortIconTemplate.SelectedIndex)
            {
                case 0:
                    grid.SortAscendingIconTemplate = Resources["SortAscendingIcon"] as DataTemplate;
                    grid.SortDescendingIconTemplate = null;
                    grid.SortIndeterminateIconTemplate = null;
                    break;
                case 1:
                    grid.SortAscendingIconTemplate = Resources["Sort2AscendingIcon"] as DataTemplate;
                    grid.SortDescendingIconTemplate = Resources["Sort2DescendingIcon"] as DataTemplate;
                    grid.SortIndeterminateIconTemplate = Resources["Sort2Icon"] as DataTemplate;
                    break;
                case 2:
                    grid.SortAscendingIconTemplate = C1IconTemplate.TriangleUp;
                    grid.SortDescendingIconTemplate = null;
                    grid.SortIndeterminateIconTemplate = null;
                    break;
                case 3:
                    grid.SortAscendingIconTemplate = C1IconTemplate.TriangleNorth;
                    grid.SortDescendingIconTemplate = null;
                    grid.SortIndeterminateIconTemplate = null;
                    break;
                case 4:
                    grid.SortAscendingIconTemplate = C1IconTemplate.ChevronUp;
                    grid.SortDescendingIconTemplate = null;
                    grid.SortIndeterminateIconTemplate = null;
                    break;
                case 5:
                    grid.SortAscendingIconTemplate = C1IconTemplate.ArrowUp;
                    grid.SortDescendingIconTemplate = null;
                    grid.SortIndeterminateIconTemplate = null;
                    break;
            }
        }
    }
}
