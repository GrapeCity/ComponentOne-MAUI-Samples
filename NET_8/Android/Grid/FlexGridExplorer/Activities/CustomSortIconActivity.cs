using Android.Content.PM;
using Android.Graphics;
using Android.Views;
using C1.Android.Core;
using C1.Android.Grid;
using C1.DataCollection;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/CustomSortIcon", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class CustomSortIconActivity : Activity
    {
        private FlexGrid grid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CustomSortIcon);

            ActionBar.Title = GetString(Resource.String.CustomSortIcon);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var positionModes = FindViewById<Spinner>(Resource.Id.SortIconPosition);
            var iconModes = FindViewById<Spinner>(Resource.Id.SortIconTemplate);

            var positionItems = new GridSortIconPosition[] { GridSortIconPosition.Left, GridSortIconPosition.Inline, GridSortIconPosition.Right };
            var positonAdapteritems = new List<string>();
            foreach (var value in Enum.GetValues(typeof(GridSortIconPosition)))
            {
                positonAdapteritems.Add(value.ToString());
            }
            positionModes.Adapter = new ArrayAdapter(BaseContext, global::Android.Resource.Layout.SimpleSpinnerDropDownItem, positonAdapteritems);
            positionModes.ItemSelected += (s, e) =>
            {
                grid.SortIconPosition = positionItems[positionModes.SelectedItemPosition];
            };
            positionModes.SetSelection(0, false);
            var arrowUpBitmapTemplate = new C1IconTemplate(context => new C1BitmapIcon(context)
            {
                Source = BitmapFactory.DecodeResource(this.Resources, Resource.Drawable.arrow_up),
                ShowAsMonochrome = true
            });
            var sortAscendingTemplate = new C1IconTemplate(context => new C1SVGIcon(context)
            {
                Source = new Uri("raw:///sort_ascending")
            });
            var sort2Template = new C1IconTemplate(context => new C1SVGIcon(context)
            {
                Source = new Uri("raw:///sort2")
            });
            var sort2AscendingTemplate = new C1IconTemplate(context => new C1SVGIcon(context)
            {
                Source = new Uri("raw:///sort2_ascending")
            });
            var sort2DescendingTemplate = new C1IconTemplate(context => new C1SVGIcon(context)
            {
                Source = new Uri("raw:///sort2_descending")
            });
            var iconAdapterItems = new List<string>();
            foreach (var value in new string[] { "Bitmap", "Custom1", "Custom2", nameof(C1IconTemplate.TriangleUp), nameof(C1IconTemplate.TriangleNorth), nameof(C1IconTemplate.ChevronUp), nameof(C1IconTemplate.ArrowUp) })
            {
                iconAdapterItems.Add(value);
            }
            iconModes.Adapter = new ArrayAdapter(BaseContext, global::Android.Resource.Layout.SimpleSpinnerDropDownItem, iconAdapterItems);
            iconModes.ItemSelected += (s, e) =>
            {
                switch (iconModes.SelectedItemPosition)
                {
                    case 0:
                        grid.SortAscendingIconTemplate = arrowUpBitmapTemplate;
                        grid.SortDescendingIconTemplate = null;
                        grid.SortIndeterminateIconTemplate = null;
                        break;
                    case 1:
                        grid.SortAscendingIconTemplate = sortAscendingTemplate;
                        grid.SortDescendingIconTemplate = null;
                        grid.SortIndeterminateIconTemplate = null;
                        break;
                    case 2:
                        grid.SortAscendingIconTemplate = sort2AscendingTemplate;
                        grid.SortDescendingIconTemplate = sort2DescendingTemplate;
                        grid.SortIndeterminateIconTemplate = sort2Template;
                        break;
                    case 3:
                        grid.SortAscendingIconTemplate = C1IconTemplate.TriangleUp;
                        grid.SortDescendingIconTemplate = null;
                        grid.SortIndeterminateIconTemplate = null;
                        break;
                    case 4:
                        grid.SortAscendingIconTemplate = C1IconTemplate.TriangleNorth;
                        grid.SortDescendingIconTemplate = null;
                        grid.SortIndeterminateIconTemplate = null;
                        break;
                    case 5:
                        grid.SortAscendingIconTemplate = C1IconTemplate.ChevronUp;
                        grid.SortDescendingIconTemplate = null;
                        grid.SortIndeterminateIconTemplate = null;
                        break;
                    case 6:
                        grid.SortAscendingIconTemplate = C1IconTemplate.ArrowUp;
                        grid.SortDescendingIconTemplate = null;
                        grid.SortIndeterminateIconTemplate = null;
                        break;
                }
            };
            iconModes.SetSelection(0, false);
            grid = FindViewById<FlexGrid>(Resource.Id.Grid);
            LoadData();
        }
        private async void LoadData()
        {
            var cv = new C1DataCollection<Customer>(Customer.GetCustomerList(100));
            await cv.SortAsync(new SortDescription("FirstName", SortDirection.Ascending), new SortDescription("LastName", SortDirection.Descending));
            grid.ItemsSource = cv;
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == global::Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }
    }
}