using C1.DataCollection;
using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace FlexGridExplorer
{
    public partial class OnDemand : ContentPage
    {
        YouTubeDataCollection _dataCollection;

        public OnDemand()
        {
            InitializeComponent();

            Title = AppResources.OnDemandTitle;
            search.Placeholder = AppResources.SearchPlaceholderText;
            emptyListLabel.Text = AppResources.EmptyListText;

            Load();
        }

        private async void Load()
        {
            _dataCollection = new YouTubeDataCollection() { PageSize = 25 };
            var grouping = new C1GroupDataCollection<YouTubeVideo>(_dataCollection, false);
            await grouping.GroupAsync("PublishedDay");
            grid.ItemsSource = grouping;
            grid.MinColumnWidth = 85;
            search.Text = "Microsoft MAUI";
            var task = PerformSearch();
        }

        private async void OnCompleted(object sender, EventArgs e)
        {
            await PerformSearch();
            grid.Focus();
        }

        private async Task PerformSearch()
        {
            try
            {
                activityIndicator.IsRunning = true;
                grid.Focus();
                await _dataCollection.SearchAsync(search.Text);
            }
            finally
            {
                activityIndicator.IsRunning = false;
            }
        }

        private void OnCellDoubleTapped(object sender, GridCellRangeEventArgs e)
        {
            if (e.CellType == GridCellType.Cell)
            {
                var video = grid.Rows[e.CellRange.Row].DataItem as YouTubeVideo;
                if (video != null)
                    _ = Launcher.OpenAsync(new Uri(video.Link));
            }
        }
    }
}
