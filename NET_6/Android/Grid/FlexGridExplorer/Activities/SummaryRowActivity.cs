using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using C1.Android.Grid;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/SummaryRowTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class SummaryRowActivity : Activity
    {
        Random _rand = new Random();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GettingStarted);

            ActionBar.Title = GetString(Resource.String.GettingStartedTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var grid = FindViewById<FlexGrid>(Resource.Id.Grid);
            grid.AutoGenerateColumns = false;
            grid.Columns.AddRange(new[]
            {

                new GridColumn
                {
                    Binding = "[Col1]",
                    Format = "C2",
                    AggregateFunctions =
                    {

                        new GridAggregateFunction { Aggregate = GridAggregate.Minimum },
                        new GridAggregateFunction { Aggregate = GridAggregate.Maximum },
                        new CountBetweenFunction{ Minimum = 0.5, Maximum = 0.7, Caption = "Between[0.5]And[0.7]({value:N0})" },
                    }
                },
                new GridColumn { Binding = "[Col2]", Format = "N2", Aggregate= GridAggregate.Std },
                new GridColumn { Binding = "[Col3]", Format = "N0", Aggregate = GridAggregate.Count },
                new GridColumn { Binding = "[Col4]" , Aggregate = GridAggregate.Sum }
            });

            grid.ColumnFooterRows.Add(new GridSummaryRow() { Height = GridLength.Auto });

            var list = new List<Dictionary<string, double>>();
            for (int i = 0; i < 100; i++)
            {
                var dictionary = new Dictionary<string, double>();
                for (int j = 1; j <= 4; j++)
                    dictionary[$"Col{j}"] = _rand.NextDouble();
                list.Add(dictionary);
            }
            grid.ItemsSource = list;
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

    /// <summary>
    /// Returns the number of rows whose value for the column is between <see cref="Minimum"/> and <see cref="Maximum"/>.
    /// </summary>
    public class CountBetweenFunction : GridAggregateFunction
    {
        /// <summary>
        /// The minimum value.
        /// </summary>
        public new double Minimum { get; set; } = double.MinValue;

        /// <summary>
        /// The maximum value.
        /// </summary>
        public new double Maximum { get; set; } = double.MaxValue;

        ///<inheritdoc/>
        public override double GetValue(GridColumn column, IEnumerable<GridRow> rows)
        {
            var count = 0;
            var grid = column.Grid;
            foreach (var row in rows)
            {
                // get raw value
                var val = grid[row, column];
                if (val is double dVal)
                    if (dVal >= Minimum && dVal <= Maximum)
                        count++;
            }

            return count;
        }
    }

}