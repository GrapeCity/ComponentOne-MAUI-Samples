using C1.Chart;
using C1.Maui.Chart;
using C1.Maui.Chart.Palettes;
using FlexChartExplorer.Data;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class RangeZones : ContentPage
    {
        public RangeZones()
        {
            InitializeComponent();
            CreatePingChart(flexChart);
        }

        void CreatePingChart(FlexChart chart)
        {
            chart.BeginUpdate();

            chart.Header = "Ping";

            var zone1 = new RangeZone(Color.FromRgb( 0xe7, 0xf6, 0xec), name: "Good", maxY: 100);
            chart.Series.Add(zone1);

            var zone2 = new RangeZone(Color.FromRgb( 0xff, 0xf6, 0xe2), name: "Fair", minY: 100, maxY: 200);
            chart.Series.Add(zone2);

            var zone3 = new RangeZone(Color.FromRgb( 0xfd, 0xea, 0xea), name: "Poor", minY: 200);
            chart.Series.Add(zone3);

            foreach (var zone in new RangeZone[] { zone1, zone2, zone3 })
                zone.DataLabel.Position = LabelPosition.Right;

            var start = DateTime.Now.AddHours(-1);
            var duration = TimeSpan.FromHours(6);
            var interval = TimeSpan.FromSeconds(60);

            var data1 = DataService.GeneratePingData(new Profile
            {
                BaseLatencyMs = 18,
                JitterMs = 5,
                SpikeChance = 0.02,
                PacketLossChance = 0.002
            }, start: start, duration: duration, interval: interval);

            var data2 = DataService.GeneratePingData(new Profile
            {
                BaseLatencyMs = 85,
                JitterMs = 18,
                SpikeChance = 0.05,
                PacketLossChance = 0.01
            }, start: start, duration: duration, interval: interval);

            chart.Binding = "LatencyMs";
            chart.BindingX = "Timestamp";

            chart.ChartType = ChartType.Line;

            var series1 = new Series() { ItemsSource = data1, SeriesName = "8.8.8.8 (Google)" };
            series1.Style = new ChartStyle() { Stroke = Qualitative.Dark2[0] };

            var series2 = new Series() { ItemsSource = data2, SeriesName = "1.1.1.1 (Cloudflare)" };
            series2.Style = new ChartStyle() { Stroke = Qualitative.Dark2[2] };

            chart.Series.Add(series1);
            chart.Series.Add(series2);

            chart.AxisX.Title = "Time →";
            chart.AxisY.Title = "Ping, ms →";

            chart.AxisX.MajorGrid = chart.AxisY.MajorGrid = true;
            chart.AxesOnTop = true;

            chart.LegendPosition = Position.Top;

            chart.EndUpdate();
        }
    }
}