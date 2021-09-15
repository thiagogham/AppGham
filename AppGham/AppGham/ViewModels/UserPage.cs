using AppGham.Helpers;
using AppGham.Services;
using AppGham.Shared;
using MvvmHelpers.Commands;
using System.Threading.Tasks;

namespace AppGham.ViewModels
{
    public class UserPage : BaseViewModel
    {
        private UserService _userService;
        private readonly int _userId;

        public UserPage(int userId)
        {
            _userId = userId;
            Init();
        }

        public UserPage(IUser user)
        {
            User = user;
            Init();
        }

        private void Init()
        {
            _userService = new UserService();
            EditCommand = new AsyncCommand(EditUserAsync);
        }

        public AsyncCommand EditCommand { get; private set; }

        public IUser User { get; set; }

        public async Task LoadUserAsync()
        {
            if(_userId > 0)
                User = await _userService.GetUserAsync(_userId);
        }

        private async Task EditUserAsync()
        {
            await NavigationService.Navigation.PushAsync(new Views.UserEditor(User));
        }
    }
}
