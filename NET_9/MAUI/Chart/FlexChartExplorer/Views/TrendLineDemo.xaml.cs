using C1.Chart;
using System.ComponentModel;
using System.Diagnostics;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class TrendLineDemo : ContentPage
    {
        PanGestureRecognizer panGestureRecognizer;

        public TrendLineDemo()
        {
            InitializeComponent();

            flexChart.Loaded += (s, e) =>
            {
                if (panGestureRecognizer == null)
                {
                    panGestureRecognizer = new ();
                    panGestureRecognizer.PanUpdated += PanUpdated;
                    flexChart.View.GestureRecognizers.Add(panGestureRecognizer);
                }
            };
        }

        #region Data

        DataItem? clickedItem;
        List<DataItem>? data;
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

        public class DataItem : INotifyPropertyChanged
        {
            double x;
            double y;

            public double X
            {
                get => x;
                set
                {
                    if (x != value)
                    {
                        x = value;
                        OnPropertyChanged("X");
                    }
                }
            }

            public double Y
            {
                get => y;
                set
                {
                    if (y != value)
                    {
                        y = value;
                        OnPropertyChanged("Y");
                    }
                }
            }

            public event PropertyChangedEventHandler? PropertyChanged;

            protected virtual void OnPropertyChanged(string propertyName)
            {
                var handler = PropertyChanged;
                if (handler != null)
                    handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public List<DataItem> Data
        {
            get
            {
                if (data == null)
                {
                    data = new List<DataItem>();
                    for (var i = 1; i <= 8; i++)
                        data.Add(new DataItem() { X = i, Y = rnd.Next(100) });
                }

                return data;
            }
        }

        #endregion


        double startY = 0;

        private void PanUpdated(object? sender, PanUpdatedEventArgs e)
        {
            if (flexChart.SelectedIndex == -1)
                return;

            // drag selected point
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    startY = flexChart.AxisY.Convert(Data[flexChart.SelectedIndex].Y);
                    break;
                case GestureStatus.Running:
                    Data[flexChart.SelectedIndex].Y = flexChart.AxisY.ConvertBack(startY+e.TotalY);
                    break;
                case GestureStatus.Completed:
                    break;
            }
        }
    }
}