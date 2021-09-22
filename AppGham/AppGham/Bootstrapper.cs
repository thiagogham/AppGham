using AppGham.Services;
using AppGham.Services.Interfaces;
using FreshMvvm;

namespace AppGham
{
    public static class Bootstrapper
    {
        public static void RegisterServices()
        {
            FreshIOC.Container.Register<IUserService, UserService>();
        }
    }
}
