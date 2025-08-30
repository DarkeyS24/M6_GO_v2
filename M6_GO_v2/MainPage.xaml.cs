namespace M6_GO_v2
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            progressBar.ProgressTo(0.1, 1000, Easing.Linear);
            progressBar.ProgressTo(0.5, 1000, Easing.Linear);
            progressBar.ProgressTo(0.6, 1000, Easing.Linear);
            progressBar.ProgressTo(0.7, 800, Easing.Linear);
            progressBar.ProgressTo(1, 800, Easing.Linear);
            Thread.Sleep(400);
            Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
