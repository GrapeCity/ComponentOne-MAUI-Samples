using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class PiePage : ContentPage
    {
        public PiePage()
        {
            InitializeComponent();

            pickerPosition.SelectedIndex = 0;
        }


        Palette palette = Palette.Standard;

        public Palette Palette
        {
            get => palette;
            set { palette = value; OnPropertyChanged("Palette"); }
        }

        List<Palette> palettes;

        public List<Palette> Palettes
        {
            get
            {
                if (palettes == null)
                {
                    palettes = Enum.GetValues(typeof(Palette)).OfType<Palette>().ToList();
                    palettes.Remove(Palette.Custom);
                }

                return palettes;
            }
        }

        PieLabelPosition labelPosition = PieLabelPosition.None;

        public PieLabelPosition LabelPosition
        {
            get => labelPosition;
            set { labelPosition = value; OnPropertyChanged("LabelPosition"); }
        }


        public PieLabelPosition[] LabelPositions => new[] { 
            PieLabelPosition.None, PieLabelPosition.Inside, PieLabelPosition.Center, PieLabelPosition.Outside};

        double innerRadius = 0;

        public double InnerRadius
        {
            get => innerRadius;
            set { innerRadius = value; OnPropertyChanged("InnerRadius"); }
        }

        double offset = 0;

        public double Offset
        {
            get => offset;
            set { offset = value; OnPropertyChanged("Offset"); }
        }


        public class DataItem
        {
            public string Country { get; set; }
            public double Q1 { get; set; }
            public double Q2 { get; set; }
            public double Q3 { get; set; }
            public double Q4 { get; set; }
        }

        List<DataItem> data = CreateData();

        public List<DataItem> Data => data;

        public static List<DataItem> CreateData()
        {
            var countries = new string[] { "US", "China", "UK", "Japan" };
            var count = countries.Length;
            var result = new List<DataItem>();
            var rnd = new Random();
            for (var i = 0; i < count; i++)
                result.Add(new DataItem()
                {
                    Country = countries[i],
                    Q1 = rnd.Next(20),
                    Q2 = rnd.Next(20),
                    Q3 = rnd.Next(20),
                    Q4 = rnd.Next(20)
                });
            return result;
        }
    }
}