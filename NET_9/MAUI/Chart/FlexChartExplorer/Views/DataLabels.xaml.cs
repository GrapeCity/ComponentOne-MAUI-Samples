using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class DataLabels : ContentPage
    {
        public DataLabels()
        {
            InitializeComponent();
        }

        public object[] Data => ClimateData.GetData();

        public LabelPosition[] Positions => new[] { LabelPosition.None, LabelPosition.Top,
            LabelPosition.Center, LabelPosition.Bottom, LabelPosition.Left, LabelPosition.Right };

        ChartType chartType = ChartType.LineSymbols;

        public ChartType ChartType
        {
            get => chartType;
            set { chartType = value; OnPropertyChanged("ChartType"); }
        }

        public ChartType[] ChartTypes => new[] 
        { 
            ChartType.Column, 
            ChartType.Line, ChartType.LineSymbols,
            ChartType.Scatter, ChartType.Area,
            ChartType.Spline, ChartType.SplineSymbols, ChartType.SplineArea,
        };

        LabelPosition position = LabelPosition.Top;

        public LabelPosition Position
        {
            get => position;
            set { position = value; OnPropertyChanged("Position"); }
        }

        void PositionChanged(object sender, EventArgs e)
        {
            if(flexChart != null)
                flexChart.DataLabel.Position = Position;
        }

        public int[] Angles => new[] { 0, 45, 90 };

        int angle = 0;

        public int Angle
        {
            get => angle;
            set { angle = value; OnPropertyChanged("Angle"); }
        }

        void AngleChanged(object sender, EventArgs e)
        {
            if (flexChart != null)
                flexChart.DataLabel.Angle = Angle;
        }
    }

    #region Data
    // The climate of New York & Los Angeles
    // https://en.wikipedia.org/wiki/Climate_of_New_York
    // https://en.wikipedia.org/wiki/Climate_of_Los_Angeles
    class ClimateData
    {
        public static object[] GetData()
        {
            return new object[]
            {
                new { month = "Jan", NewYork =  0.9, LosAngeles = 14.7, Seattle =  6.0},
                new { month = "Feb", NewYork =  2.2, LosAngeles = 15.0, Seattle =  6.7},
                new { month = "Mar", NewYork =  6.0, LosAngeles = 16.2, Seattle =  8.4},
                new { month = "Apr", NewYork = 12.1, LosAngeles = 17.6, Seattle = 10.7},
                new { month = "May", NewYork = 17.3, LosAngeles = 18.8, Seattle = 14.2},
                new { month = "Jun", NewYork = 22.2, LosAngeles = 20.7, Seattle = 16.7},
            };
        }
    }
    #endregion

}