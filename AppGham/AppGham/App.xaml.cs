using AppGham.Views;
using Xamarin.Forms;

namespace AppGham
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new UserRegistration();
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
