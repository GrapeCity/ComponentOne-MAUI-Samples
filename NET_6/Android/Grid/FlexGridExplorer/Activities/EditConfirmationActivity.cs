using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Util;
using Android.Views;
using C1.Android.Grid;
using System;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/EditConfirmationTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class EditConfirmationActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GettingStarted);

            ActionBar.Title = GetString(Resource.String.EditConfirmationTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var dips_50 = TypedValue.ApplyDimension(ComplexUnitType.Dip, 50, Resources.DisplayMetrics);

            Grid = FindViewById<FlexGrid>(Resource.Id.Grid);
            Grid.GridLinesVisibility = GridLinesVisibility.None;
            Grid.ColumnHeaderGridLinesVisibility = GridLinesVisibility.None;
            Grid.HeadersVisibility = GridHeadersVisibility.Column;
            Grid.Background = new ColorDrawable(Color.White);
            Grid.RowBackground = new ColorDrawable(Color.Argb(0xFF, 0xE2, 0xEF, 0xDB));
            Grid.RowForeground = new ColorDrawable(Color.Black);
            Grid.AlternatingRowBackground = new ColorDrawable(Color.White);
            Grid.ColumnHeaderBackground = new ColorDrawable(Color.Argb(0xFF, 0x70, 0xAD, 0x46));
            Grid.ColumnHeaderForeground = new ColorDrawable(Color.White);
            Grid.ColumnHeaderTypeface = Typeface.Create("", TypefaceStyle.Bold);
            Grid.SelectionBackground = new ColorDrawable(Color.Argb(0xFF, 0x5A, 0x82, 0x3F));
            Grid.SelectionForeground = new ColorDrawable(Color.White);
            Grid.DefaultRowHeight = new GridLength(dips_50);
            Grid.ItemsSource = Customer.GetCustomerList(100);
            Grid.BeginningEdit += OnBeginningEdit;
            Grid.CellEditEnded += OnCellEditEnded;
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

        private object _originalValue;

        private void OnBeginningEdit(object sender, GridCellEditEventArgs e)
        {
            _originalValue = Grid[e.CellRange.Row, e.CellRange.Column];
        }

        private void OnCellEditEnded(object sender, GridCellEditEventArgs e)
        {
            var originalValue = _originalValue;
            var currentValue = Grid[e.CellRange.Row, e.CellRange.Column];
            if (!e.CancelEdits && (originalValue == null && currentValue != null || !originalValue.Equals(currentValue)))
            {
                var alert = new Android.App.AlertDialog.Builder(this);
                alert.SetTitle(Resources.GetString(Resource.String.EditConfirmationQuestionTitle));
                alert.SetMessage(Resources.GetString(Resource.String.EditConfirmationQuestion));
                alert.SetPositiveButton("Yes", new EventHandler<DialogClickEventArgs>((s, e2) => { }));
                alert.SetNegativeButton("No", new EventHandler<DialogClickEventArgs>((s, e2) =>
                {
                    Grid[e.CellRange.Row, e.CellRange.Column] = originalValue;
                }));
                alert.Show();
            }
        }
    }
}