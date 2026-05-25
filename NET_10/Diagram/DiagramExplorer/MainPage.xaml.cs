namespace DiagramExplorer
{
#pragma warning disable 1591
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
            if (e.CurrentSelection.Count>0 && e.CurrentSelection[0] is PageDataViewModel pageData)
            {
                var page = Activator.CreateInstance(pageData.Type) as Page;
                await Navigation.PushAsync(page);

                view.SelectedItem = null;
            }
        }
    }
}
