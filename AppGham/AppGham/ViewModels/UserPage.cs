using AppGham.Services;
using AppGham.Shared;
using System.Threading.Tasks;

namespace AppGham.ViewModels
{
    public class UserPage : BaseViewModel
    {
        private readonly UserService _userService;
        private readonly int _userId;

        public UserPage(int userId)
        {
            _userId = userId;
            _userService = new UserService();
        }

        public IUser User { get; set; }

        public async Task LoadUserAsync()
        {
            User = await _userService.GetUserAsync(_userId);
        }
    }
}
