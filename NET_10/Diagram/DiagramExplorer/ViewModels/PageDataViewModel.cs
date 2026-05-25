namespace DiagramExplorer
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

        static PageDataViewModel()
        {
            All = new List<SampleGroup>
            {
                new SampleGroup(
                    "Basics",
                    new List<PageDataViewModel> {
                        new PageDataViewModel(typeof(Intro), "➡️ Introduction", "Flowchart fully defined in XAML"),
                    }),
                    new SampleGroup(
                       "📁 Unbound",
                        new List<PageDataViewModel> {
                            new PageDataViewModel(typeof(FlexChartFamily), "📈 FlexChart Family",
                                      "Shows diagram of FlexChart visualization components."),
                            new PageDataViewModel(typeof(Dynamic), "⏩ Dynamic Graph",
                                      "Dynamic diagram generation with adaptive layout. Click on the diagram to pause / start generation."),
                            new PageDataViewModel(typeof(CustomShape), "💧 Custom Shape",
                                      "FlexDiagram with custom shape node."),
                    }),
                    new SampleGroup("📁 Data Binding",
                        new List<PageDataViewModel> 
                        {
                            new PageDataViewModel(typeof(Animals), "🦁 Animals",
                                      "Animal classification bound to flat data collection."),
                            new PageDataViewModel(typeof(OrgChart), "👥 OrgChart",
                                      "Organizational chart created with hierarchical data."),
                            new PageDataViewModel(typeof(NodeTemplate), "📑 Node Template",
                                      "Shows diagram with custom XAML template."),
                        }),
                    new SampleGroup("🖱️ Interaction",
                        new List<PageDataViewModel>
                        {
                            new PageDataViewModel(typeof(HitTesting), "🎯 HitTesting",
                                      "Tap over node to highlight."),
                            new PageDataViewModel(typeof(Selection), "✔️ Selection",
                                      "FlexDiagram support node selection with customizable selection style. Tap on the node to select."),
                            new PageDataViewModel(typeof(Tooltips), "💬 Tooltips",
                                      "You can specify node and edge tooltips when creating FlexDiagram."),
                            new PageDataViewModel(typeof(Collapsible), "➕ Collapsible",
                                      "FlexDiagram with collapsible nodes. Click on the node to collapse or expand."),
                        }),
                    new SampleGroup(
                       "📁 Usage Scenarios",
                        new List<PageDataViewModel> {
                            new PageDataViewModel(typeof(DecisionTree), "🌳 Decision Tree",
                                      "Shows sample decision tree diagram. The diagram is loaded from Mermaid.js graph."),
                            new PageDataViewModel(typeof(Literature), "📚 Literature",
                                      "Shows literature genre tree. The diagram is loaded from Mermaid.js graph."),
                            new PageDataViewModel(typeof(FamilyTree), "👪 Family Tree",
                                      "Shows relations between main characters of Games of Thrones. The diagram is loaded from Mermaid.js graph."),
                            new PageDataViewModel(typeof(ProgrammingLanguages), "💻 Programming Languages",
                                      "Shows relationship and history of programming languages. The diagram is loaded from Mermaid.js graph."),
                    })
            };
        }

        public static IList<SampleGroup> All { private set; get; }
    }
}
