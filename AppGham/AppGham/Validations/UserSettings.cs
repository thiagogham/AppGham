using AppGham.Extensions;
using AppGham.Shared;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppGham.Validations
{
    public static class UserSettings
    {
        public static async Task SetIsLogged(IUser user)
        {
            try
            {
                await SecureStorage.SetAsync("IsLoggedIn", "true");
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }

        public static async Task<bool> CheckIsLogged(IUser user)
        {
            try
            {
                var retorno =  bool.Parse(await SecureStorage.GetAsync("IsLoggedIn"));

                return retorno;
            }
            catch
            {
                return false;
            }
        }

        public static void Clean() => SecureStorage.RemoveAll();
    }
}
