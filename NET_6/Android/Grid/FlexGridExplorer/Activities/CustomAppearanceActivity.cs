using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using C1.Android.Grid;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/CustomAppearanceTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class CustomAppearanceActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.CustomAppearance);

            ActionBar.Title = GetString(Resource.String.CustomAppearanceTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            Grid = FindViewById<FlexGrid>(Resource.Id.Grid);

            PopulateEditGrid();
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
        public FlexGrid Grid { get; set; }

        private void PopulateEditGrid()
        {
            // create the data
            var data = Customer.GetCustomerList(100);
            Grid.ItemsSource = data;

            // provide auto-complete lists for first and last name columns
            var col = Grid.Columns["FirstName"];
            col.DataMap = new GridDataMap() { ItemsSource = Customer.GetFirstNames() };
            col = Grid.Columns["LastName"];
            col.DataMap = new GridDataMap() { ItemsSource = Customer.GetLastNames() };


            col = Grid.Columns["Email"];
            col.InputType = Android.Text.InputTypes.TextVariationEmailAddress;

            // make read-only columns look disabled
            var readOnlyBrush = new ColorDrawable(Color.Argb(0xe0, 0xe0, 0xe0, 0xe0));
            foreach (var c in Grid.Columns)
            {
                if (c.PropertyInfo != null && !c.PropertyInfo.CanWrite)
                {
                    c.Background = readOnlyBrush;
                }
            }
        }
    }
}