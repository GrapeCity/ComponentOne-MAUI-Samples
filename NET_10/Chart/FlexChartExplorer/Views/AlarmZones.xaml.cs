using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class AlarmZones : ContentPage
    {
        public AlarmZones()
        {
            InitializeComponent();
        }

        void Rendering(object sender, C1.Maui.Chart.RenderEventArgs e)
        {
            var xmin = flexChart.AxisX.ActualMin;
            var xmax = flexChart.AxisX.ActualMax;

            DrawAlarmZone(e.Engine, "Critical", xmin, 80, xmax, 100);
            DrawAlarmZone(e.Engine, "Caution", xmin, 50, xmax, 80);
            DrawAlarmZone(e.Engine, "Success", xmin, 0, xmax, 50);
        }

        void DrawAlarmZone(IRenderEngine engine, string color, double xMin, double yMin, double xMax, double yMax)
        {
            var pt1 = flexChart.DataToPoint(new Point(xMin, yMin));
            var pt2 = flexChart.DataToPoint(new Point(xMax, yMax));
            var brush = Resources[color];

            engine.SetFill(brush);
            engine.DrawRect(Math.Min(pt1.X, pt2.X), Math.Min(pt1.Y, pt2.Y),
                Math.Abs(pt2.X - pt1.X), Math.Abs(pt2.Y - pt1.Y));
        }


        #region Data

        List<Point> data = null;

        public List<Point> Data => data != null ? data : data = GetData();

        Random random = new Random();

        List<Point> GetData()
        {
            var list = new List<Point>();

            for (var i = 0; i < 100; i++)
                list.Add(new Point(i, GetRandomTemp()));

            return list;
        }

        double GetRandomTemp()
        {
            var seed = random.NextDouble();
            if (seed > 0.9)
                return (Math.Round(random.NextDouble() * 800) / 10) + 20;
            else if (seed > 0.6)
                return (Math.Round(random.NextDouble() * 600) / 10) + 20;
            else
                return (Math.Round(random.NextDouble() * 300) / 10) + 20;
        }


        #endregion

    }
}