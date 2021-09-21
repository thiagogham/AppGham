using AppGham.Extensions;
using AppGham.Helpers;
using AppGham.Services;
using AppGham.Shared;
using AppGham.Shared.Helpers;
using System;
using System.Threading.Tasks;

namespace AppGham.ViewModels
{
    public class UserSignUp : UserEditorBase
    {
        public UserSignUp() : base(new UserService(), new DialogService())
        {

        }

        bool RegisterIsEnabled => !string.IsNullOrWhiteSpace(User.Name) ||
                                  !string.IsNullOrWhiteSpace(User.Email) ||
                                  !string.IsNullOrWhiteSpace(User.Password) ||
                                  (User.Name.Length > 5 && User.Email.Length > 5 && User.Password.Length > 5);

        public override IUser User { get; set; }

        public override async Task SaveAsync()
        {
            try
            {
                if (!RegisterIsEnabled)
                    return;

                User.Password = Utils.MD5Hash(User.Password);
                await _userService.AddUserAsync(User);
                await _dialogService.DisplayAlert("User Registration", "User saved successfully!", "Ok");
                await NavigationService.Navigation.PushAsync(new Views.UserPage(User));
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }
    }
}
