namespace FlexChartExplorer
{
   public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            Application.Current.RequestedThemeChanged += (s, e) =>
            {
                view.ItemsSource = null;
                view.ItemsSource = PageDataViewModel.All;
            }; 
        }

        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var pageData = e.CurrentSelection.FirstOrDefault() as PageDataViewModel;

                if (pageData != null)
                {
                    var page = Activator.CreateInstance(pageData.Type) as Page;
                    await Navigation.PushAsync(page);

                    view.SelectedItem = null;
                }
            }
        }
    }
}
