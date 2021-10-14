using AppGham.Extensions;
using AppGham.Services.Interfaces;
using AppGham.Validations;
using System;
using System.Threading.Tasks;

namespace AppGham.PageModels
{
    public class UserEditorPageModel : UserEditorBase
    {
        public UserEditorPageModel(IUserService userService) : base(userService, new UserValidator(UserPageValidator.UserEditorPage))
        {

        }

        public override async Task SaveAsync()
        {
            try
            {
                await _userService.UpdateUserAsync(User);
                await CoreMethods.PushPageModelWithNewNavigation<UserPageModel>(User);
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }
    }
}
