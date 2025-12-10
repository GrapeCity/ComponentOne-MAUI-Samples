using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class Legend : ContentPage
    {
        public Legend()
        {
            InitializeComponent();
        }

        #region Data

        public object[] Data => ClimateData.GetData();

        Position position = Position.Bottom;

        public Position Position
        {
            get => position;
            set { position = value; OnPropertyChanged(); }
        }

        public Position[] Positions => new[]
            {
                Position.Left, Position.LeftTop, Position.LeftBottom,
                Position.Right, Position.RightTop, Position.RightBottom,
                Position.Top, Position.TopLeft, Position.TopRight,
                Position.Bottom, Position.BottomLeft, Position.BottomRight
            };

        Orientation orientation = Orientation.Auto;

        public Orientation Orientation
        {
            get => orientation;
            set { orientation = value; OnPropertyChanged(); }
        }

        public C1.Chart.Orientation[] Orientations => new[] {
            C1.Chart.Orientation.Auto, C1.Chart.Orientation.Horizontal, C1.Chart.Orientation.Vertical };

        #endregion

    }
}