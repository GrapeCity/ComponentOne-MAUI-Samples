using Android.App;
using Android.Content.PM;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using C1.Android.Grid;
using System;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/FullTextFilterTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class FullTextFilterActivity : Activity
    {
        private FullTextFilterBehavior fullTextFilter;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.FullTextFilter);

            ActionBar.Title = GetString(Resource.String.FullTextFilterTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var grid = FindViewById<FlexGrid>(Resource.Id.Grid);
            var entry = FindViewById<EditText>(Resource.Id.Filter);
            grid.ItemsSource = Customer.GetCustomerList(100);

            fullTextFilter = new FullTextFilterBehavior();
            fullTextFilter.MatchNumbers=true;
            fullTextFilter.Attach(grid);
            fullTextFilter.HighlightColor = new ColorDrawable(global::Android.Graphics.Color.ParseColor("#B00F50"));
            fullTextFilter.FilterEntry = entry;

            var matchCaseCheckBox = FindViewById<CheckBox>(Resource.Id.MatchCaseCheckBox);
            matchCaseCheckBox.CheckedChange += OnMatchCaseCheckBoxChanged;
            matchCaseCheckBox.Text = GetString(Resource.String.MatchCase);
                
            var matchWholeWordCheckBox = FindViewById<CheckBox>(Resource.Id.MatchWholeWordCheckBox);
            matchWholeWordCheckBox.CheckedChange += OnMatchWholeWordeCheckBoxChanged;
            matchWholeWordCheckBox.Text = GetString(Resource.String.MatchWholeWord);
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

        private void OnMatchCaseCheckBoxChanged(object sender, EventArgs e)
        {
            fullTextFilter.MatchCase = FindViewById<CheckBox>(Resource.Id.MatchCaseCheckBox).Checked;
        }

        private void OnMatchWholeWordeCheckBoxChanged(object sender, EventArgs e)
        {
            fullTextFilter.MatchWholeWord = FindViewById<CheckBox>(Resource.Id.MatchWholeWordCheckBox).Checked;
        }
    }
}