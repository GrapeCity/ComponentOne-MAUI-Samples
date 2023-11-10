using System.Linq;
using C1.iOS.Core;
using C1.iOS.Grid;
using CoreAnimation;
using CoreGraphics;
using Foundation;
using UIKit;

namespace Flights
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        FlexGrid _grid;

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
            _grid = new FlexGrid();
            _grid.CellFactory = new ListViewCellFactoryStyle(6, 12, 2);
            _grid.AutoGeneratingColumn += OnAutoGeneratingColumn;
            _grid.ItemsSource = Flight.CreateRandomList(100);
            _grid.TranslatesAutoresizingMaskIntoConstraints = false;
            //vc.View.BackgroundColor = UIColor.White;
            _grid.Background = new CALayer { Contents = UIImage.FromBundle("Background").CGImage, ContentsGravity = CALayer.GravityResizeAspectFill };
            _grid.RowBackground = new CAGradientLayer
            {
                Colors = new CGColor[]
                {
                    UIColor.FromRGBA(0x29, 0x29, 0x29, 0x66).CGColor,
                    UIColor.FromRGBA(0x5D, 0x5D, 0x5D, 0x66).CGColor,
                    UIColor.FromRGBA(0x5D, 0x5D, 0x5D, 0x66).CGColor,
                },
                Locations = new NSNumber[]
                {
                    0f,
                    0.5f,
                    1f
                }
            };
            vc.View.AddSubview(_grid);

            Window.RootViewController = vc;
            var constraints = new NSLayoutConstraint[]
            {
                _grid.TopAnchor.ConstraintEqualTo(vc.View.TopAnchor),
                _grid.BottomAnchor.ConstraintEqualTo(vc.View.BottomAnchor),
                _grid.LeftAnchor.ConstraintEqualTo(vc.View.LeftAnchor),
                _grid.RightAnchor.ConstraintEqualTo(vc.View.RightAnchor)
            };
            NSLayoutConstraint.ActivateConstraints(constraints);

            NSNotificationCenter.DefaultCenter.AddObserver(UIDevice.OrientationDidChangeNotification, OnOrientationChanged);

            // make the window visible
            Window.MakeKeyAndVisible();

            return true;
        }

        private void OnAutoGeneratingColumn(object sender, GridAutoGeneratingColumnEventArgs e)
        {
            if (e.Property.Name == nameof(Flight.Time))
            {
                e.Column.Format = "HH:mm";
                e.Column.Width = GridLength.Auto;
                e.Column.AllowDragging = false;
            }
            if (e.Property.Name == nameof(Flight.Destination))
            {
                e.Column.Width = GridLength.Star;
            }
            if (e.Property.Name == nameof(Flight.FlightId))
            {
                e.Column.Width = GridLength.Auto;
            }
            if (e.Property.Name == nameof(Flight.Gate))
            {
                e.Column.Width = GridLength.Auto;
                e.Column.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
                e.Column.HeaderHorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            }
            if (e.Property.Name == nameof(Flight.Remark))
            {
                e.Column.Width = new GridLength(0.5, GridUnitType.Star);
                e.Column.AllowDragging = false;
                e.Column.HorizontalAlignment = UIControlContentHorizontalAlignment.Center;
                e.Column.HeaderHorizontalAlignment = UIControlContentHorizontalAlignment.Center;
            }
        }

        private void OnOrientationChanged(NSNotification notification)
        {
            UpdateOrientation();
        }

        private void UpdateOrientation()
        {
            _grid.Columns[nameof(Flight.Remark)].IsVisible = UIDevice.CurrentDevice.Orientation.IsLandscape();
            _grid.Refresh();
        }
    }

    public class ListViewCellFactoryStyle : GridCellFactory
    {
        float _border, _cornerRadius, _elevation;

        public ListViewCellFactoryStyle(float border, float cornerRadius, float elevation)
        {
            _border = border;
            _cornerRadius = cornerRadius;
            _elevation = elevation;
        }

        //public Style LeftCellStyle { get; set; }
        //public Style CenterCellStyle { get; set; }
        //public Style RightCellStyle { get; set; }

        public override void PrepareCell(GridCellType cellType, GridCellRange range, GridCellView cell, UIEdgeInsets internalBorders)
        {
            base.PrepareCell(cellType, range, cell, internalBorders);
            //cell.ClearValue(Control.BorderThicknessProperty);
            if (cellType == GridCellType.Cell)
            {
                if (range.Column == 0)
                {
                    cell.CornerRadius = new C1CornerRadius(_cornerRadius, 0, 0, _cornerRadius);
                    cell.BorderThickness = new UIEdgeInsets(_border, _border, _border, 0);
                    //cell.Style = LeftCellStyle ?? CenterCellStyle;
                }
                else if (range.Column == Grid.Columns.Count(c => c.IsVisible) - 1)
                {
                    cell.CornerRadius = new C1CornerRadius(0, _cornerRadius, _cornerRadius, 0);
                    cell.BorderThickness = new UIEdgeInsets(_border, 0, _border, _border);
                    //cell.Style = RightCellStyle ?? CenterCellStyle;
                }
                else
                {
                    cell.CornerRadius = new C1CornerRadius(0, 0, 0, 0);
                    cell.BorderThickness = new UIEdgeInsets(_border, 0, _border, 0);
                    //cell.Style = CenterCellStyle;
                }
            }
        }

        public override void BindCellContent(GridCellType cellType, GridCellRange range, UIView cellContent)
        {
            base.BindCellContent(cellType, range, cellContent);
            if (cellType == GridCellType.Cell && cellContent is UITextView label)
            {
                //label.SetShadowLayer(4, 4, 4, UIColor.Black);
                if (range.Column == Grid.Columns[nameof(Flight.Remark)].Index)
                {
                    var flight = Grid.Rows[range.Row].DataItem as Flight;
                    switch (flight.Remark)
                    {
                        case "Delayed":
                            label.TextColor = UIColor.Red;
                            break;
                        case "Check-in":
                            label.TextColor = UIColor.Green;
                            break;
                    }
                }
            }
            else if (cellType == GridCellType.ColumnHeader && cellContent is GridColumnHeaderCell columnHeader)
            {
                //var label2 = columnHeader.GetChildAt(1) as TextView;
                //var sortButton = columnHeader.GetChildAt(2) as C1ToggleButton;
                //var icon1 = sortButton.CheckedContent as C1Icon;
                //var icon2 = sortButton.CheckedContent as C1Icon;
                //label2.SetShadowLayer((float)_elevation, (float)_elevation, (float)_elevation, Color.Black);
                ////icon1.SetOutlineSpotShadowColor(Color.Black);
                ////icon2.SetOutlineSpotShadowColor(Color.Black);
                //icon1.OutlineProvider = ViewOutlineProvider.Background;
                //icon2.OutlineProvider = ViewOutlineProvider.Background;
                //icon1.Elevation = (float)_elevation;
                //icon2.Elevation = (float)_elevation;
                ////icon1.Foreground = new ColorDrawable(Color.Red);
                ////icon2.Foreground = new ColorDrawable(Color.Red);
            }
        }
    }
}
