namespace M6_GO_v2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await progressBar.ProgressTo(0.1, 1000, Easing.Linear);
            await progressBar.ProgressTo(0.5, 1000, Easing.Linear);
            await progressBar.ProgressTo(0.6, 1000, Easing.Linear);
            await progressBar.ProgressTo(0.7, 800, Easing.Linear);
            await progressBar.ProgressTo(1, 800, Easing.Linear);
            await Task.Delay(400);
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
