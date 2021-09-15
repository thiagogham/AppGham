using AppGham.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGham.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserEditor : ContentPage
    {
        public UserEditor(IUser user)
        {
            InitializeComponent();
            BindingContext = new ViewModels.UserEditor(user);
        }
    }
}