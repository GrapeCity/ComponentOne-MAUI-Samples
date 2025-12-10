using System;
using System.Collections.Generic;
using System.Linq;
using C1.Chart;
using System.Globalization;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class ContourPage : ContentPage
    {
        public ContourPage()
        {
            this.InitializeComponent();
        }

        private void ChartLoaded(object sender, EventArgs e)
        {
            if (chart.Series.Count > 0)
                return;

            chart.BeginUpdate();
            
            var contour = new C1.Maui.Chart.Contour();
            var data = MonkeySaddleData();
            var scale = new C1.Maui.Chart.GradientColorScale() { Min = 0, Max = 100 };
            scale.Colors = C1.Maui.Chart.Palettes.Diverging.RdBu.Select(b => ((SolidColorBrush)b).Color).Reverse().ToList();

            contour.ItemsSource = data;
            contour.ColorScale = scale;

            //contour.Levels = new double[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20 };
            chart.AxisX.Max = data.GetLength(0) - 1;
            chart.AxisY.Max = data.GetLength(1) - 1;
            chart.Series.Add(contour);

            chart.EndUpdate();
        }

        private static double[,] MonkeySaddleData()
        {
            int width = 20;
            int height = 20;
            double[,] data = new double[width, height];
            double minValue = 0;
            double maxValue = 0;
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    // Center the coordinates
                    double xc = x - width / 2.0;
                    double yc = y - height / 2.0;

                    // Monkey saddle equation: z = x³ - 3xy²
                    data[x, y] = (xc * xc * xc - 3 * xc * yc * yc);

                    // set initial min and max value, can't be 0.
                    if (x == 0 && y == 0)
                    {
                        maxValue = data[0, 0];
                        minValue = data[0, 0];
                    }

                    // Track min and max values
                    minValue = Math.Min(minValue, data[x, y]);
                    maxValue = Math.Max(maxValue, data[x, y]);
                }
            }

            // normalize to range [0,100] 
            for (int x = 0; x < width; x++)
                for (int y = 0; y < height; y++)
                    data[x, y] = 100 * (data[x, y] - minValue) / (maxValue - minValue);

            return data;
        }
    }
}