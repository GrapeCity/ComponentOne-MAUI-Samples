using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class HitTest : ContentPage
    {
        public HitTest()
        {
            InitializeComponent();

           
            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                var point = e.GetPosition(flexChart);
                if (point == null)
                    return;

                var hitTestInfo = flexChart.HitTest( point.Value);

                if (hitTestInfo != null)
                {
                    label1.Text = $"Chart element: {hitTestInfo.ChartElement}";
                    if (hitTestInfo.Distance < 10 && hitTestInfo.PointIndex >= 0)
                        label2.Text = $"Series: {hitTestInfo.Series.Name} | Point Index: {hitTestInfo.PointIndex} | X: {hitTestInfo.X} | Y: {hitTestInfo.Y:p0}";
                    else
                        label2.Text = "";

                }
            };
            flexChart.View.GestureRecognizers.Add(tapGestureRecognizer);
        }

        #region Data
        const string DefaultMessage = "Move pointer over chart to see hit test results.";

        public string Message { get; set; } = DefaultMessage;

        static Random rnd = new();
        List<object> _data;

        public static List<object> CreateData()
        {
            var data = new List<Object>();
            for (int year = 2000; year <= 2017; year++)
            {
                var count = year - 1999;
                data.Add(new
                {
                    Year = year,
                    TV = rnd.Next(71 - count / 2, 71 + count / 2) / 100f,
                    Newspaper = rnd.Next(40 - count / 3, 40 - count / 3 + 10) / 100f,
                    Radio = rnd.Next(30 - count, 30 - count + 3) / 100f,
                    Internet = rnd.Next(count * 3, count * 3 + 10) / 100f,
                });
            }
            return data;
        }

        public List<object> Data => _data != null ? _data : _data = CreateData();

        #endregion
    }
}