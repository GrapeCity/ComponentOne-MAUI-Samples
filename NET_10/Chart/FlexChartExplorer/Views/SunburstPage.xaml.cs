using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class SunburstPage : ContentPage
    {
        public SunburstPage()
        {
            InitializeComponent();
        }


        Palette palette = Palette.Standard;

        public Palette Palette
        {
            get => palette;
            set { palette = value; OnPropertyChanged("Palette"); }
        }

        List<Palette> _palettes;

        public List<Palette> Palettes
        {
            get
            {
                if (_palettes == null)
                {
                    _palettes = Enum.GetValues(typeof(Palette)).OfType<Palette>().ToList();
                    _palettes.Remove(Palette.Custom);
                }

                return _palettes;
            }
        }

        SalesDataItem[] _data;

        public SalesDataItem[] Data => _data == null ? _data = DataSource.CreateHierarchicalData() : _data;

        public class SalesDataItem
        {
            public string type { get; set; }
            public double sales { get; set; }

            public SalesDataItem[] items { get; set; }
        }

        class DataSource
        {
            public static SalesDataItem[] CreateHierarchicalData()
            {
                var data = new SalesDataItem[] {
                    new SalesDataItem {
                        type = "Electronics",
                        items = new SalesDataItem[] {
                            new SalesDataItem{
                                type = "Camera",
                                items = new SalesDataItem[]
                                {
                                        new SalesDataItem{ type = "Digital", sales = rand() },
                                        new SalesDataItem{ type = "Film", sales = rand() },
                                        new SalesDataItem{ type = "Lenses", sales = rand() },
                                        new SalesDataItem{ type = "Video",  sales = rand() },
                                        new SalesDataItem{ type = "Accessories", sales = rand() }
                                }
                            },
                            new SalesDataItem{
                                type = "Headphones",
                                items = new SalesDataItem[]
                                {
                                        new SalesDataItem{ type = "Earbud", sales = rand() },
                                        new SalesDataItem{ type = "Over-ear", sales = rand() },
                                        new SalesDataItem{ type = "On-ear", sales = rand() },
                                        new SalesDataItem{ type = "Bluetooth", sales = rand() },
                                        new SalesDataItem{ type = "Noise-cancelling", sales = rand() },
                                        new SalesDataItem{ type = "Audiophile", sales = rand() }
                                }
                            }
                        }
                    },
                    new SalesDataItem{
                        type = "Computers\n& Tablets",
                        items = new SalesDataItem[]
                        {
                            new SalesDataItem
                            {
                                type = "Desktops",
                                items = new SalesDataItem[]
                                {
                                    new SalesDataItem{ type = "All-in-ones", sales = rand() },
                                    new SalesDataItem{ type = "Minis", sales = rand() },
                                    new SalesDataItem{ type = "Towers", sales = rand() }
                                }
                            },
                            new SalesDataItem
                            {
                                type = "Laptops",
                                items = new SalesDataItem[]
                                {
                                    new SalesDataItem{ type = "2 in 1", sales = rand() },
                                    new SalesDataItem{ type = "Traditional", sales = rand() }
                                }
                            },
                            new SalesDataItem
                            {
                                type = "Tablets",
                                items = new SalesDataItem[]
                                {
                                    new SalesDataItem{ type = "iOS", sales = rand() },
                                    new SalesDataItem{ type = "Android", sales = rand() },
                                    new SalesDataItem{ type = "Fire os", sales = rand() },
                                    new SalesDataItem{ type = "Windows", sales = rand() }
                                }
                            }
                        }
                    }
                };
                return data;
            }

            static Random rnd = new Random();
            static int rand() => rnd.Next(10, 100);
        }

    }
}