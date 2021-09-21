using AppGham.Extensions;
using AppGham.Helpers;
using AppGham.Services;
using AppGham.Shared;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;

namespace AppGham.ViewModels
{
    public class LoginPage : UserEditorBase
    {
        public LoginPage() : base(new UserService(), new DialogService())
        {
            SignUpCommand = new AsyncCommand(async () => await NavigationService.Navigation.PushAsync(new Views.UserSignUp()));
        }

        public override IUser User { get; set; }

        public AsyncCommand SignUpCommand { get; }

        bool LoginIsEnabled => !string.IsNullOrWhiteSpace(User.Email) ||
                               !string.IsNullOrWhiteSpace(User.Password);

        public override async Task SaveAsync()
        {
            try
            {
                if (!LoginIsEnabled)
                    return;

                var logged = await _userService.LoginUserAsync(User.Email, User.Password);
                if(logged)
                {
                    User = await _userService.GetUserAsync(User.Email);
                    await NavigationService.Navigation.PushAsync(new Views.UserPage(User));
                }
                else
                    await _dialogService.DisplayAlert("User Login", "Wrong email or password!", "Ok");
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }
    }
}
