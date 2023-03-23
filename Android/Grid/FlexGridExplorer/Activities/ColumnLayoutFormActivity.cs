using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using C1.Android.Grid;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/ColumnLayoutTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class ColumnLayoutFormActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            var columnLayout = Intent.Extras.GetString("columnLayout");
            var columns = ColumnLayoutActivity.DeserializeLayout(columnLayout);

            SetContentView(Resource.Layout.GettingStarted);

            ActionBar.Title = GetString(Resource.String.ColumnLayoutTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            Grid = FindViewById<FlexGrid>(Resource.Id.Grid);
            Grid.AutoGenerateColumns = false;
            Grid.Columns.Add(new GridColumn { Binding = "Name", Header = "Column name", Width = GridLength.Star });
            var checklistBehavior = new CheckListBehavior { SelectionBinding = "IsVisible" };
            checklistBehavior.Attach(Grid);
            Grid.ItemsSource = columns;
        }

        public FlexGrid Grid { get; set; }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var filterMenuItem = menu.Add(0, 0, 0, Resource.String.ColumnLayoutSave);
            filterMenuItem.SetShowAsAction(ShowAsAction.Always);
            //filterMenuItem.SetIcon(Resource.Drawable.ic_action_save);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == 0)
            {
                FinishColumnLayout();
            }
            else if (item.ItemId == global::Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }



        private async void FinishColumnLayout()
        {
            Grid.FinishEditing();
            Intent.PutExtra("columnLayout", await ColumnLayoutActivity.SerializeLayout(Grid.ItemsSource as ColumnInfo[]));
            SetResult(Result.Ok, Intent);
            Finish();
        }
    }
}