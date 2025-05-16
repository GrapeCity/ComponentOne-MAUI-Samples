using Android.Content.PM;
using Android.Views;
using C1.Android.Calendar;

namespace CalendarExplorer
{
    [Activity(Label = "@string/custom_appearance", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.ScreenLayout)]
    public class CustomAppearanceActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.CustomAppearance);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var calendar = FindViewById<C1Calendar>(Resource.Id.Calendar);
            calendar.ViewModeAnimation.AnimationMode = CalendarViewModeAnimationMode.ZoomOutIn;
            calendar.ViewModeAnimation.ScaleFactor = 1.1;
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