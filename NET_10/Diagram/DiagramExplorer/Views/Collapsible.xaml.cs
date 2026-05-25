using C1.Chart;
using C1.Diagram;
using C1.Maui.Diagram;
using DiagramExplorer.Common;
using DiagramExplorer.Data;
using Microsoft.Maui.Controls.PlatformConfiguration;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class Collapsible : ContentPage
    {
        public Collapsible()
        {
            InitializeComponent();

            diagram.NodeCreated += (s, args) =>
            {
                var node = args.Node;
                var country = args.Data as Country;
                node.Shape = Shape.RoundedRectangle;

                if (country != null && country.Flag != null)
                {
                    node.Tooltip = country.Name;
                    node.TitleImage = country.Flag;
                    node.TitleImageAlignment = ContentAlignment.MiddleCenter;
                    node.TitleImageSize = new Size(16, 16);
                }

                if (country != null)
                {
                    args.ParentNode.LegendItem = node.LegendItem = country.Continent;
                    // hide low-level nodes
                    args.ParentNode.Appearance = node.Appearance = NodeAppearance.None;
                }
                else
                {
                    var text = args.Data.ToString();
                    node.Content = text == "World" ? "➖" : "➕";
                    node.LegendItem = text;
                }
            };

            diagram.BeginUpdate();
            diagram.ItemsSource = DataService.GetService().GetCountryData();
            diagram.Binding = "World,Continent,Region,Code";
            diagram.EndUpdate();

            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += (s, e) =>
            {
                var point = e.GetPosition(diagram);
                if (point == null)
                    return;

                var info = diagram.HitTest(point.Value.X, point.Value.Y);

                if (info != null && info.Distance == 0 && info.Element is INode node)
                {
                    diagram.BeginUpdate();
                    ToggleNodeState(node);
                    diagram.EndUpdate();
                }
            };
            diagram.View.GestureRecognizers.Add(tapGestureRecognizer);
        }

        public FlexDiagram Diagram => diagram;

        // expand or collapse the node
        static void ToggleNodeState(INode node)
        {
            var childs = GetChilds(node);

            if (childs.Count > 0)
            {
                var collapsed = childs[0].Appearance != NodeAppearance.Visible;

                if (collapsed)
                {
                    foreach (var child in childs)
                        child.Appearance = NodeAppearance.Visible;
                }
                else
                {
                    SetChildAppearance(node, NodeAppearance.None,
                        (n) =>
                        {
                            if (n.Content.Text == "➖")
                                n.Content.Text = "➕";
                        }
                    );

                    // the first child is hidden to keep node on the same layout layer
                    childs[0].Appearance = NodeAppearance.Hidden;
                }

                node.Content.Text = collapsed ? "➖" : "➕";
            }
        }

        static void SetChildAppearance(INode node, NodeAppearance appearance, Action<INode> action = null)
        {
            var childNodes = GetChilds(node);

            foreach (var child in childNodes)
            {
                child.Appearance = appearance;
                SetChildAppearance(child, appearance, action);
                action?.Invoke(child);
            }
        }

        static List<INode> GetChilds(INode node)
        {
            var list = new List<INode>();
            var diagram = node.Diagram;
            var edges = diagram.GetEdges();

            foreach (var edge in edges)
            {
                if (edge.Source == node && edge.Target != null)
                    list.Add(edge.Target);
            }

            return list;
        }
    }
}