using AppGham.Extensions;
using AppGham.Helpers;
using AppGham.Services;
using AppGham.Shared;
using System;
using System.Threading.Tasks;

namespace AppGham.ViewModels
{
    public class UserEditor : UserEditorBase
    {
        public UserEditor(IUser user) : base(new UserService())
        {
            User = user;
        }

        public override IUser User { get; set; }

        bool UpdateIsEnabled => !string.IsNullOrWhiteSpace(User.Name) ||
                                !string.IsNullOrWhiteSpace(User.Email) ||
                                !string.IsNullOrWhiteSpace(PhotoPath);

        public override async Task SaveAsync()
        {
            try
            {
                if (!UpdateIsEnabled)
                    return;

                await _userService.UpdateUserAsync(User);
                await NavigationService.Navigation.PushAsync(new Views.UserPage(User));
            }
            catch (Exception ex)
            {
                ex.ErrorAlert();
            }
        }
    }
}
