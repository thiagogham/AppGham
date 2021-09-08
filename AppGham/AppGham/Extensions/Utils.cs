using System;
using Xamarin.Forms;

namespace AppGham.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ErrorAlert(this Exception ex)
        {
            Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");
        }
    }
}
