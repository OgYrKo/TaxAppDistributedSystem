using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace ClientControlsLibrary
{
    public partial class SubMenu : UserControl
    {

        public SubMenu()
        {
            InitializeComponent();
            DataContext = this;
        }

        private int PreviousSelectedIndex;

        public delegate void SelctionChangedHandler(int id);

        public event SelctionChangedHandler OnSelctionChanged;

        public static readonly DependencyProperty HeaderProperty =
        DependencyProperty.Register("Header", typeof(string), typeof(SubMenu), new PropertyMetadata(string.Empty));

        public static readonly DependencyProperty ItemsSourceProperty =
        DependencyProperty.Register("ItemsSource", typeof(IEnumerable), typeof(SubMenu), new PropertyMetadata(null));

        public static readonly DependencyProperty SelectionImportantProperty =
        DependencyProperty.Register("SelectionImportant", typeof(bool), typeof(SubMenu), new PropertyMetadata(false));

        public static readonly DependencyProperty SelectedIndexProperty =
        DependencyProperty.Register("SelectedIndex", typeof(int), typeof(SubMenu), new PropertyMetadata(-1));

        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public bool SelectionImportant
        {
            get { return (bool)GetValue(SelectionImportantProperty); }
            set { SetValue(SelectionImportantProperty, value); }
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set 
            { 
                SetValue(ItemsSourceProperty, value);
                listView.Items.Refresh();
            }
        }

        public string Header
        {
            get { return (string)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public void ListActions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ListView)sender).SelectedIndex == -1) return;

            if (OnSelctionChanged != null && ((SubMenuItem)((ListView)sender).SelectedItem).IsEnabled)
            {
                PreviousSelectedIndex = ((ListView)sender).SelectedIndex;
                OnSelctionChanged(PreviousSelectedIndex);

            }
            else
            {
                ((ListView)sender).SelectedIndex = PreviousSelectedIndex;
            }

            if (!SelectionImportant)
                ((ListView)sender).SelectedIndex = -1;
        }
    }
}
