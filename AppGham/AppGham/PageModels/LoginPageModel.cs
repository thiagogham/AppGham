using AppGham.Extensions;
using AppGham.Services.Interfaces;
using AppGham.Shared.Models;
using MvvmHelpers.Commands;
using System;
using System.Threading.Tasks;

namespace AppGham.PageModels
{
    public class LoginPageModel : UserEditorBase
    {
        public LoginPageModel(IUserService userService) : base(userService)
        {
            SignUpCommand = new AsyncCommand(async () => await CoreMethods.PushPageModel<UserSignUpPageModel>());
        }

        public AsyncCommand SignUpCommand { get; }

        bool LoginIsEnabled => !string.IsNullOrWhiteSpace(User.Email) ||
                               !string.IsNullOrWhiteSpace(User.Password);

        public override void Init(object initData)
        {
            User = new User();
        }

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
                    await CoreMethods.PushPageModelWithNewNavigation<UserPageModel>(User);
                    return;
                }
                
                await CoreMethods.DisplayAlert("User Login", "Wrong email or password!", "Ok");
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }
    }
}
