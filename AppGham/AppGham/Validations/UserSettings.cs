using AppGham.Extensions;
using AppGham.Shared;
using AppGham.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace AppGham.Validations
{
    public static class UserSettings
    {
        public static void SetKeepLogged(bool value) => Preferences.Set("KeepLogged", value);

        public static bool GetKeepLogged() => Preferences.Get("KeepLogged", false);

        public static void SetIsLogged(bool value) => Preferences.Set("IsLoggedIn", value);

        public static bool CheckIsLogged() => Preferences.Get("IsLoggedIn", false);

        public static async Task SetUserLogged(IUser user)
        {
            try
            {
                await SecureStorage.SetAsync("UserLogged", JsonConvert.SerializeObject((User)user));
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }

        public static async Task RemoveUserLogged()
        {
            try
            {
                await SecureStorage.SetAsync("UserLogged", string.Empty);
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }

        public static async Task<IUser> GetUserLogged()
        {
            try
            {
                var user = await SecureStorage.GetAsync("UserLogged");
                if(!string.IsNullOrWhiteSpace(user) && user != "null")
                {
                    return JsonConvert.DeserializeObject<User>(user);
                }
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }

            return null;
        }

    }
}
