using Xamarin.Forms;

namespace AppGham.Controls
{
    public class EntryControl : StackLayout
    {
        public EntryControl()
        {
            Entry entry = new Entry();
            entry.SetBinding(Entry.TextProperty, new Binding(nameof(EntryText), source: this));
            entry.SetBinding(Entry.PlaceholderProperty, new Binding(nameof(Placeholder), source: this));
            entry.SetBinding(Entry.IsPasswordProperty, new Binding(nameof(IsPassword), source: this));
            var frame = new Frame
            {
                Style = (Style)Application.Current.Resources["EntryControlStyle"],
                Content = entry
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
           typeof(EntryControl),
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
           typeof(EntryControl),
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
           typeof(EntryControl),
           false,
           BindingMode.TwoWay);
    }
}
