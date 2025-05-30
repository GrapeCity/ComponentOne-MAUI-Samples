using Android.Content.PM;
using Android.Text;
using Android.Util;
using Android.Views;
using C1.Android.Core;
using C1.Android.Grid;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/HierarchicalRowsTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class HierarchicalRowsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.HierarchicalRows);

            ActionBar.Title = GetString(Resource.String.HierarchicalRowsTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            Grid = FindViewById<FlexGrid>(Resource.Id.Grid)!;
            TreeIndentEditText = FindViewById<EditText>(Resource.Id.TreeIndentEditText)!;
            TreeColumnIndexEditText = FindViewById<EditText>(Resource.Id.TreeColumnIndexEditText)!;
            TreeExpandModeSpinner = FindViewById<Spinner>(Resource.Id.TreeExpandModeSpinner)!;
            TreeLinesModeSpinner = FindViewById<Spinner>(Resource.Id.TreeLinesModeSpinner)!;
            TreeIndentModeSpinner = FindViewById<Spinner>(Resource.Id.TreeIndentModeSpinner)!;

            #region Create tasks
            var task1 = new ProjectTask() { WBS = "1", Name = "Requirements", Duration = new TimeSpan(50, 0, 0, 0), Start = new DateTime(2009, 12, 4) };
            var task11 = new ProjectTask() { WBS = "1.1", Name = "Analysis", Duration = new TimeSpan(38, 3, 0, 0), Start = new DateTime(2009, 12, 4), ParentTask = task1 };
            var task111 = new ProjectTask() { WBS = "1.1.1", Name = "Analyze online reservations", Duration = new TimeSpan(12, 12, 0, 0), Start = new DateTime(2009, 12, 4), ParentTask = task11 };
            var task112 = new ProjectTask() { WBS = "1.1.2", Name = "Analyze query processes", Duration = new TimeSpan(12, 12, 0, 0), Start = new DateTime(2009, 12, 4), ParentTask = task11 };
            var task113 = new ProjectTask() { WBS = "1.1.3", Name = "Analyze multimedia enhancements", Duration = new TimeSpan(12, 12, 0, 0), Start = new DateTime(2010, 1, 4), ParentTask = task11 };
            var task114 = new ProjectTask() { WBS = "1.1.4", Name = "Draft Preliminary requirements", Duration = new TimeSpan(5, 0, 0, 0), Start = new DateTime(2010, 1, 14), ParentTask = task11 };
            var task115 = new ProjectTask() { WBS = "1.1.5", Name = "Review preliminary requirements", Duration = new TimeSpan(2, 12, 0, 0), Start = new DateTime(2010, 1, 14), ParentTask = task11 };
            var task116 = new ProjectTask() { WBS = "1.1.6", Name = "Incorporate feedback on requirements", Duration = new TimeSpan(2, 12, 0, 0), Start = new DateTime(2010, 1, 14), ParentTask = task11 };
            var task117 = new ProjectTask() { WBS = "1.1.7", Name = "Obtain approval to proceed", Duration = new TimeSpan(3, 2, 0, 0), Start = new DateTime(2010, 2, 6), ParentTask = task11 };
            task11.SubTasks = new List<ProjectTask> { task111, task112, task113, task114, task115, task116, task117 };
            var task12 = new ProjectTask() { WBS = "1.2", Name = "Acceptance Test Plan", Duration = new TimeSpan(12, 3, 0, 0), Start = new DateTime(2010, 6, 23), ParentTask = task1 };
            var task121 = new ProjectTask() { WBS = "1.2.1", Name = "Write acceptance test plans", Duration = new TimeSpan(5, 2, 0, 0), Start = new DateTime(2010, 6, 23), ParentTask = task12 };
            var task122 = new ProjectTask() { WBS = "1.2.2", Name = "Draft acceptance test plan", Duration = new TimeSpan(5, 0, 0, 0), Start = new DateTime(2010, 6, 23), ParentTask = task12 };
            var task123 = new ProjectTask() { WBS = "1.2.3", Name = "Review acceptance test plan", Duration = new TimeSpan(5, 6, 0, 0), Start = new DateTime(2010, 7, 4), ParentTask = task12 };
            var task124 = new ProjectTask() { WBS = "1.2.4", Name = "Incorporate feedback on acceptance", Duration = new TimeSpan(5, 0, 0, 0), Start = new DateTime(2010, 7, 4), ParentTask = task12 };
            task12.SubTasks = new List<ProjectTask> { task121, task122, task123, task124 };
            task1.SubTasks = new List<ProjectTask> { task11, task12 };
            var task2 = new ProjectTask() { WBS = "2", Name = "Design", Duration = new TimeSpan(55, 0, 0, 0), Start = new DateTime(2010, 8, 12) };
            var task21 = new ProjectTask() { WBS = "2.1", Name = "Top-level Design", Duration = new TimeSpan(27, 12, 0, 0), Start = new DateTime(2010, 8, 12), ParentTask = task2 };
            var task211 = new ProjectTask() { WBS = "2.1.1", Name = "Design online reservations", Duration = new TimeSpan(10, 0, 0, 0), Start = new DateTime(2010, 9, 7), ParentTask = task21 };
            var task212 = new ProjectTask() { WBS = "2.1.2", Name = "Design query processes", Duration = new TimeSpan(10, 12, 0, 0), Start = new DateTime(2010, 9, 14), ParentTask = task21 };
            var task213 = new ProjectTask() { WBS = "2.1.3", Name = "Design multimedia enhancements", Duration = new TimeSpan(10, 6, 0, 0), Start = new DateTime(2010, 10, 4), ParentTask = task21 };
            var task214 = new ProjectTask() { WBS = "2.1.4", Name = "Review design specification", Duration = new TimeSpan(5, 12, 0, 0), Start = new DateTime(2010, 10, 9), ParentTask = task21 };
            var task215 = new ProjectTask() { WBS = "2.1.5", Name = "Incorporate feedback into design", Duration = new TimeSpan(2, 2, 0, 0), Start = new DateTime(2010, 10, 9), ParentTask = task21 };
            var task216 = new ProjectTask() { WBS = "2.1.6", Name = "Top-level design approved", Duration = new TimeSpan(1, 0, 0, 0), Start = new DateTime(2010, 12, 1), ParentTask = task21 };
            task21.SubTasks = new List<ProjectTask> { task211, task212, task213, task214, task215, task216 };
            var task22 = new ProjectTask() { WBS = "2.2", Name = "Detailed Design", Duration = new TimeSpan(23, 0, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task2 };
            var task221 = new ProjectTask() { WBS = "2.2.1", Name = "Draft design specifications", Duration = new TimeSpan(17, 12, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task22 };
            var task222 = new ProjectTask() { WBS = "2.2.2", Name = "Review design specifications", Duration = new TimeSpan(17, 0, 0, 0), Start = new DateTime(2010, 12, 8), ParentTask = task22 };
            var task223 = new ProjectTask() { WBS = "2.2.3", Name = "Incorporate feedback on design specifications", Duration = new TimeSpan(17, 6, 0, 0), Start = new DateTime(2010, 12, 14), ParentTask = task22 };
            var task224 = new ProjectTask() { WBS = "2.2.4", Name = "Obtain approval to proceed", Duration = new TimeSpan(5, 0, 0, 0), Start = new DateTime(2010, 12, 24), ParentTask = task22 };
            var task225 = new ProjectTask() { WBS = "2.2.5", Name = "Detailed design approved", Duration = new TimeSpan(2, 12, 0, 0), Start = new DateTime(2010, 12, 28), ParentTask = task22 };
            task22.SubTasks = new List<ProjectTask> { task221, task222, task223, task224, task225 };
            task2.SubTasks = new List<ProjectTask> { task21, task22 };
            var task3 = new ProjectTask() { WBS = "3", Name = "Code and Unit Test", Duration = new TimeSpan(32, 4, 0, 0), Start = new DateTime(2010, 12, 4) };
            var task31 = new ProjectTask() { WBS = "3.1", Name = "Assign development staff", Duration = new TimeSpan(2, 3, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task3 };
            var task32 = new ProjectTask() { WBS = "3.2", Name = "Develop Code", Duration = new TimeSpan(10, 3, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task3 };
            var task321 = new ProjectTask() { WBS = "3.2.1", Name = "Develop online reservations", Duration = new TimeSpan(10, 2, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task32 };
            var task322 = new ProjectTask() { WBS = "3.2.2", Name = "Test online reservations", Duration = new TimeSpan(1, 11, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task32 };
            var task323 = new ProjectTask() { WBS = "3.2.3", Name = "Develop query processes", Duration = new TimeSpan(10, 0, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task32 };
            var task324 = new ProjectTask() { WBS = "3.2.4", Name = "Test query processes", Duration = new TimeSpan(1, 4, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task32 };
            task32.SubTasks = new List<ProjectTask> { task321, task322, task323, task324 };
            task3.SubTasks = new List<ProjectTask> { task31, task32 };
            var tasks = new List<ProjectTask> { task1, task2, task3 };
            #endregion

            Grid.ChildItemsPath = nameof(ProjectTask.SubTasks);
            Grid.AutoGeneratingColumn += OnAutoGeneratingColumn;
            Grid.ItemsSource = tasks;

            TreeIndentEditText.Text = (Grid.TreeIndent / BaseContext.Resources.DisplayMetrics.ScaledDensity).ToString();
            TreeColumnIndexEditText.Text = Grid.TreeColumnIndex.ToString();
            var items = Enum.GetValues<GridTreeExpandMode>();
            TreeExpandModeSpinner.Adapter = new ArrayAdapter(BaseContext, global::Android.Resource.Layout.SimpleSpinnerDropDownItem, items.Select(mode => mode.ToString()).ToArray());
            TreeExpandModeSpinner.SetSelection(2);
            TreeExpandModeSpinner.ItemSelected += (s, e) =>
            {
                Grid.TreeExpandMode = items[TreeExpandModeSpinner.SelectedItemPosition];
            };
            var treeLinesItems = Enum.GetValues<GridTreeLinesMode>();
            TreeLinesModeSpinner.Adapter = new ArrayAdapter(BaseContext, global::Android.Resource.Layout.SimpleSpinnerDropDownItem, treeLinesItems.Select(mode => mode.ToString()).ToArray());
            TreeLinesModeSpinner.SetSelection(1);
            TreeLinesModeSpinner.ItemSelected += (s, e) =>
            {
                Grid.TreeLinesMode = treeLinesItems[TreeLinesModeSpinner.SelectedItemPosition];
            };
            var treeIndentItems = Enum.GetValues<GridTreeIndentMode>();
            TreeIndentModeSpinner.Adapter = new ArrayAdapter(BaseContext, global::Android.Resource.Layout.SimpleSpinnerDropDownItem, treeIndentItems.Select(mode => mode.ToString()).ToArray());
            TreeIndentModeSpinner.ItemSelected += (s, e) =>
            {
                Grid.TreeIndentMode = treeIndentItems[TreeIndentModeSpinner.SelectedItemPosition];
            }; 
            TreeIndentEditText.TextChanged += (s, e) =>
            {
                if (int.TryParse(e.Text.ToString(), out var treeIndent))
                {
                    Grid.TreeIndent = TypedValue.ApplyDimension(ComplexUnitType.Dip, treeIndent, BaseContext.Resources.DisplayMetrics);
                }
            };
            TreeColumnIndexEditText.TextChanged += (s, e) =>
            {
                if (int.TryParse(e.Text.ToString(), out var treeColumnIndex))
                {
                    Grid.TreeColumnIndex = treeColumnIndex;
                }
            };
        }


        public FlexGrid Grid;
        private EditText TreeIndentEditText;
        private EditText TreeColumnIndexEditText;
        private Spinner TreeExpandModeSpinner, TreeLinesModeSpinner, TreeIndentModeSpinner;

        private void OnAutoGeneratingColumn(object sender, C1.Android.Grid.GridAutoGeneratingColumnEventArgs e)
        {
            if (e.Property.Name == "Name")
            {
                e.Column.AllowResizing = false;
                e.Column.MinWidth = 300;
                e.Column.Width = new GridLength(1, GridUnitType.Star);
            }
            if (e.Property.Name == "WBS")
                e.Column.Width = GridLength.Auto;
            if (e.Property.Name == "Duration")
                e.Column.ValueConverter = DelegateConverter.Create(
                    (value, type, parameter, culture) => string.Format("{0:N1} days?", ((TimeSpan)value).TotalDays),
                    (value, type, parameter, culture) =>
                    {
                        var str = value?.ToString() ?? "";
                        TimeSpan timeSpan;
                        if (TimeSpan.TryParse(str, out timeSpan))
                            return timeSpan;
                        if (str.EndsWith(" days?"))
                            str = str.Substring(0, str.Length - " days?".Length);
                        double totalDays;
                        if (double.TryParse(str, out totalDays))
                            return TimeSpan.FromDays(totalDays);
                        return TimeSpan.Zero;
                    });
            if (e.Column is GridDateTimeColumn)
                e.Column.Format = "ddd d/M/yyyy";
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