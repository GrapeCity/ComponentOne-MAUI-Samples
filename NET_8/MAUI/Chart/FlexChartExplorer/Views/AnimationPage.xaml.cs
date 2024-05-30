using C1.Chart;
using C1.Maui.Chart;
using System.Collections.ObjectModel;

namespace FlexChartExplorer
{
    public partial class AnimationPage : ContentPage
    {
        public AnimationPage()
        {
            InitializeComponent();
        }

        ChartType[] _chartTypes;
        IDataSource data;

        void ChartLoaded(object sender, EventArgs args)
        {
            data = new PointDataSource(chart);
            data.NewData();
        }

        void NewData(object sender, EventArgs args)
        {
            data?.NewData();
        }

        void UpdateData(object sender, EventArgs args)
        {
            data?.UpdateData();
        }
        void AddSeries(object sender, EventArgs args)
        {
            data?.AddSeries();
        }

        void RemoveSeries(object sender, EventArgs args)
        {
            var cnt = chart.Series.Count;
            if (cnt > 0)
                chart.Series.RemoveAt(cnt - 1);
        }

        void AddPoint(object sender, EventArgs args)
        {
            data?.AddPoint();
        }

        void RemovePoint(object sender, EventArgs args)
        {
            data?.RemovePoint();
        }

        public ChartType[] ChartTypes
        {
            get
            {
                if (_chartTypes == null)
                {
                    _chartTypes = new[] { ChartType.Column, ChartType.Bar, ChartType.Line, ChartType.LineSymbols,
                        ChartType.Scatter, ChartType.Area,
                        ChartType.Spline, ChartType.SplineSymbols, ChartType.SplineArea,
                        ChartType.Step, ChartType.StepSymbols, ChartType.StepArea
                    };
                }

                return _chartTypes;
            }
        }

        #region Data

        interface IDataSource
        {
            void NewData();
            void UpdateData();

            void AddSeries();

            void AddPoint();
            void RemovePoint();
        }

        class PointDataSource : IDataSource
        {
            protected static Random rnd = new Random();
            protected FlexChart chart;
            protected int nser;
            protected int npts;

            public PointDataSource(FlexChart chart, int nser = 3, int npts = 6)
            {
                this.chart = chart;
                this.nser = nser;
                this.npts = npts;
            }

            public void NewData()
            {
                chart.BeginUpdate();
                chart.AxisX.LabelAngle = 0;
                chart.ItemsSource = null;
                chart.Series.Clear();
                for (var i = 0; i < nser; i++)
                    chart.Series.Add(CreateSeries(npts, i.ToString()));
                chart.EndUpdate();
            }

            public void UpdateData()
            {
                var nser = chart.Series.Count;
                for (var i = 0; i < nser; i++)
                    UpdateSeries(chart.Series[i], npts);
            }

            public void AddSeries()
            {
                chart.Series.Add(CreateSeries(npts, chart.Series.Count.ToString()));
            }

            public void AddPoint()
            {
                var max = (1 + (int)(rnd.NextDouble() * 5)) * 100;
                foreach (var ser in chart.Series)
                {
                    var col = (ObservableCollection<Point>)ser.ItemsSource;
                    col.Add(new Point(col.Count, (float)rnd.NextDouble() * max));
                }
            }

            public void RemovePoint()
            {
                foreach (var ser in chart.Series)
                {
                    var col = (ObservableCollection<Point>)ser.ItemsSource;
                    if (col.Count > 0)
                        col.RemoveAt(col.Count - 1);
                }
            }

            public Series CreateSeries(int npts, string name)
            {
                var max = (1 + (int)(rnd.NextDouble() * 5)) * 100;

                return new Series()
                {
                    Binding = "Y",
                    BindingX = "X",
                    ItemsSource = Create(npts, max),
                    SeriesName = name
                };
            }

            public void UpdateSeries(Series ser, int npts)
            {
                var max = (1 + (int)(rnd.NextDouble() * 5)) * 100;
                ser.ItemsSource = Create(npts, max);
            }

            ObservableCollection<Point> Create(int npts, float max = 100)
            {
                var pts = new Point[npts];

                for (var i = 0; i < npts; i++)
                    pts[i] = new Point(i, (int)(rnd.NextDouble() * max));

                return new ObservableCollection<Point>(pts);
            }
        }

        #endregion
    }

}