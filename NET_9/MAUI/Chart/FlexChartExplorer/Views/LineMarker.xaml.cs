using C1.Chart;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class LineMarker : ContentPage
    {
        DataService dataService = DataService.GetService();

        public LineMarker()
        {
            this.InitializeComponent();
        }

        void PositionChanged(object sender, C1.Maui.Chart.Interaction.PositionChangedArgs e)
        {
            var pt = e.Position;
            var dataPoint = flexChart.PointToData(e.Position);
            var date = DateTime.FromOADate(dataPoint.X);
            var i = (int)(date.Date - Data[0].Date).TotalDays;
            if (i >= 0 && i < Data.Count)
            {
                var item = Data[i];
                marker.Content =
                    $"{item.Date.ToString("MMM-dd")}\n" +
                    $"O:{item.Open}\nH:{item.High}\nL:{item.Low}\nC:{item.Close}";
            }
        }

        #region Data

        List<Quote>? _data = null;

        public List<Quote> Data => _data != null ? _data : _data = dataService.CreateFinancialData(new DateTime(2022, 1, 1), 180);

        #endregion
    }
}