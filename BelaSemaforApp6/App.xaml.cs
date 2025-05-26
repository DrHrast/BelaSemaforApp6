namespace BelaSemaforApp6
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            UserAppTheme = AppTheme.Light;

            MainPage = new AppShell();
        }
    }
}
