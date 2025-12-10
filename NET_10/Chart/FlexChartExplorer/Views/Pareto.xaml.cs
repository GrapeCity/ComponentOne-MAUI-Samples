using C1.Chart;
using C1.Maui.Chart;
using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class Pareto : ContentPage
    {
        public Pareto()
        {
            this.InitializeComponent();
            Init();
        }

        private void Init()
        {
            flexChart.BeginUpdate();

            //Pareto Line
            var histogramYs = histogramSeries.GetValues(0);
            var sum = histogramYs.Sum();
            double accumulatedValue = 0;
            var lineData = new List<Point>();
            for (int i = 0; i < histogramYs.Length; i++)
            {
                accumulatedValue += histogramYs[i];
                lineData.Add(new Point
                {
                    X = i,
                    Y = accumulatedValue / sum
                });
            }

            var paretoLine = new Series()
            {
                SeriesName = "Cumulative %",
                ItemsSource = lineData,
                Binding = "Y",
                BindingX = "X",
                ChartType = ChartType.LineSymbols,
                AxisY = new Axis()
                {
                    Position = Position.Right,
                    Max = 1,
                    Min = 0,
                    Format = "P0",
                    Title = "Cumulative Percentage"
                }
            };
            paretoLine.Tooltip = "Cumulative %: {value:P0}";
            paretoLine.DataLabel = new DataLabel() { Content = "{value:P0}", Position = LabelPosition.Top };

            var stroke = new SolidColorBrush(Color.FromRgb( 192, 80, 77));
            var fill = new SolidColorBrush(Color.FromRgb( 192, 80, 77));
            paretoLine.Style = new ChartStyle() { StrokeThickness = 3, Stroke = stroke };
            paretoLine.SymbolStyle = new ChartStyle() { StrokeThickness = 3, Stroke = stroke, Fill = fill };

            flexChart.Series.Add(paretoLine);

            flexChart.EndUpdate();
        }

        List<object> data;

        public List<object> Data
        {
            get
            {
                if (data == null)
                {
                    data = new List<object>();
                    data.AddRange(new[] {  new { Name="Brand",Value=144},new { Name="Quality",Value=136},new { Name="New Product",Value=54},
                                   new { Name="Service",Value=58},new { Name="Price",Value=100},new { Name="Easy Returns",Value=29},
                                   new { Name="Quality",Value=139},new { Name="Brand",Value=123},new { Name="Easy Returns",Value=27},
                                   new { Name="Price",Value=133},new { Name="Easy Returns",Value=24},new { Name="Quality",Value=114},
                                   new { Name="Reviews",Value=73},new { Name="Quality",Value=119},new { Name="Service",Value=62},
                                   new { Name="New Product",Value=67},new { Name="Brand",Value=116},new { Name="Reviews",Value=79},
                                   new { Name="Price",Value=128},new { Name="Price",Value=125},new { Name="New Product",Value=69},
                                   new { Name="New Product",Value=65},new { Name="Price",Value=140},new { Name="Easy Returns",Value=28},
                                   new { Name="Brand",Value=146},new { Name="Service",Value=54},new { Name="Price",Value=100},
                                   new { Name="Price",Value=145},new { Name="Service",Value=69},new { Name="Quality",Value=118},
                                   new { Name="Easy Returns",Value=22},new { Name="New Product",Value=59},new { Name="Quality",Value=103},
                                   new { Name="Service",Value=66},new { Name="Brand",Value=123},new { Name="Brand",Value=117},
                                   new { Name="Service",Value=59},new { Name="Easy Returns",Value=29},new { Name="Service",Value=58},
                                   new { Name="Quality",Value=133},new { Name="Reviews",Value=73},new { Name="Price",Value=132},
                                   new { Name="Price",Value=125},new { Name="New Product",Value=57},new { Name="Quality",Value=128},
                                   new { Name="Quality",Value=107},new { Name="Brand",Value=137},new { Name="Reviews",Value=74},
                                   new { Name="Quality",Value=117},new { Name="Reviews",Value=70},}
                    );
                }
                return data;
            }
        }
    }
}