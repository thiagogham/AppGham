using Xamarin.Forms;

namespace AppGham.Controls
{
    public class CustomEntry : StackLayout
    {
        public CustomEntry()
        {
            var _entry = new Entry();
            _entry.SetBinding(Entry.TextProperty, new Binding(nameof(EntryText), source: this));
            _entry.SetBinding(Entry.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));
            _entry.SetBinding(Entry.IsPasswordProperty, new Binding(nameof(IsPassword), source: this));
            var frame = new Frame
            {
                Style = (Style)Application.Current.Resources["CustomEntryStyle"],
                Content = _entry
            };
            Children.Add(frame);
        }

        public string EntryText
        {
            get => (string)GetValue(EntryTextProperty);
            set => SetValue(EntryTextProperty, value);
        }

        public static readonly BindableProperty EntryTextProperty = BindableProperty.Create(
           nameof(EntryText),
           typeof(string),
           typeof(CustomEntry),
           string.Empty,
           BindingMode.TwoWay);

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
           "Placeholder",
           typeof(string),
           typeof(CustomEntry),
           string.Empty,
           BindingMode.TwoWay);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
           "IsPassword",
           typeof(bool),
           typeof(CustomEntry),
           false,
           BindingMode.TwoWay);
    }
}
