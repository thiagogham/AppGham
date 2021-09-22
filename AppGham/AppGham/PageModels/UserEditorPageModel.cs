using AppGham.Extensions;
using AppGham.Services.Interfaces;
using System;
using System.Threading.Tasks;

namespace AppGham.PageModels
{
    public class UserEditorPageModel : UserEditorBase
    {
        public UserEditorPageModel(IUserService userService) : base(userService)
        {

        }

        bool UpdateIsEnabled => !string.IsNullOrWhiteSpace(User.Name) ||
                                !string.IsNullOrWhiteSpace(User.Email);

        public override async Task SaveAsync()
        {
            try
            {
                if (!UpdateIsEnabled)
                    return;

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
