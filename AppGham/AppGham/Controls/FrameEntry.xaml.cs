using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppGham.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FrameEntry
    {
        public FrameEntry()
        {
            InitializeComponent();
        }

        public static readonly BindableProperty TextProperty = BindableProperty.Create(
           "Text",
           typeof(string),
           typeof(FrameEntry),
           "");

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
           "Placeholder",
           typeof(string),
           typeof(FrameEntry),
           "");

        public string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public static readonly BindableProperty IsPasswordProperty = BindableProperty.Create(
           "IsPassword",
           typeof(bool),
           typeof(FrameEntry),
           false);

        public bool IsPassword
        {
            get => (bool)GetValue(IsPasswordProperty);
            set => SetValue(IsPasswordProperty, value);
        }
    }
}