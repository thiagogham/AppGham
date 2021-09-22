using AppGham.PageModels;
using FreshMvvm;
using Xamarin.Forms;

namespace AppGham
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Bootstrapper.RegisterServices();
            MainPage = new FreshNavigationContainer(FreshPageModelResolver.ResolvePageModel<LoginPageModel>());
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
