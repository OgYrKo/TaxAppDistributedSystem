using System.Windows;
using System.Windows.Controls;

namespace ClientControlsLibrary
{
    /// <summary>
    /// Interaction logic for CardContent.xaml
    /// </summary>
    public partial class CardContent : UserControl
    {
        public CardContent()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty CardContentProperty =
            DependencyProperty.Register("WrapContent", typeof(object), typeof(Page));

        public object WrapContent
        {
            get { return GetValue(CardContentProperty); }
            set { SetValue(CardContentProperty, value); }
        }
    }
}
