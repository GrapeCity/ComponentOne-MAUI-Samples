using C1.Chart;
using C1.Maui.Chart;
using C1.Maui.Diagram;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class HitTesting : ContentPage
    {
        public HitTesting()
        {
            InitializeComponent();

            CreateRandomPersonDiagram(diagram);

            var nodeStyle = new ChartStyle();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                var point = e.GetPosition(diagram);
                if (point == null)
                    return;

                var info = diagram.HitTest(point.Value.X, point.Value.Y);

                if (info?.Distance <= 3)
                {
                    diagram.BeginUpdate();

                    foreach (var node in diagram.Nodes)
                        node.NodeStyle = nodeStyle;

                    if (info?.Element is Node node1)
                        node1.NodeStyle = new ChartStyle() { StrokeThickness = 4 };

                    diagram.EndUpdate();
                }
            };
            diagram.View.GestureRecognizers.Add(tapGestureRecognizer);
        }

        public FlexDiagram Diagram => diagram;

        static Random random = new Random();

        static void CreateRandomPersonDiagram(FlexDiagram diagram)
        {
            var nodes = diagram.Nodes;
            var edges = diagram.Edges;

            diagram.BeginUpdate();

            nodes.Clear();
            edges.Clear();

            var persons = "👨,👩,👦,👧".Split(",");

            for (var i = 0; i < 10; i++)
            {
                var text = persons[random.Next(0, persons.Length)];
                var node = new Node() { Title = text, LegendItem = text, Shape = C1.Diagram.Shape.RoundedRectangle };
                nodes.Add(node);

                if (i == 0)
                    continue;

                var k = random.Next(0, nodes.Count - 2);
                edges.Add(new Edge() { Source = nodes[k], Target = nodes[nodes.Count - 1] });
                k = random.Next(0, nodes.Count - 1);
                edges.Add(new Edge() { Source = nodes[k], Target = nodes[nodes.Count - 1] });

                diagram.EndUpdate();
            }
        }
    }
}