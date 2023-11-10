using C1.iOS.Core;
using C1.iOS.Grid;

namespace FlexGridExplorer.Mac;

[Register("AppDelegate")]
public class AppDelegate : UIApplicationDelegate
{
    public override UIWindow? Window
    {
        get;
        set;
    }

    public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        // create a new window instance based on the screen size
        Window = new UIWindow(UIScreen.MainScreen.Bounds);

        // create a UIViewController with a single UILabel
        var vc = new UIViewController();
        var grid = new FlexGrid {  HeadersVisibility = GridHeadersVisibility.All };
        grid.SelectionMode = GridSelectionMode.MultiRange;
        grid.SelectionBackground = new CoreAnimation.CALayer { BackgroundColor = new CGColor(0, 0, 0, 0.08f) };
        grid.Rows.Add(new GridFilterRow() { AutoComplete = true, Placeholder = "Enter text to filer" });
        grid.ItemsSource = Customer.GetCustomerList(100);
        grid.TranslatesAutoresizingMaskIntoConstraints = false;
        vc.View!.AddSubview(grid);

        //var button = new C1Button(new CGRect(100, 50, 100, 40));
        //button.Content = "Click me!";
        //button.Click += (s, e) =>
        //{
        //    grid.SimulateDrag(0);
        //};
        //vc.View!.AddSubview(button);

        Window.RootViewController = vc;
        var constraints = new NSLayoutConstraint[]
        {
            grid.TopAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.TopAnchor),
            grid.BottomAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.BottomAnchor),
            grid.LeftAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.LeftAnchor),
            grid.RightAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.RightAnchor)
        };
        NSLayoutConstraint.ActivateConstraints(constraints);

        // make the window visible
        Window.MakeKeyAndVisible();

        return true;
    }
}

