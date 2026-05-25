using C1.Chart;
using C1.Diagram;
using C1.Maui.Diagram;
using DiagramExplorer.Common;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class FlexChartFamily : ContentPage
    {
        public FlexChartFamily()
        {
            InitializeComponent();

            CreateFlexChartFamily(diagram);
        }
        public FlexDiagram Diagram => diagram;

        public static void CreateFlexChartFamily(FlexDiagram diagram)
        {
            diagram.BeginUpdate();

            diagram.FontSize = 12;

            var nodes = diagram.Nodes;
            var edges = diagram.Edges;

            nodes.Add(new Node() { Text = "📈 FlexChart", Content = "6 Charting Components" });// 0
            nodes.Add(new Node() { Text = "📋 Table-based Data" }); // 1
            nodes.Add(new Node() { Appearance = NodeAppearance.Hidden });//
            nodes.Add(new Node() { Text = "🌳 Hierarchical Data" }); // 2

            nodes.Add(new Node() { Text = "📈 FlexChart" });//3
            nodes.Add(new Node() { Text = "🥧 FlexPie" });// 4
            nodes.Add(new Node() { Text = "📡 FlexRadar" }); // 5

            nodes.Add(new Node() { Text = "🧱 TreeMap" }); // 6
            nodes.Add(new Node() { Text = "💥 Sunburst" });// 7
            nodes.Add(new Node() { Text = "🔀 FlexDiagram" });// 7

            edges.Add(new Edge() { Source = nodes[0], Target = nodes[1] });
            edges.Add(new Edge() { Source = nodes[0], Target = nodes[2] });
            edges.Add(new Edge() { Source = nodes[0], Target = nodes[3] });

            edges.Add(new Edge() { Source = nodes[1], Target = nodes[4] });
            edges.Add(new Edge() { Source = nodes[1], Target = nodes[5] });
            edges.Add(new Edge() { Source = nodes[1], Target = nodes[6] });

            edges.Add(new Edge() { Source = nodes[3], Target = nodes[7] });
            edges.Add(new Edge() { Source = nodes[3], Target = nodes[8] });
            edges.Add(new Edge() { Source = nodes[3], Target = nodes[9] });

            foreach (var node in nodes)
            {
                node.TitleDirection = Direction.Vertical;
                node.TitleOrder = LabelOrder.TextImage;
                node.Shape = Shape.RoundedRectangle;
            }
            diagram.ScaleMode = ScaleMode.ScaleToFit;

            diagram.EndUpdate();
        }
    }
}