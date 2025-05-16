using C1.Chart;
using System.Globalization;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class HeatmapPage : ContentPage
    {
        DataService dataService = DataService.GetService();

        public HeatmapPage()
        {
            this.InitializeComponent();
        }

        private void ChartLoaded(object sender, EventArgs e)
        {
            if (chart.Series.Count > 0)
                return;

            chart.BeginUpdate();

            var scale = new C1.Maui.Chart.GradientColorScale() { Min = -20, Max = 20 };
            scale.Colors = new List<Color> { Colors.Blue, Colors.White, Colors.Red };

            var hmap = new C1.Maui.Chart.Heatmap();
            hmap.ItemsSource = new double[,] {
                {  3.0, 3.1, 5.7, 8.2, 12.5, 15.0, 17.1, 17.1, 14.3, 10.6, 6.6, 4.3 },
                { -9.3, -7.7, -2.2, 5.8, 13.1, 16.6, 18.2, 16.4, 11.0, 5.1, -1.2, -6.1},
                 { -15.1, -12.5, -5.2, 3.1, 10.1, 15.5, 18.3, 15.0, 9.4, 1.4, -5.6, -11.4},
                };
            hmap.ColorScale = scale;
            chart.Series.Add(hmap);

            chart.AxisX.ItemsSource = CultureInfo.CurrentUICulture.DateTimeFormat.AbbreviatedMonthNames;
            chart.AxisY.ItemsSource = new string[] { "Amsterdam", "Moscow", "Perm" };

            chart.EndUpdate();
        }

    }
}