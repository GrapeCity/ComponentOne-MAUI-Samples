using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.Views;
using C1.Android.Calendar;
using C1.Android.Core;

namespace CalendarExplorer
{
    [Activity(Label = "@string/custom_day_content", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class CustomDayContentActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            SetContentView(Resource.Layout.GettingStarted);

            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var calendar = FindViewById<C1Calendar>(Resource.Id.Calendar);
            calendar.DaySlotLoading += OnDaySlotLoading;
        }

        private void OnDaySlotLoading(object sender, CalendarDaySlotLoadingEventArgs e)
        {
            // add weather image for certain days in the current month
            if (e.Date.Year == DateTime.Today.Year &&
                e.Date.Month == DateTime.Today.Month &&
                e.Date.Day >= 14 && e.Date.Day <= 23)
            {
                var slot = LayoutInflater.Inflate(Resource.Layout.DaySlot, null);
                var bitmapIcon = slot.FindViewById<C1BitmapIcon>(Resource.Id.Image);
                var tv = slot.FindViewById<TextView>(Resource.Id.Day);
                bitmapIcon.ShowAsMonochrome = true;
                bitmapIcon.Foreground = new ColorDrawable(new Android.Graphics.Color(tv.TextColors.DefaultColor));

                tv.Text = e.Date.Day.ToString();

                switch (e.Date.Day % 5)
                {
                    case 0:
                        bitmapIcon.SetImageResource(Resource.Drawable.Cloudy);
                        break;
                    case 1:
                        bitmapIcon.SetImageResource(Resource.Drawable.PartlyCloudy);
                        break;
                    case 2:
                        bitmapIcon.SetImageResource(Resource.Drawable.Rain);
                        break;
                    case 3:
                        bitmapIcon.SetImageResource(Resource.Drawable.Storm);
                        break;
                    case 4:
                        bitmapIcon.SetImageResource(Resource.Drawable.Sun);
                        break;

                }
                e.DaySlot = slot;
            }

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