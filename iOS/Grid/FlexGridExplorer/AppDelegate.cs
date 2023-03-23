using C1.iOS.Grid;
using Foundation;
using UIKit;

namespace FlexGridExplorer
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var vc = new UIViewController();
            var grid = new FlexGrid();
            grid.Rows.Add(new GridFilterRow() { AutoComplete = true, Placeholder = "Enter text to filer" });
            grid.ItemsSource = Customer.GetCustomerList(100);
            grid.TranslatesAutoresizingMaskIntoConstraints = false;
            vc.View.BackgroundColor = UIColor.White;
            vc.View.AddSubview(grid);

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
}
