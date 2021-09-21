using AppGham.Helpers;
using AppGham.Views;
using Xamarin.Forms;

namespace AppGham
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());
            NavigationService.Navigation = MainPage.Navigation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
