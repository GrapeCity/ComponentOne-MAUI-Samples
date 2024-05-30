using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class TrendLineDemo : ContentPage
    {
        public TrendLineDemo()
        {
            InitializeComponent();
        }

        #region Data

        List<Point> _data;
        Random rnd = new();

        public FitType[] FitTypes => new FitType[] {
            FitType.Linear, FitType.Polynom, FitType.Logarithmic,
            FitType.Exponent, FitType.Power, FitType.Fourier
        };

        FitType fitType = FitType.Linear;

        public FitType FitType
        {
            get => fitType;
            set { fitType = value; OnPropertyChanged(); }
        }

        public int[] Orders => new int[] { 1, 2, 3, 4, 5, 6, 7 };

        int order = 2;
        public int Order
        {
            get => order;
            set { order = value; OnPropertyChanged(); }
        }

        public List<Point> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<Point>();
                    for (var i = 1; i <= 8; i++)
                        _data.Add(new Point(i, rnd.Next(100)));
                }

                return _data;
            }
        }

        #endregion

    }
}