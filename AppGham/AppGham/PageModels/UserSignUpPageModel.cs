using AppGham.Extensions;
using AppGham.Services.Interfaces;
using AppGham.Validations;
using System;
using System.Threading.Tasks;

namespace AppGham.PageModels
{
    public class UserSignUpPageModel : UserEditorBase
    {
        public UserSignUpPageModel(IUserService userService) : base(userService, new UserValidator(UserPageValidator.UserSignUpPage))
        {

        }
        
        public override async Task SaveAsync()
        {
            try
            {
                var user = await _userService.GetUserAsync(User.Email);
                if(user != null)
                {
                    await CoreMethods.DisplayAlert("User SignUp", "Email already registered!", "Ok");
                    return;
                }

                await _userService.AddUserAsync(User);
                await CoreMethods.PushPageModelWithNewNavigation<UserPageModel>(User);
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }
    }
}
