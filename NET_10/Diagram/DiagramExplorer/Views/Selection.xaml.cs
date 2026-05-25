using C1.Chart;
using C1.Maui.Diagram;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class Selection : ContentPage
    {
        public Selection()
        {
            InitializeComponent();

            var node = new Node() { Title = "1.1", Shape = C1.Diagram.Shape.Circle };
            diagram.Nodes.Add(node);
            CreateSubNodes(diagram, node, 0, () => 3, 1);
        }

        public FlexDiagram Diagram => diagram;

        public static void CreateSubNodes(FlexDiagram diagram, Node node, int level, Func<int> n, int maxLevel = 2, C1.Diagram.Shape shape = C1.Diagram.Shape.Circle)
        {
            for (var i = 0; i < n(); i++)
            {
                var subNode = new Node() { Title = $"{level + 2}.{i + 1}", Shape = shape };
                diagram.Nodes.Add(subNode);
                diagram.Edges.Add(new Edge() { Source = node, Target = subNode });

                if (level < maxLevel)
                    CreateSubNodes(diagram, subNode, level + 1, n, maxLevel, shape);
            }
        }
    }
}