namespace FlexChartExplorer
{
#pragma warning disable 1591

    public class SampleGroup : List<PageDataViewModel>
    {
        public string Name { get; private set; }

        public SampleGroup(string name, List<PageDataViewModel> pages) : base(pages)
        {
            Name = name;
        }
    }

    public class PageDataViewModel
    {
        public PageDataViewModel(Type type, string title, string description)
        {
            Type = type;
            Title = title;
            Description = description;
        }

        public Type Type { private set; get; }

        public string Title { private set; get; }

        public string Description { private set; get; }

        public string Image { private set; get; } = "bar_chart";

        public string ImageSource => Application.Current.RequestedTheme == AppTheme.Dark ? $"{Image}_dark.png" : $"{Image}.png";

        static PageDataViewModel()
        {
            All = new List<SampleGroup>
            {
                new SampleGroup(
                    "Basics",
                    new List<PageDataViewModel> {
                        new PageDataViewModel(typeof(IntroPage), "Intro",
                                      "Show FlexChart"),
                        new PageDataViewModel(typeof(PiePage), "Pie",
                                      "Show Pie Chart") { Image = "pie_chart" },
                        new PageDataViewModel(typeof(BindingPage), "Binding",
                                      "Show Binding Demo") { Image = "line_chart" },
                        new PageDataViewModel(typeof(SeriesBindingPage), "Series Binding",
                                      "Show Series Binding Demo") { Image = "line_chart" },
                    }),
                    new SampleGroup(
                       "Special Charts",
                        new List<PageDataViewModel> {
                            new PageDataViewModel(typeof(TreeMapPage), "TreeMap",
                                      "Show TreeMap Chart"){ Image = "treemap" },
                            new PageDataViewModel(typeof(RadarPage), "Radar",
                                      "Show Radar Chart") { Image = "radar_chart" },
                            new PageDataViewModel(typeof(FinancialPage), "Financial",
                                      "Show Financial Chart"){ Image = "candlestick_chart" },
                            new PageDataViewModel(typeof(Funnel), "Funnel",
                                      "Show Funnel Chart") { Image = "funnel_chart" },
                            new PageDataViewModel(typeof(BubblePage), "Bubble",
                                      "Show Bubble Chart") { Image = "bubble_chart" },
                            new PageDataViewModel(typeof(SunburstPage), "Sunburst",
                                      "Show Sunburst Chart") { Image = "donut_chart"},
                            new PageDataViewModel(typeof(BoxWhiskerDemo), "Box-Whisker",
                                      "Show Box-Whisker Chart"),
                            new PageDataViewModel(typeof(BreakEvenDemo), "Break-Even",
                                      "Show Break-Even Chart"){ Image = "line_chart" },
                            new PageDataViewModel(typeof(ErrorBars), "Error Bars",
                                      "Show Error Bars Chart"),
                            new PageDataViewModel(typeof(HistogramDemo), "Histogram",
                                      "Show Histogram Chart"),
                            new PageDataViewModel(typeof(WaterfallDemo), "Waterfall",
                                      "Show Waterfall Chart") { Image = "waterfall_chart" },
                            new PageDataViewModel(typeof(HeatmapPage), "Heatmap",
                                      "Show Heatmap Chart") { Image = "treemap" },
                            new PageDataViewModel(typeof(ColumnHeatmapPage), "Column & Heatmap",
                                      "Show combination of Range Column and Heatmap Charts") { Image = "treemap" },
                    }),
                    new SampleGroup(
                       "Interaction",
                        new List<PageDataViewModel> {
                            new PageDataViewModel(typeof(SelectionPage), "Selection",
                                      "Show FlexChart selection"),
                            new PageDataViewModel(typeof(PieSelectionPage), "Pie Selection",
                                      "Show FlexPie selection"){ Image = "pie_chart" },
                            new PageDataViewModel(typeof(PanZoomPage), "Pan & Zoom",
                                      "Show Pan&Zoom interaction") { Image = "pan_zoom" },
                            new PageDataViewModel(typeof(LineMarker), "Line Marker",
                                      "Show FlexChart with LineMarker") { Image = "line_marker"},
                            new PageDataViewModel(typeof(HitTest), "Hit Test",
                                      "Show using FlexChart.HitTest() method"),
                            new PageDataViewModel(typeof(AxisMarkers), "Axis Markers",
                                      "Show how to display LineMarker's over the axes")
                        }),
                    new SampleGroup(
                       "Axes",
                        new List<PageDataViewModel> {
                            new PageDataViewModel(typeof(AxisLabelsPage), "Axis Labels",
                                      "Show Axis Label options") { Image = "axis"},
                            new PageDataViewModel(typeof(LogAxesPage), "Log Axes",
                                      "Show FlexChart with logarithmic axes"){ Image = "axis"},
                            new PageDataViewModel(typeof(TwoYAxesPage), "Two Y Axes",
                                      "Show FlexChart with Two Y Axes"){ Image = "axis"},
                            new PageDataViewModel(typeof(AxisGroupsPage), "Axis Groups",
                                      "Show Axis Groups"){ Image = "axis"},
                            new PageDataViewModel(typeof(AxisBreak), "Axis Break",
                                      "Show FlexChart with Axis Break"){ Image = "axis"}
                        }),
                    new SampleGroup(
                       "Features",
                        new List<PageDataViewModel> {
                            new PageDataViewModel(typeof(AlarmZones), "Alarm Zones",
                                      "Show Chart With Alarm Zones"),
                            new PageDataViewModel(typeof(AnimationPage), "Animation",
                                      "Show Animation"),
                            new PageDataViewModel(typeof(DataLabels), "Data Labels",
                                      "Show FlexChart with Data Labels"),
                            new PageDataViewModel(typeof(CustomDataLabels), "Custom Data Labels",
                                      "Show FlexChart with Custom Data Labels"),
                            new PageDataViewModel(typeof(ExtendedPalettes), "Extended Palettes",
                                      "Show extended chart palettes") { Image = "palette" },
                            new PageDataViewModel(typeof(Legend), "Legend",
                                      "Show chart legend options") { Image = "legend" },
                            new PageDataViewModel(typeof(MultiPiePage), "Multi Pie",
                                      "Show Multi Pie Chart") { Image = "pie_chart" },
                            new PageDataViewModel(typeof(Pareto), "Pareto Chart",
                                      "Show Pareto Chart"),
                            new PageDataViewModel(typeof(TrendLineDemo), "Trend Line",
                                      "Show FlexChart with trend line"),
                    }),
            };
        }

        public static IList<SampleGroup> All { private set; get; }
    }
}
