using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;

namespace ClientControlsLibrary
{
    public partial class SubMenuItem : UserControl
    {
        public SubMenuItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(SubMenuItem), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty KindProperty = DependencyProperty.Register("Kind", typeof(PackIconKind), typeof(SubMenuItem), new PropertyMetadata(PackIconKind.Connection, OnKindPropertyChanged));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public PackIconKind Kind
        {
            get { return (PackIconKind)GetValue(KindProperty); }
            set
            {
                SetValue(KindProperty, value);
            }
        }

        private static void OnKindPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            SubMenuItem subMenuItem = (SubMenuItem)d;
            PackIconKind newKind = (PackIconKind)e.NewValue;

            if (newKind == PackIconKind.None)
            {
                subMenuItem.packIcon.Visibility = Visibility.Collapsed;
                subMenuItem.textBlock.HorizontalAlignment = HorizontalAlignment.Center;
                subMenuItem.textBlock.Margin = new Thickness();
            }
        }
    }
}
