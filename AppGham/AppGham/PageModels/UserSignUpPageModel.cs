using AppGham.Extensions;
using AppGham.Services.Interfaces;
using AppGham.Shared.Helpers;
using System;
using System.Threading.Tasks;

namespace AppGham.PageModels
{
    public class UserSignUpPageModel : UserEditorBase
    {
        public UserSignUpPageModel(IUserService userService) : base(userService)
        {

        }

        bool RegisterIsEnabled => !string.IsNullOrWhiteSpace(User.Name) &&
                                  !string.IsNullOrWhiteSpace(User.Email) &&
                                  !string.IsNullOrWhiteSpace(User.Password) &&
                                  User.Name.Length > 5 && 
                                  User.Email.Length > 5 && 
                                  User.Password.Length > 5;

        public override async Task SaveAsync()
        {
            try
            {
                if (!RegisterIsEnabled)
                    return;

                User.Password = Utils.MD5Hash(User.Password);
                await _userService.AddUserAsync(User);
                await CoreMethods.DisplayAlert("User Sign Up", "User saved successfully!", "Ok");
                await CoreMethods.PushPageModelWithNewNavigation<UserPageModel>(User);
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }
    }
}
