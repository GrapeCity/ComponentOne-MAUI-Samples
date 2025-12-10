using C1.Chart;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class AxisBreak : ContentPage
    {
        DataService dataService = DataService.GetService();

        public AxisBreak()
        {
            InitializeComponent();
        }

        #region Data
        ChartType chartType = ChartType.Column;
        Point[]? _data;
        bool axisBreak = false;

        public ChartType ChartType
        {
            get => chartType;
            set { chartType = value; OnPropertyChanged(); }
        }

        public ChartType[] ChartTypes => new[] { ChartType.Column, ChartType.Line, ChartType.LineSymbols, ChartType.Area };

        public Point[] Data => _data == null ? _data = CreateData() : _data;

        Point[] CreateData()
        {
            var rnd = new Random();
            var pts = new Point[20];

            for (var i = 0; i < pts.Length; i++)
            {
                if (rnd.NextDouble() < 0.85)
                    pts[i] = new Point(i, rnd.Next(0, 10));
                else
                    pts[i] = new Point(i, 50 + rnd.Next(0, 50));
            }

            return pts;
        }

        #endregion

        private void switchAxisBreak_Toggled(object sender, ToggledEventArgs e)
        {
            axisBreak = ((Switch)sender).IsToggled;
            if (axisBreak)
            {
                if (flexChart.Rotated)
                    AxisBreakHelper.CreateAxisBreak(flexChart.AxisX, 0, 10, 50, 100);
                else
                    AxisBreakHelper.CreateAxisBreak(flexChart.AxisY, 0, 10, 50, 100);
            }
            else
                AxisBreakHelper.Remove(flexChart);
        }

        private void switchRotated_Toggled(object sender, ToggledEventArgs e)
        {
            flexChart.Rotated = ((Switch)sender).IsToggled;
            flexChart.AxisX.MajorGrid = flexChart.Rotated;
            flexChart.AxisY.MajorGrid = !flexChart.Rotated;

            if (axisBreak)
            {
                if (flexChart.Rotated)
                    AxisBreakHelper.CreateAxisBreak(flexChart.AxisX, 0, 10, 50, 100);
                else
                    AxisBreakHelper.CreateAxisBreak(flexChart.AxisY, 0, 10, 50, 100);
            }
        }

        private void switchAxisBreak_Loaded(object sender, EventArgs e)
        {
            ((Switch)sender).IsToggled = true;
        }
    }
}