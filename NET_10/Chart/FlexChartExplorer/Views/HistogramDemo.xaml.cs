using C1.Chart;
using C1.Maui.Chart;
using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class HistogramDemo : ContentPage
    {
        List<Point> _data;

        public HistogramDemo()
        {
            this.InitializeComponent();
        }

        void NormalChanged(object sender, EventArgs args) => 
            histogram.NormalCurve.Visible = ((CheckBox)sender).IsChecked;

        void CumulativeChanged(object sender, EventArgs args) => 
            histogram.CumulativeMode = ((CheckBox)sender).IsChecked;

        public HistogramAppearance[] Appearances => new[]
        {
            HistogramAppearance.HistogramAndFrequencyPolygon, HistogramAppearance.Histogram, HistogramAppearance.FrequencyPolygon
        };

        HistogramAppearance appearance = HistogramAppearance.Histogram;

        public HistogramAppearance Appearance
        {
            get => appearance;
            set { appearance = value; OnPropertyChanged(); }
        }

        #region Data

        public List<Point> Data => _data != null ? _data : _data = CreateData();

        public List<Point> CreateData()
        {
            var data = new List<Point>()
            {
                new Point{X=161.2,Y= 51.6}, new Point{X=167.5,Y= 59.0}, new Point{X=159.5,Y= 49.2}, new Point{X=157.0,Y= 63.0}, new Point{X=155.8,Y= 53.6},
                new Point{X=170.0,Y= 59.0}, new Point{X=159.1,Y= 47.6}, new Point{X=166.0,Y= 69.8}, new Point{X=176.2,Y= 66.8}, new Point{X=160.2,Y= 75.2},
                new Point{X=172.5,Y= 55.2}, new Point{X=170.9,Y= 54.2}, new Point{X=172.9,Y= 62.5}, new Point{X=153.4,Y= 42.0}, new Point{X=160.0,Y= 50.0},
                new Point{X=147.2,Y= 49.8}, new Point{X=168.2,Y= 49.2}, new Point{X=175.0,Y= 73.2}, new Point{X=157.0,Y= 47.8}, new Point{X=167.6,Y= 68.8},
                new Point{X=159.5,Y= 50.6}, new Point{X=175.0,Y= 82.5}, new Point{X=166.8,Y= 57.2}, new Point{X=176.5,Y= 87.8}, new Point{X=170.2,Y= 72.8},
                new Point{X=174.0,Y= 54.5}, new Point{X=173.0,Y= 59.8}, new Point{X=179.9,Y= 67.3}, new Point{X=170.5,Y= 67.8}, new Point{X=160.0,Y= 47.0},
                new Point{X=154.4,Y= 46.2}, new Point{X=162.0,Y= 55.0}, new Point{X=176.5,Y= 83.0}, new Point{X=160.0,Y= 54.4}, new Point{X=152.0,Y= 45.8},
                new Point{X=162.1,Y= 53.6}, new Point{X=170.0,Y= 73.2}, new Point{X=160.2,Y= 52.1}, new Point{X=161.3,Y= 67.9}, new Point{X=166.4,Y= 56.6},
                new Point{X=168.9,Y= 62.3}, new Point{X=163.8,Y= 58.5}, new Point{X=167.6,Y= 54.5}, new Point{X=160.0,Y= 50.2}, new Point{X=161.3,Y= 60.3},
                new Point{X=167.6,Y= 58.3}, new Point{X=165.1,Y= 56.2}, new Point{X=160.0,Y= 50.2}, new Point{X=170.0,Y= 72.9}, new Point{X=157.5,Y= 59.8},
                new Point{X=167.6,Y= 61.0}, new Point{X=160.7,Y= 69.1}, new Point{X=163.2,Y= 55.9}, new Point{X=152.4,Y= 46.5}, new Point{X=157.5,Y= 54.3},
                new Point{X=168.3,Y= 54.8}, new Point{X=180.3,Y= 60.7}, new Point{X=165.5,Y= 60.0}, new Point{X=165.0,Y= 62.0}, new Point{X=164.5,Y= 60.3},
                new Point{X=156.0,Y= 52.7}, new Point{X=160.0,Y= 74.3}, new Point{X=163.0,Y= 62.0}, new Point{X=165.7,Y= 73.1}, new Point{X=161.0,Y= 80.0},
                new Point{X=162.0,Y= 54.7}, new Point{X=166.0,Y= 53.2}, new Point{X=174.0,Y= 75.7}, new Point{X=172.7,Y= 61.1}, new Point{X=167.6,Y= 55.7},
                new Point{X=151.1,Y= 48.7}, new Point{X=164.5,Y= 52.3}, new Point{X=163.5,Y= 50.0}, new Point{X=152.0,Y= 59.3}, new Point{X=169.0,Y= 62.5},
                new Point{X=164.0,Y= 55.7}, new Point{X=161.2,Y= 54.8}, new Point{X=155.0,Y= 45.9}, new Point{X=170.0,Y= 70.6}, new Point{X=176.2,Y= 67.2},
                new Point{X=170.0,Y= 69.4}, new Point{X=162.5,Y= 58.2}, new Point{X=170.3,Y= 64.8}, new Point{X=164.1,Y= 71.6}, new Point{X=169.5,Y= 52.8},
                new Point{X=163.2,Y= 59.8}, new Point{X=154.5,Y= 49.0}, new Point{X=159.8,Y= 50.0}, new Point{X=173.2,Y= 69.2}, new Point{X=170.0,Y= 55.9},
                new Point{X=161.4,Y= 63.4}, new Point{X=169.0,Y= 58.2}, new Point{X=166.2,Y= 58.6}, new Point{X=159.4,Y= 45.7}, new Point{X=162.5,Y= 52.2},
                new Point{X=159.0,Y= 48.6}, new Point{X=162.8,Y= 57.8}, new Point{X=159.0,Y= 55.6}, new Point{X=179.8,Y= 66.8}, new Point{X=162.9,Y= 59.4},
                new Point{X=161.0,Y= 53.6}, new Point{X=151.1,Y= 73.2}, new Point{X=168.2,Y= 53.4}, new Point{X=168.9,Y= 69.0}, new Point{X=173.2,Y= 58.4},
                new Point{X=171.8,Y= 56.2}, new Point{X=178.0,Y= 70.6}, new Point{X=164.3,Y= 59.8}, new Point{X=163.0,Y= 72.0}, new Point{X=168.5,Y= 65.2},
                new Point{X=166.8,Y= 56.6}, new Point{X=172.7,Y= 105.2}, new Point{X=163.5,Y= 51.8}, new Point{X=169.4,Y= 63.4}, new Point{X=167.8,Y= 59.0},
                new Point{X=159.5,Y= 47.6}, new Point{X=167.6,Y= 63.0}, new Point{X=161.2,Y= 55.2}, new Point{X=160.0,Y= 45.0}, new Point{X=163.2,Y= 54.0},
                new Point{X=162.2,Y= 50.2}, new Point{X=161.3,Y= 60.2}, new Point{X=149.5,Y= 44.8}, new Point{X=157.5,Y= 58.8}, new Point{X=163.2,Y= 56.4},
                new Point{X=172.7,Y= 62.0}, new Point{X=155.0,Y= 49.2}, new Point{X=156.5,Y= 67.2}, new Point{X=164.0,Y= 53.8}, new Point{X=160.9,Y= 54.4},
                new Point{X=162.8,Y= 58.0}, new Point{X=167.0,Y= 59.8}, new Point{X=160.0,Y= 54.8}, new Point{X=160.0,Y= 43.2}, new Point{X=168.9,Y= 60.5},
                new Point{X=158.2,Y= 46.4}, new Point{X=156.0,Y= 64.4}, new Point{X=160.0,Y= 48.8}, new Point{X=167.1,Y= 62.2}, new Point{X=158.0,Y= 55.5},
                new Point{X=167.6,Y= 57.8}, new Point{X=156.0,Y= 54.6}, new Point{X=162.1,Y= 59.2}, new Point{X=173.4,Y= 52.7}, new Point{X=159.8,Y= 53.2},
                new Point{X=170.5,Y= 64.5}, new Point{X=159.2,Y= 51.8}, new Point{X=157.5,Y= 56.0}, new Point{X=161.3,Y= 63.6}, new Point{X=162.6,Y= 63.2},
                new Point{X=160.0,Y= 59.5}, new Point{X=168.9,Y= 56.8}, new Point{X=165.1,Y= 64.1}, new Point{X=162.6,Y= 50.0}, new Point{X=165.1,Y= 72.3},
                new Point{X=166.4,Y= 55.0}, new Point{X=160.0,Y= 55.9}, new Point{X=152.4,Y= 60.4}, new Point{X=170.2,Y= 69.1}, new Point{X=162.6,Y= 84.5},
                new Point{X=170.2,Y= 55.9}, new Point{X=158.8,Y= 55.5}, new Point{X=172.7,Y= 69.5}, new Point{X=167.6,Y= 76.4}, new Point{X=162.6,Y= 61.4},
                new Point{X=167.6,Y= 65.9}, new Point{X=156.2,Y= 58.6}, new Point{X=175.2,Y= 66.8}, new Point{X=172.1,Y= 56.6}, new Point{X=162.6,Y= 58.6},
                new Point{X=160.0,Y= 55.9}, new Point{X=165.1,Y= 59.1}, new Point{X=182.9,Y= 81.8}, new Point{X=166.4,Y= 70.7}, new Point{X=165.1,Y= 56.8},
                new Point{X=177.8,Y= 60.0}, new Point{X=165.1,Y= 58.2}, new Point{X=175.3,Y= 72.7}, new Point{X=154.9,Y= 54.1}, new Point{X=158.8,Y= 49.1},
                new Point{X=172.7,Y= 75.9}, new Point{X=168.9,Y= 55.0}, new Point{X=161.3,Y= 57.3}, new Point{X=167.6,Y= 55.0}, new Point{X=165.1,Y= 65.5},
                new Point{X=175.3,Y= 65.5}, new Point{X=157.5,Y= 48.6}, new Point{X=163.8,Y= 58.6}, new Point{X=167.6,Y= 63.6}, new Point{X=165.1,Y= 55.2},
                new Point{X=165.1,Y= 62.7}, new Point{X=168.9,Y= 56.6}, new Point{X=162.6,Y= 53.9}, new Point{X=164.5,Y= 63.2}, new Point{X=176.5,Y= 73.6},
                new Point{X=168.9,Y= 62.0}, new Point{X=175.3,Y= 63.6}, new Point{X=159.4,Y= 53.2}, new Point{X=160.0,Y= 53.4}, new Point{X=170.2,Y= 55.0},
                new Point{X=162.6,Y= 70.5}, new Point{X=167.6,Y= 54.5}, new Point{X=162.6,Y= 54.5}, new Point{X=160.7,Y= 55.9}, new Point{X=160.0,Y= 59.0},
                new Point{X=157.5,Y= 63.6}, new Point{X=162.6,Y= 54.5}, new Point{X=152.4,Y= 47.3}, new Point{X=170.2,Y= 67.7}, new Point{X=165.1,Y= 80.9},
                new Point{X=172.7,Y= 70.5}, new Point{X=165.1,Y= 60.9}, new Point{X=170.2,Y= 63.6}, new Point{X=170.2,Y= 54.5}, new Point{X=170.2,Y= 59.1},
                new Point{X=161.3,Y= 70.5}, new Point{X=167.6,Y= 52.7}, new Point{X=167.6,Y= 62.7}, new Point{X=165.1,Y= 86.3}, new Point{X=162.6,Y= 66.4},
                new Point{X=152.4,Y= 67.3}, new Point{X=168.9,Y= 63.0}, new Point{X=170.2,Y= 73.6}, new Point{X=175.2,Y= 62.3}, new Point{X=175.2,Y= 57.7},
                new Point{X=160.0,Y= 55.4}, new Point{X=165.1,Y= 104.1}, new Point{X=174.0,Y= 55.5}, new Point{X=170.2,Y= 77.3}, new Point{X=160.0,Y= 80.5},
                new Point{X=167.6,Y= 64.5}, new Point{X=167.6,Y= 72.3}, new Point{X=167.6,Y= 61.4}, new Point{X=154.9,Y= 58.2}, new Point{X=162.6,Y= 81.8},
                new Point{X=175.3,Y= 63.6}, new Point{X=171.4,Y= 53.4}, new Point{X=157.5,Y= 54.5}, new Point{X=165.1,Y= 53.6}, new Point{X=160.0,Y= 60.0},
                new Point{X=174.0,Y= 73.6}, new Point{X=162.6,Y= 61.4}, new Point{X=174.0,Y= 55.5}, new Point{X=162.6,Y= 63.6}, new Point{X=161.3,Y= 60.9},
                new Point{X=156.2,Y= 60.0}, new Point{X=149.9,Y= 46.8}, new Point{X=169.5,Y= 57.3}, new Point{X=160.0,Y= 64.1}, new Point{X=175.3,Y= 63.6},
                new Point{X=169.5,Y= 67.3}, new Point{X=160.0,Y= 75.5}, new Point{X=172.7,Y= 68.2}, new Point{X=162.6,Y= 61.4}, new Point{X=157.5,Y= 76.8},
                new Point{X=176.5,Y= 71.8}, new Point{X=164.4,Y= 55.5}, new Point{X=160.7,Y= 48.6}, new Point{X=174.0,Y= 66.4}, new Point{X=163.8,Y= 67.3}};

            return data;
        }

        #endregion

    }
}