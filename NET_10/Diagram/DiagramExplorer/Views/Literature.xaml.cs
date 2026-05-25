using C1.Chart;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class Literature : ContentPage
    {
        public Literature()
        {
            InitializeComponent();

            Common.Samples.LoadDiagramFromResource(diagram, "literature.mermaid");
        }
    }
}