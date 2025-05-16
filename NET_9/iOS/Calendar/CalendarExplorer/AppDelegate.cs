using System;
using C1.iOS.Calendar;
using Foundation;
using UIKit;

namespace CoreExplorer
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

            // create a UIViewController with a single UILabel
            var vc = new UIViewController();
            var calendar = new C1Calendar();
            calendar.MouseOverMode = CalendarMouseOverMode.Slot;
            calendar.Orientation = CalendarOrientation.Vertical;
            calendar.BoldedDates = new DateTime[] { DateTime.Today.AddDays(3) };
            calendar.TranslatesAutoresizingMaskIntoConstraints = false;
            vc.View.BackgroundColor = UIColor.White;
            vc.View.AddSubview(calendar);

            Window.RootViewController = vc;
            var constraints = new NSLayoutConstraint[]
            {
                calendar.TopAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.TopAnchor),
                calendar.BottomAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.BottomAnchor),
                calendar.LeftAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.LeftAnchor),
                calendar.RightAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.RightAnchor)
            };
            NSLayoutConstraint.ActivateConstraints(constraints);


            // make the window visible
            Window.MakeKeyAndVisible();

            return true;
        }
    }
}
