namespace M6_GO_v2
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(IdosoPage), typeof(IdosoPage));
            Routing.RegisterRoute(nameof(CuidadorPage), typeof(CuidadorPage));
            Routing.RegisterRoute(nameof(NovoAtendimentoPage), typeof(NovoAtendimentoPage));
        }
    }
}
