using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace FlexGridExplorer
{
    public partial class EditConfirmation : ContentPage
    {
        public EditConfirmation()
        {
            InitializeComponent();

            Title = AppResources.EditConfirmationTitle;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.MinColumnWidth = 85;
        }

        private object _originalValue;

        private void OnBeginningEdit(object sender, GridCellEditEventArgs e)
        {
            _originalValue = grid[e.CellRange.Row, e.CellRange.Column];
        }

        private void OnCellEditEnded(object sender, GridCellEditEventArgs e)
        {
            var originalValue = _originalValue;
            var currentValue = grid[e.CellRange.Row, e.CellRange.Column];
            if (!e.CancelEdits && (originalValue == null && currentValue != null || !originalValue.Equals(currentValue)))
            {
                DisplayAlert(AppResources.EditConfirmationQuestionTitle, AppResources.EditConfirmationQuestion, AppResources.Apply, AppResources.Cancel).ContinueWith(t =>
                {
                    if (!t.Result)
                    {
                        grid[e.CellRange.Row, e.CellRange.Column] = originalValue;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext());
            }
        }
    }
}
