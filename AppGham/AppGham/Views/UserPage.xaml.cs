using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGham.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserPage : ContentPage
    {
        private readonly ViewModels.UserPage _userPage;

        public UserPage(int userId)
        {
            InitializeComponent();
            _userPage = new ViewModels.UserPage(userId);
            BindingContext = _userPage;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await _userPage.LoadUserAsync();
        }
    }
}