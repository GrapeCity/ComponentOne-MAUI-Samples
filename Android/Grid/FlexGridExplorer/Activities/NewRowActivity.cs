using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using C1.Android.Grid;
using C1.DataCollection;
using System.Collections;
using System.Threading.Tasks;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/NewRowTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class NewRowActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.NewRow);

            ActionBar.Title = GetString(Resource.String.NewRowTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var grid = FindViewById<FlexGrid>(Resource.Id.Grid);

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = new CustomDataCollection<Customer>(data);
            grid.NewRowPlaceholder = ApplicationContext.GetString(Resource.String.NewRowPlaceholder);
            grid.AllowDragging = GridAllowDragging.Both;
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

    public class CustomDataCollection<T> : C1DataCollection<T>
        where T : class
    {
        public CustomDataCollection(IEnumerable source)
            : base(source)
        {
        }

        public override async Task<int> InsertAsync(int index, object item, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000, cancellationToken); //simulates a network operation
            return await base.InsertAsync(index, item, cancellationToken);
        }

        public override async Task RemoveAsync(int index, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000, cancellationToken); //simulates a network operation
            await base.RemoveAsync(index, cancellationToken);
        }

        public override async Task RemoveRangeAsync(int index, int count, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000, cancellationToken); //simulates a network operation
            await base.RemoveRangeAsync(index, count, cancellationToken);
        }

        public override async Task<int> ReplaceAsync(int index, object item, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000, cancellationToken); //simulates a network operation
            return await base.ReplaceAsync(index, item, cancellationToken);
        }

        public override async Task MoveAsync(int fromIndex, int toIndex, CancellationToken cancellationToken = default)
        {
            await Task.Delay(1000); //simulates a network operation
            await base.MoveAsync(fromIndex, toIndex, cancellationToken);
        }
    }
}