using Android.Content.PM;
using Android.Views;
using C1.Android.Grid;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/ExportTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class ExportActivity : ActivityBase
    {
        private string FILENAME = "ExportedGrid";
        FlexGrid grid;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.GettingStarted);

            ActionBar.Title = GetString(Resource.String.ExportTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            grid = FindViewById<FlexGrid>(Resource.Id.Grid);
            grid.ItemsSource = Customer.GetCustomerList(100);
            grid.ZoomMode = GridZoomMode.Disabled;
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            var editMenuItem = menu.Add(0, 0, 0, Resource.String.ExportTitle);
            editMenuItem.SetShowAsAction(ShowAsAction.Always);
            //editMenuItem.SetIcon(Resource.Drawable.ic_action_save);
            return base.OnCreateOptionsMenu(menu);
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == 0)
            {
                Save(FindViewById(item.ItemId));
            }
            else if (item.ItemId == global::Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            return base.OnOptionsItemSelected(item);
        }
        public void Save(View? anchor)
        {
            PopupMenu menu = new PopupMenu(this, anchor);
            menu.MenuItemClick += async (s1, arg1) =>
            {
                if (CheckSelfPermission(Android.Manifest.Permission.WriteExternalStorage) != Permission.Granted)
                {
                    if ((await RequestPermissionsAsync(new string[] { Android.Manifest.Permission.WriteExternalStorage })).Any(r => r != Permission.Granted))
                        return;
                }
                string type = arg1.Item.TitleFormatted.ToString();
                string PathAndName = Path.Combine(Android.OS.Environment.ExternalStorageDirectory!.AbsolutePath, Android.OS.Environment.DirectoryDownloads!, FILENAME) + "." + type;
                switch (type)
                {
                    case "CSV":
                        grid.Save(PathAndName, GridFileFormat.Csv, System.Text.Encoding.UTF8, GridSaveOptions.SaveColumnHeaders);
                        menu.Dismiss();
                        break;
                    case "Txt":
                        grid.Save(PathAndName, GridFileFormat.Text, System.Text.Encoding.UTF8, GridSaveOptions.SaveColumnHeaders);
                        menu.Dismiss();
                        break;
                    case "HTML":
                        grid.Save(PathAndName, GridFileFormat.Html, System.Text.Encoding.UTF8, GridSaveOptions.SaveColumnHeaders);
                        menu.Dismiss();
                        break;
                }
                if (type != "Cancel")
                {
                    Android.App.AlertDialog.Builder alert = new Android.App.AlertDialog.Builder(this);
                    alert.SetTitle("Saved");
                    alert.SetMessage("File has been saved to: " + PathAndName);
                    alert.SetPositiveButton("OK", (senderAlert, args) => { });
                    Dialog dialog = alert.Create();
                    dialog.Show();
                }
                if (type == "HTML")
                {
                    //Device.OpenUri(new Uri(PathAndName));
                }
            };
            menu.Inflate(Resource.Layout.PopUp);
            menu.Show();
        }
    }
}