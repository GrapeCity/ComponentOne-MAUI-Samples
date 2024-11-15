using C1.Chart;
using System.Globalization;
using System.ComponentModel;
using FlexChartExplorer.Data;
using C1.Maui.Chart;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class ColumnHeatmapPage : ContentPage
    {
        DataService dataService = DataService.GetService();

        public ColumnHeatmapPage()
        {
            this.InitializeComponent();
        }

        private void ChartLoaded(object sender, EventArgs e)
        {
            if (heatmap.ItemsSource == null)
            {
                // init data
                heatmap.ItemsSource = HeatmapData;
                heatmap.StartX = Data[0].Date.ToOADate();

                chart.AxisX.Min = Data[0].Date.ToOADate() - 0.5;
                chart.AxisX.Max = Data[Data.Count - 1].Date.ToOADate() + 0.5;
            }
        }

        #region Data
        double[,] heatmapData = null;
        public List<TemperatureDiff> Data { get; set; } = DataService.GetTemperatureDifferenceData();

        public double[,] HeatmapData
        {
            get
            {
                if (heatmapData == null)
                {
                    heatmapData = new double[Data.Count, 1];
                    for (int i = 0; i < Data.Count; i++)
                        heatmapData[i, 0] = Data[i].Diff >= 0 ? 0 : 1;
                }
                return heatmapData;
            }
        }
        #endregion

        private void SymbolRendering(object sender, RenderSymbolEventArgs e)
        {
            var clr = ((TemperatureDiff)e.Item).Diff >= 0 ? Resources["clr1"] : Resources["clr2"];
            e.Engine.SetFill(clr);
            e.Engine.SetStrokeThickness(1.5);
            e.Engine.SetStroke(clr);
        }
    }

    public class ColorList : List<Color>
    {
    }
}