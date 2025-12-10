using C1.Maui.Grid;

namespace FlexGridExplorer;

public partial class NodeCell : ContentView
{
	public NodeCell()
	{
		InitializeComponent();

        BindingContext = this;
	}

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(NodeCell));

    public ImageSource ImageSource
    {
        get { return (ImageSource)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }

    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create("ImageSource", typeof(ImageSource), typeof(NodeCell));


    public bool IsCollapsed
    {
        get { return (bool)GetValue(IsCollapsedProperty); }
        set { SetValue(IsCollapsedProperty, value); }
    }

    public static readonly BindableProperty IsCollapsedProperty = BindableProperty.Create("IsCollapsed", typeof(bool), typeof(NodeCell));


    public event EventHandler IsCollapsedChanged;

    public GridGroupRow Tag { get; internal set; }

    private void OnChecked(object sender, EventArgs e)
    {
        IsCollapsedChanged?.Invoke(this, e);
    }
}