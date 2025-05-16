using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using C1.Android.Core;
using C1.Android.Grid;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/UnboundTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class UnboundActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GettingStarted);

            ActionBar.Title = GetString(Resource.String.UnboundTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            Grid = FindViewById<FlexGrid>(Resource.Id.Grid);
            Grid.HeadersVisibility = GridHeadersVisibility.All;
            Grid.GridLinesVisibility = GridLinesVisibility.None;
            Grid.RowHeaderGridLinesVisibility = GridLinesVisibility.Vertical;
            Grid.SelectionMode = GridSelectionMode.CellRange;
            Grid.RowHeaderSelectedBackground = new ColorDrawable(C1ThemeInfo.ForContext(Grid.Context).IsDark ? Color.Argb(255, 11, 11, 11) : Color.Argb(255, 248, 248, 248));
            Grid.ColumnHeaderSelectedBackground = new ColorDrawable(C1ThemeInfo.ForContext(Grid.Context).IsDark ? Color.Argb(255, 11, 11, 11) : Color.Argb(255, 248, 248, 248));
            PopulateUnboundGrid();
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

        /// <summary>
        /// Fill unbound grid with data
        /// </summary>
        private void PopulateUnboundGrid()
        {
            // allow merging
            Grid.AllowMerging = GridAllowMerging.All;

            // add rows/columns to the unbound grid
            for (int i = 0; i < 12; i++) // three years, four quarters per year
            {
                Grid.Columns.Add(new GridColumn() { HeaderHorizontalAlignment = GravityFlags.Center });
            }
            for (int i = 0; i < 500; i++)
            {
                Grid.Rows.Add(new GridRow());
            }

            // populate the unbound Grid with some stuff
            for (int r = 0; r < Grid.Rows.Count; r++)
            {
                for (int c = 0; c < Grid.Columns.Count; c++)
                {
                    Grid[r, c] = string.Format("cell [{0},{1}]", r, c);
                }
            }

            // set unbound column headers
            var ch = Grid.ColumnHeaders;
            ch.Rows.Clear();
            ch.Rows.Add(new GridRow()); // one header row for years, one for quarters
            ch.Rows.Add(new GridRow());
            for (int c = 0; c < ch.Columns.Count; c++)
            {
                ch[0, c] = 2016 + c / 4; // year
                ch[1, c] = string.Format("Q {0}", c % 4 + 1); // quarter
            }

            // allow merging the first fixed row
            ch.Rows[0].AllowMerging = true;

            // set unbound row headers
            var rh = Grid.RowHeaders;
            rh.Columns.Add(new GridColumn());
            for (int c = 0; c < rh.Columns.Count; c++)
            {
                rh.Columns[c].Width = GridLength.Auto;
                for (int r = 0; r < rh.Rows.Count; r++)
                {
                    rh[r, c] = string.Format("hdr {0},{1}", c == 0 ? r / 2 : r, c);
                }
            }

            // allow merging the first fixed column
            rh.Columns[0].AllowMerging = true;

            //Grid.RowHeaders.Columns[0].Width = 110;
            //Grid.RowHeaders.Columns[1].Width = 110;
        }

    }
}