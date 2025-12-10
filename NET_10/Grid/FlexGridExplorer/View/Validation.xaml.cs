using C1.Maui.Core;
using FlexGridExplorer.Strings;

namespace FlexGridExplorer
{
    public partial class Validation : ContentPage
    {
        public Validation()
        {
            InitializeComponent();
            Title = AppResources.ValidationTitle;

            foreach (var value in new string[] { AppResources.ValidationDefaultIcon, AppResources.ValidationBugIcon, AppResources.ValidationExclamationTriangleIcon, AppResources.ValidationExclamationOctagonIcon })
            {
                errorIconTemplate.Items.Add(value);
            }
            errorIconTemplate.SelectedIndex = 0;

            foreach (var value in new string[] { AppResources.ValidationDefaultStyle, AppResources.ValidationClassicStyle, AppResources.ValidationDataGridStyle })
            {
                errorStyles.Items.Add(value);
            }
            errorStyles.SelectedIndex = 0;

            var list = Customer.GetCustomerList(100);
            //Set some properties so the validation appears form scratch
            list[1].LastName = "";
            list[3].Address = "1";
            list[4].Email = "Lorem";
            list[5].FirstName = "";
            grid.ItemsSource = list;
        }

        private void errorIconTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (errorIconTemplate.SelectedIndex)
            {
                case 0:
                    grid.ErrorIconTemplate = C1IconTemplate.AlertCircle;
                    break;
                case 1:
                    grid.ErrorIconTemplate = Resources["BugIcon"] as DataTemplate;
                    break;
                case 2:
                    grid.ErrorIconTemplate = Resources["ExclamationTriangleIcon"] as DataTemplate;
                    break;
                case 3:
                    grid.ErrorIconTemplate = Resources["ExclamationOctagonIcon"] as DataTemplate;
                    break;
            }
        }

        private void errorStyles_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (errorStyles.SelectedIndex)
            {
                case 0:
                    grid.ClearValue(StyleProperty);
                    break;
                case 1:
                    grid.Style = Resources["ClassicErrorStyle"] as Style;
                    break;
                case 2:
                    grid.Style = Resources["DataGridErrorStyle"] as Style;
                    break;
            }
        }
    }
}
