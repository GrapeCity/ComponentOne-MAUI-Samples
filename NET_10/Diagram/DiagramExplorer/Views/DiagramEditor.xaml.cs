using C1.Chart;
using C1.Diagram;
using C1.Maui.Diagram;

namespace DiagramExplorer.Views;

#pragma warning disable 1591

public partial class DiagramEditor : ContentView
{
    public DiagramEditor()
    {
        InitializeComponent();
        BindingContext = this;

        Loaded += (s, e) =>
        {
            var diagram = Diagram;
            if (diagram != null)
            {
                Direction = diagram.Direction;
                EdgeRouting = diagram.EdgeRouting;
                Palette = diagram.Palette;
            }
            ;
        };
    }

    [Flags]
    public enum ControlFlags
    {
        Direction,
        EdgeRouting,
        Palette,
        All = Direction | EdgeRouting | Palette,
    }

    ControlFlags controls = ControlFlags.Direction | ControlFlags.EdgeRouting;

    public ControlFlags Controls
    {
        get => controls;
        set
        {
            if (controls != value)
            {
                controls = value;
                directionStack.IsVisible = controls.HasFlag(ControlFlags.Direction);
                edgeRoutingStack.IsVisible = controls.HasFlag(ControlFlags.EdgeRouting);
                paletteStack.IsVisible = controls.HasFlag(ControlFlags.Palette);
            }
        }
    }

    FlexDiagram? Diagram => Parent?.FindByName("diagram") as FlexDiagram;

    DiagramDirection direction = DiagramDirection.TopBottom;

    public DiagramDirection Direction
    {
        get => direction;
        set
        {
            if (direction != value)
            {
                direction = value;
                OnPropertyChanged(nameof(Direction));
                var diagram = Diagram;
                if (diagram != null)
                    diagram.Direction = direction;
            }
        }
    }

    public Array Directions { get; } = Enum.GetValues(typeof(DiagramDirection));


    EdgeRouting edgeRouting = EdgeRouting.RectilinearToCenter;

    public EdgeRouting EdgeRouting
    {
        get => edgeRouting;
        set
        {
            if (edgeRouting != value)
            {
                edgeRouting = value;
                OnPropertyChanged(nameof(EdgeRouting));
                var diagram = Diagram;
                if (diagram != null)
                    diagram.EdgeRouting = edgeRouting;
            }
        }
    }

    public Array EdgeRoutings { get; } = Enum.GetValues(typeof(EdgeRouting));

    Palette palette = Palette.Standard;

    public Palette Palette
    {
        get => palette;
        set
        {
            if (palette != value)
            {
                palette = value;
                OnPropertyChanged(nameof(Palette));
                var diagram = Diagram;
                if (diagram != null)
                    diagram.Palette = palette;
            }
        }
    }

    public List<Palette> Palettes { get; } = Enum.GetValues(typeof(Palette)).OfType<Palette>().Take(16).ToList();
}