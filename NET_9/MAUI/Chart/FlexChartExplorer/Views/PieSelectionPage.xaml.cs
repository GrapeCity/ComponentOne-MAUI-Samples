using C1.Chart;
using System.ComponentModel;
using System.Globalization;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class PieSelectionPage : ContentPage
    {
        public PieSelectionPage()
        {
            InitializeComponent();
        }

        double selectionOffset = 0.0;

        public double SelectionOffset
        {
            get => selectionOffset;
            set { selectionOffset = value; OnPropertyChanged("SelectionOffset"); }
        }

        public double[] Offsets => new [] { 0, 0.1, 0.2 };

        Position selectionPosition = Position.None;

        public Position SelectionPosition
        {
            get => selectionPosition;
            set { selectionPosition = value; OnPropertyChanged("SelectionPosition"); }
        }

        public Position[] Positions => new[] { Position.None, Position.Left, Position.Top, Position.Right, Position.Bottom };

        #region Data
        List<DataItem> _data;

        public class DataItem
        {
            public string Model { get; set; }
            public int Vehicles { get; set; }
        }

        public static List<DataItem> CreateData()
        {
            return new List<DataItem>
            {
                new DataItem() { Model = "Others", Vehicles = 46292 },
                new DataItem() { Model = "Model 3", Vehicles = 26684 },
                new DataItem() { Model = "Model Y", Vehicles = 26148 },
                new DataItem() { Model = "Leaf", Vehicles = 13078 },
                new DataItem() { Model = "Model S", Vehicles = 7523 },
                new DataItem() { Model = "Bolt EV", Vehicles = 5594 },
                new DataItem() { Model = "Model X", Vehicles = 4994 },
                new DataItem() { Model = "Volt", Vehicles = 4869 },
                new DataItem() { Model = "ID.4", Vehicles = 2831 },
                new DataItem() { Model = "Niro", Vehicles = 2757 },
                new DataItem() { Model = "Prius Prime", Vehicles = 2499 },
            };
        }

        public List<DataItem> Data => _data == null ? _data = CreateData() : _data;

#endregion

    }
}