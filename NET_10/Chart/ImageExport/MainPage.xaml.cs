using C1.Chart;
using CommunityToolkit.Maui.Storage;
using Microsoft.Maui.Graphics.Skia;
using SkiaSharp;
using System.Diagnostics;

namespace ImageExport
{
    public partial class MainPage : ContentPage
    {
        static Random rnd = new Random();
        int cnt = 2000;
        string[] coef = new string[]
        {
          "AMTMNQQXUYGA",
          "CVQKGHQTPHTE",
          "FIRCDERRPVLD",
          "GIIETPIQRRUL",
          "GLXOESFTTPSV",
          "GXQSNSKEECTX",
        };

        public MainPage()
        {
            InitializeComponent();
            Loaded += (s, e) => CreateData();
            btnNew.Clicked += (s, e) => CreateData();
            btnSave.Clicked += (s, e) => SaveImage($"chart-{DateTime.Now:yyyyMMdd-hhmmss}.png");
            btnSaveSvg.Clicked += async (s, e) => await SaveSvg($"chart-{DateTime.Now:yyyyMMdd-hhmmss}.svg");
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

        async void SaveImage(string fileName)
        {
            var rect = new Rect(0, 0, flexChart.Width, flexChart.Height);
            var context = new SkiaBitmapExportContext((int)flexChart.Width, (int)flexChart.Height, 1.0f);

            // fill background            
            context.Canvas.FillColor = App.Current.RequestedTheme == AppTheme.Dark ? 
                Colors.Black : Colors.White;
            context.Canvas.FillRectangle(rect);

            // draw chart
            flexChart.Draw(context.Canvas, rect);

            var ms = new MemoryStream();
            context.WriteToStream(ms);

            // save image to picture library
            var photoLibrary = new PhotoLibrary();
            await photoLibrary.SavePhotoAsync(ms.ToArray(), "", fileName);
        }

        async Task SaveSvg(string fileName)
        {
            var ms = new MemoryStream();
            flexChart.SaveSvg(ms);

            var fileSaverResult = await FileSaver.Default.SaveAsync(fileName, ms);
            if (fileSaverResult.IsSuccessful)
                Debug.WriteLine($"The file was saved successfully to location: {fileSaverResult.FilePath}");
            else
                Debug.WriteLine($"The file was not saved successfully with error: {fileSaverResult.Exception.Message}");
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
