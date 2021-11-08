using Xamarin.Forms;

namespace AppGham.Controls
{
    public class UserPhotoControl : StackLayout
    {
        public UserPhotoControl()
        {
            Image image = new Image
            {
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
                Aspect = Aspect.Fill
            };
            image.SetBinding(Image.SourceProperty, new Binding(nameof(Source), source: this));
            Frame frame = new Frame
            {
                Style = (Style)Application.Current.Resources["UserPhotoControlStyle"],
                Content = image
            };
            Children.Add(frame);
        }

        public string Source
        {
            get => (string)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public static readonly BindableProperty SourceProperty = BindableProperty.Create(
           nameof(Source),
           typeof(string),
           typeof(UserPhotoControl),
           string.Empty,
           BindingMode.TwoWay);
    }
}
