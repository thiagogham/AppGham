using AppGham.Extensions;
using AppGham.Services.Interfaces;
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
        }

        public AsyncCommand SignUpCommand { get; }

        public override void Init(object initData)
        {
            User = new User();
            CoreMethods.RemoveFromNavigation();
            UserSettings.Clean();
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
                    User = await _userService.GetUserAsync(User.Email);
                    await UserSettings.SetIsLogged(User);
                    await CoreMethods.PushPageModelWithNewNavigation<UserPageModel>(User);
                    return;
                }

                await CoreMethods.DisplayAlert("User Login", "Wrong password!", "Ok");
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }
    }
}
