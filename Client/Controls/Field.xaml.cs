using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace Client.Controls
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

        public event TextCompositionEventHandler PreviewTxtInput;

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

        public static new readonly DependencyProperty ColorProperty =
      DependencyProperty.Register("Color", typeof(Brush), typeof(Field), new PropertyMetadata(null));

        public new Brush Color
        {
            get { return (Brush)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            PreviewTxtInput?.Invoke(sender, e);
        }
    }
}
