using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using C1.Android.Grid;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/FilterRowTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class FilterRowActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GettingStarted);

            ActionBar.Title = GetString(Resource.String.FilterRowTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var grid = FindViewById<FlexGrid>(Resource.Id.Grid);
            grid.HeadersVisibility = GridHeadersVisibility.All;
            grid.FrozenRows = 1;
            grid.ItemsSource = Customer.GetCustomerList(100);
            grid.Rows.Insert(0, new GridFilterRow { Placeholder = GetString(Resource.String.FilterPlaceholderText), AutoComplete = true });
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == global::Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}