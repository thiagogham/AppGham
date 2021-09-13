using AppGham.Interfaces;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace AppGham.Helpers
{
    public class DialogService : IDialogService
    {
        Page _currentPage;
        Page CurrentPage 
        {
            get
            {
                if (_currentPage == null)
                    _currentPage = Application.Current.MainPage;
                return _currentPage;
            }
        }

        public async Task DisplayAlert(string title, string message, string cancel = "Cancel") => 
            await CurrentPage.DisplayAlert(title, message, cancel);

        public async Task<bool> DisplayAlert(string title, string message, string accept = "OK", string cancel = "Cancel") =>
            await CurrentPage.DisplayAlert(title, message, accept, cancel);

        public async Task<string> DisplayPromptAsync(string title, string message, string accept = "OK", string cancel = "Cancel", string placeholder = null, int maxLength = -1, string initialValue = "") =>
            await CurrentPage.DisplayPromptAsync(title, message, accept, cancel, placeholder, maxLength, initialValue: initialValue);
    }
}
