using C1.Chart;
using C1.Maui.Diagram;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class Dynamic : ContentPage
    {
        public Dynamic()
        {
            InitializeComponent();

            timer = Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(1000);

            CreateDiagram();

            Loaded += (s, e) => timer.Start();
            Unloaded += (s, e) => timer.Stop();
        }

        IDispatcherTimer timer;
        Random random = new Random();

        void CreateDiagram()
        {
            var insects = "🐝,🦋,🐞,🐜,🐛,🦗,🦟,🐌".Split(",");

            timer.Tick += (s, e) =>
            {
                diagram.BeginUpdate();

                var nodes = diagram.Nodes;
                var edges = diagram.Edges;

                if (nodes.Count >= 30)
                {
                    nodes.Clear();
                    edges.Clear();
                }

                var text = insects[random.Next(0, insects.Length)].Trim();
                nodes.Add(new Node() { Title = text, LegendItem = text, Shape = C1.Diagram.Shape.Circle });

                var i = random.Next(0, nodes.Count - 1);
                if (i != nodes.Count - 1)
                    edges.Add(new Edge() { Source = nodes[i], Target = nodes[nodes.Count - 1] });
                i = random.Next(0, nodes.Count - 1);
                if (i != nodes.Count - 1)
                    edges.Add(new Edge() { Source = nodes[i], Target = nodes[nodes.Count - 1] });

                diagram.EndUpdate();
            };

            //diagram.PointerReleased += (s, e) =>
            //{
            //    if (timer.IsEnabled)
            //        timer.Stop();
            //    else
            //        timer.Start();
            //};
        }

        public FlexDiagram Diagram => diagram;
    }
}