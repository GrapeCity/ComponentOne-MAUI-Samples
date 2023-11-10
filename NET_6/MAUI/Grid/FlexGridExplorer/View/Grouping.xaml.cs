using C1.DataCollection;
using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;
using System.Threading.Tasks;

namespace FlexGridExplorer
{
    public partial class Grouping : ContentPage
    {
        C1DataCollection<Customer> _dataCollection;
        public Grouping()
        {
            InitializeComponent();

            Title = AppResources.GroupingTitle;
 
            var task = UpdateVideos();
        }

        private async Task UpdateVideos()
        {
            var data = Customer.GetCustomerList(100);
            _dataCollection = new C1DataCollection<Customer>(data);
            await _dataCollection.GroupAsync(c => c.Country);          
            grid.ItemsSource = _dataCollection;
            grid.MinColumnWidth = 85;
        }
    } 
}
