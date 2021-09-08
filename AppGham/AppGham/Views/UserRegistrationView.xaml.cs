using AppGham.Services;
using AppGham.ViewModels;
using Xamarin.Forms;

namespace AppGham.Views
{
    public partial class UserRegistrationView : ContentPage
    {
        public UserRegistrationView()
        {
            BindingContext = new UserRegistrationViewModel();
            InitializeComponent();
        }
    }
}