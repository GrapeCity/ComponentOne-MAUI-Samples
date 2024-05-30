using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using System;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/ApplicationName", MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //C1.Android.Core.LicenseManager.Key = License.Key;
            SetContentView(Resource.Layout.Main);

            var recyclerView = FindViewById<RecyclerView>(Resource.Id.RecyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(BaseContext));
            var adapter = new FlexGridSamplesAdapter();
            adapter.ItemClicked += (s, e) =>
            {
                switch (e.Position)
                {
                    case 0:
                        StartActivity(typeof(GettingStartedActivity));
                        break;
                    case 1:
                        StartActivity(typeof(ColumnDefinitionActivity));
                        break;
                    case 2:
                        StartActivity(typeof(SelectionModesActivity));
                        break;
                    case 3:
                        StartActivity(typeof(EditConfirmationActivity));
                        break;
                    case 4:
                        StartActivity(typeof(EditingActivity));
                        break;
                    case 5:
                        StartActivity(typeof(ConditionalFormattingActivity));
                        break;
                    case 6:
                        StartActivity(typeof(CustomCellsActivity));
                        break;
                    case 7:
                        StartActivity(typeof(HierarchicalRowsActivity));
                        break;
                    case 8:
                        StartActivity(typeof(GroupingActivity));
                        break;
                    case 9:
                        StartActivity(typeof(RowDetailsActivity));
                        break;
                    case 10:
                        StartActivity(typeof(FilterActivity));
                        break;
                    case 11:
                        StartActivity(typeof(FilterRowActivity));
                        break;
                    case 12:
                        StartActivity(typeof(FullTextFilterActivity));
                        break;
                    case 13:
                        StartActivity(typeof(ColumnLayoutActivity));
                        break;
                    case 14:
                        StartActivity(typeof(StarSizingActivity));
                        break;
                    case 15:
                        StartActivity(typeof(CellFreezingActivity));
                        break;
                    case 16:
                        StartActivity(typeof(CustomMergingActivity));
                        break;
                    case 17:
                        StartActivity(typeof(UnboundActivity));
                        break;
                    case 18:
                        StartActivity(typeof(OnDemandActivity));
                        break;
                    case 19:
                        StartActivity(typeof(CustomAppearanceActivity));
                        break;
                    case 20:
                        StartActivity(typeof(NewRowActivity));
                        break;
                    case 21:
                        StartActivity(typeof(CheckListActivity));
                        break;
                    case 22:
                        StartActivity(typeof(CustomSortIconActivity));
                        break;
                    case 23:
                        StartActivity(typeof(ExportActivity));
                        break;
                    case 24:
                        StartActivity(typeof(SummaryRowActivity));
                        break;
                    case 25:
                        StartActivity(typeof(TransposedGridActivity));
                        break;
                }
            };
            recyclerView.SetAdapter(adapter);
        }
    }
    internal class FlexGridSamplesAdapter : RecyclerView.Adapter
    {
        public event EventHandler<FlexGridSampleItemClickedEventArgs> ItemClicked;

        public override int ItemCount
        {
            get
            {
                return 26;
            }
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            var view = LayoutInflater.From(parent.Context)
                               .Inflate(Resource.Layout.ListItem, null);
            view.Click += (s, e) =>
            {
                var v = s as View;
                var recyclerView = v.Parent as RecyclerView;
                int itemPosition = recyclerView.GetChildAdapterPosition(v);

                if (ItemClicked != null)
                    ItemClicked(v, new FlexGridSampleItemClickedEventArgs { Position = itemPosition });
            };
            return new FlexGridSamplesViewHolder(view);
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
            var h = holder as FlexGridSamplesViewHolder;
            var resources = holder.ItemView.Context.Resources;
            switch (position)
            {
                case 0:
                    h.SetTitle(resources.GetString(Resource.String.GettingStartedTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.GettingStartedDescription));
                    h.SetIcon(Resource.Drawable.flexgrid);
                    break;
                case 1:
                    h.SetTitle(resources.GetString(Resource.String.ColumnDefinitionTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.ColumnDefinitionDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_columns);
                    break;
                case 2:
                    h.SetTitle(resources.GetString(Resource.String.SelectionModesTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.SelectionModesDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_selection);
                    break;
                case 3:
                    h.SetTitle(resources.GetString(Resource.String.EditConfirmationTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.EditConfirmationDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_editConfirmation);
                    break;
                case 4:
                    h.SetTitle(resources.GetString(Resource.String.EditingTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.EditingDescription));
                    h.SetIcon(Resource.Drawable.input_form);
                    break;
                case 5:
                    h.SetTitle(resources.GetString(Resource.String.ConditionalFormattingTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.ConditionalFormattingDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_conditionalFormatting);
                    break;
                case 6:
                    h.SetTitle(resources.GetString(Resource.String.CustomCellsTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.CustomCellsDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_custom);
                    break;
                case 7:
                    h.SetTitle(resources.GetString(Resource.String.HierarchicalRowsTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.HierarchicalRowsDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_grouping);
                    break;
                case 8:
                    h.SetTitle(resources.GetString(Resource.String.GroupingTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.GroupingDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_grouping);
                    break;
                case 9:
                    h.SetTitle(resources.GetString(Resource.String.RowDetailsTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.RowDetailsDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_rowdetails);
                    break;
                case 10:
                    h.SetTitle(resources.GetString(Resource.String.FilterTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.FilterDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_filter);
                    break;
                case 11:
                    h.SetTitle(resources.GetString(Resource.String.FilterRowTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.FilterRowDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_filter);
                    break;
                case 12:
                    h.SetTitle(resources.GetString(Resource.String.FullTextFilterTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.FullTextFilterDescription));
                    h.SetIcon(Resource.Drawable.filter);
                    break;
                case 13:
                    h.SetTitle(resources.GetString(Resource.String.ColumnLayoutTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.ColumnLayoutDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_columns);
                    break;
                case 14:
                    h.SetTitle(resources.GetString(Resource.String.StarSizingTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.StarSizingDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_starSizing);
                    break;
                case 15:
                    h.SetTitle(resources.GetString(Resource.String.CellFreezingTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.CellFreezingDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_freezing);
                    break;
                case 16:
                    h.SetTitle(resources.GetString(Resource.String.CustomMergingTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.CustomMergingDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_merging);
                    break;
                case 17:
                    h.SetTitle(resources.GetString(Resource.String.UnboundTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.UnboundDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_headers);
                    break;
                case 18:
                    h.SetTitle(resources.GetString(Resource.String.OnDemandTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.OnDemandDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_loading);
                    break;
                case 19:
                    h.SetTitle(resources.GetString(Resource.String.CustomAppearanceTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.CustomAppearanceDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_customAppearance);
                    break;
                case 20:
                    h.SetTitle(resources.GetString(Resource.String.NewRowTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.NewRowDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_newRow);
                    break;
                case 21:
                    h.SetTitle(resources.GetString(Resource.String.CheckListTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.CheckListDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_checkList);
                    break;
                case 22:
                    h.SetTitle(resources.GetString(Resource.String.CustomSortIconTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.CustomSortIconDescription));
                    h.SetIcon(Resource.Drawable.flexgrid_customSort);
                    break;
                case 23:
                    h.SetTitle(resources.GetString(Resource.String.ExportTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.ExportDescription));
                    h.SetIcon(Resource.Drawable.export_grid);
                    break;
                case 24:
                    h.SetTitle(resources.GetString(Resource.String.SummaryRowTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.SummaryRowDescription));
                    h.SetIcon(Resource.Drawable.flexgrid);
                    break;
                case 25:
                    h.SetTitle(resources.GetString(Resource.String.TransposedGridTitle));
                    h.SetSubtitle(resources.GetString(Resource.String.TransposedGridDescription));
                    h.SetIcon(Resource.Drawable.flexgrid);
                    break;
            }
        }
    }

    public class FlexGridSampleItemClickedEventArgs : EventArgs
    {
        public int Position { get; set; }
    }

    internal class FlexGridSamplesViewHolder : RecyclerView.ViewHolder
    {
        private TextView _title;
        private TextView _subTitle;
        private ImageView _icon;

        public FlexGridSamplesViewHolder(View itemView)
            : base(itemView)
        {
            _title = itemView.FindViewById<TextView>(Resource.Id.Title);
            _subTitle = itemView.FindViewById<TextView>(Resource.Id.Subtitle);
            _icon = itemView.FindViewById<ImageView>(Resource.Id.Icon);
        }

        internal void SetTitle(string title)
        {
            _title.Text = title;
        }

        internal void SetSubtitle(string title)
        {
            _subTitle.Text = title;
        }

        internal void SetIcon(int resourceId)
        {
            _icon.SetImageResource(resourceId);
        }
    }
}

