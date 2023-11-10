using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using C1.Android.Calendar;

namespace CalendarExplorer
{
    [Activity(Label = "@string/vertical_orientation", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class VerticalOrientationActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.VerticalOrientation);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var calendar = FindViewById<C1Calendar>(Resource.Id.Calendar);
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