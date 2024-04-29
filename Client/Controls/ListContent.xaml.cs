using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace Client.Controls
{
    /// <summary>
    /// Interaction logic for ListContent.xaml
    /// </summary>
    public partial class ListContent : UserControl
    {
        public ListContent()
        {
            InitializeComponent();
        }

        public delegate void SelctionChangedHandler(int id);

        public event SelctionChangedHandler OnSelctionChanged;

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource",typeof(IEnumerable),typeof(ListContent),new PropertyMetadata(null));
        public static readonly DependencyProperty SelectedIndexProperty =
        DependencyProperty.Register("SelectedIndex", typeof(int), typeof(ListContent), new PropertyMetadata(-1));

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private void TaskActions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (OnSelctionChanged != null)
                OnSelctionChanged(((ListView)sender).SelectedIndex);
        }
    }
}
