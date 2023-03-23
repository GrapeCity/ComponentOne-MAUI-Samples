namespace FlexGridExplorer;

public partial class ImageCell : ContentView
{
    public ImageCell()
    {
        InitializeComponent();

        BindingContext = this;
    }

    public string Text
    {
        get { return (string)GetValue(TextProperty); }
        set { SetValue(TextProperty, value); }
    }

    public static readonly BindableProperty TextProperty = BindableProperty.Create("Text", typeof(string), typeof(ImageCell));

    public ImageSource ImageSource
    {
        get { return (ImageSource)GetValue(ImageSourceProperty); }
        set { SetValue(ImageSourceProperty, value); }
    }

    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create("ImageSource", typeof(ImageSource), typeof(ImageCell));


}