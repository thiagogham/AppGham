using AppGham.Extensions;
using AppGham.Services.Interfaces;
using AppGham.Shared;
using AppGham.Shared.Models;
using AppGham.Validations;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;

namespace AppGham.PageModels
{
    public class LoginPageModel : UserEditorBase
    {
        public LoginPageModel(IUserService userService) : base(userService, new UserValidator(UserPageValidator.LoginPage))
        {
            SignUpCommand = new AsyncCommand(async () => await CoreMethods.PushPageModel<UserSignUpPageModel>());
            KeepLogged = UserSettings.GetKeepLogged();
        }

        public AsyncCommand SignUpCommand { get; }

        private bool _keepLogged;
        public bool KeepLogged
        {
            get => _keepLogged;
            set
            {
                _keepLogged = value;
                UserSettings.SetKeepLogged(value);
            }
        }
        
        public override void Init(object initData)
        {
            User = new User();
            UserSettings.SetIsLogged(false);
            CoreMethods.RemoveFromNavigation();

            if (initData == null && KeepLogged)
                _ = LoginUserByUser();
            else
                _ = UserSettings.RemoveUserLogged();
        }

        public override async Task SaveAsync()
        {
            try
            {
                var user = await _userService.GetUserAsync(User.Email);

                if (user == null)
                {
                    await CoreMethods.DisplayAlert("User Login", "Email not registered!", "Ok");
                    return;
                }

                if (await _userService.LoginUserAsync(User.Email, User.Password))
                {
                    User = user;
                    await LoginSuccess();
                    return;
                }

                await CoreMethods.DisplayAlert("User Login", "Wrong password!", "Ok");
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }

        private async Task LoginUserByUser()
        {
            try
            {
                User = await UserSettings.GetUserLogged();
                if (await _userService.LoginUserAsync(User))
                    await LoginSuccess();
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }

        private async Task LoginSuccess()
        {
            UserSettings.SetIsLogged(true);
            if (KeepLogged)
                await UserSettings.SetUserLogged(User);

            await CoreMethods.PushPageModelWithNewNavigation<UserPageModel>(User);
        }
    }
}
