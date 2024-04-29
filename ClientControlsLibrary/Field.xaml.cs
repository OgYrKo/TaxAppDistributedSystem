using System.Windows;
using System.Windows.Controls;

namespace ClientControlsLibrary
{
    /// <summary>
    /// Interaction logic for Field.xaml
    /// </summary>
    public partial class Field : UserControl
    {
        public Field()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty HeaderProperty =
       DependencyProperty.Register("Header", typeof(string), typeof(Field), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TextProperty =
       DependencyProperty.Register("Text", typeof(string), typeof(Field), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty TipProperty =
       DependencyProperty.Register("Tip", typeof(string), typeof(Field), new PropertyMetadata(string.Empty));

        public static new readonly DependencyProperty IsEnabledProperty =
      DependencyProperty.Register("IsEnabled", typeof(bool), typeof(Field), new PropertyMetadata(false));

        public new bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public string Tip
        {
            get { return (string)GetValue(TipProperty); }
            set { SetValue(TipProperty, value); }
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
    }
}
