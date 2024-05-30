using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class PanZoomPage : ContentPage
    {
        static Random rnd = new Random();
        int cnt = 1000;
        string[] coef = new string[]
        {
          "AMTMNQQXUYGA",
          "CVQKGHQTPHTE",
          "FIRCDERRPVLD",
          "GIIETPIQRRUL",
          "GLXOESFTTPSV",
          "GXQSNSKEECTX",
        };

        public PanZoomPage()
        {
            InitializeComponent();
            Loaded += (s, e) => CreateData();
            btnNew.Clicked += (s, e) =>
            {
                zoomBehavior.Reset();
                CreateData();
            };
            btnReset.Clicked += (s, e) =>
            {
                zoomBehavior.Reset();
            };
        }

        void CreateData()
        {
            flexChart.BeginUpdate();
            flexChart.ItemsSource = DataSource.Create(coef, cnt);
            flexChart.Palette = (Palette)rnd.Next(0, 10);
            flexChart.AxisX.Min = flexChart.AxisX.Max = double.NaN;
            flexChart.AxisY.Min = flexChart.AxisY.Max = double.NaN;
            flexChart.EndUpdate();
        }

        class DataSource
        {
            static Random rnd = new Random();

            public static Point[] Create(string[] coef, int count)
            {
                var points = new Point[count];

                double[] c = StringToCoeff(coef[rnd.Next(coef.Length)]);

                for (int i = 1; i < count; i++)
                    points[i] = Iterate(points[i - 1].X, points[i - 1].Y, c);

                return points;
            }

            static Point Iterate(double x, double y, double[] c)
            {
                double x1 = c[0] + c[1] * x + c[2] * x * x + c[3] * x * y + c[4] * y + c[5] * y * y;
                double y1 = c[6] + c[7] * x + c[8] * x * x + c[9] * x * y + c[10] * y + c[11] * y * y;

                return new Point() { X = x1, Y = y1 };
            }

            static double[] StringToCoeff(string s)
            {
                int len = s.Length;
                double[] c = new double[len];

                for (int i = 0; i < len; i++)
                {
                    c[i] = -1.2 + 0.1 * (s[i] - 'A');
                }

                return c;
            }

        }

    }
}