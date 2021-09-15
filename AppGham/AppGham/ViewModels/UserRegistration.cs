using AppGham.Extensions;
using AppGham.Helpers;
using AppGham.Interfaces;
using AppGham.Services;
using AppGham.Shared;
using AppGham.Shared.Models;
using System;
using System.Threading.Tasks;

namespace AppGham.ViewModels
{
    public class UserRegistration : UserEditorBase
    {
        private readonly IDialogService _dialogService;

        //public UserRegistrationViewModel(IUserService userService, IDialogService dialogService, INavigation navigationService)
        public UserRegistration() : base(new UserService())
        {
            _dialogService = new DialogService();
            User = new User();
        }

        bool RegisterIsEnabled => !string.IsNullOrWhiteSpace(User.Name) ||
                                  !string.IsNullOrWhiteSpace(User.Email) ||
                                  !string.IsNullOrWhiteSpace(User.Photo);

        public override IUser User { get; set; }

        public override async Task SaveAsync()
        {
            try
            {
                if (!RegisterIsEnabled)
                    return;

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
