using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Client.Controls
{
    public partial class ListTable : UserControl
    {
        public ListTable()
        {
            InitializeComponent();
        }

        public delegate void Click(object o);
        public event Click OnClick;

        public delegate void GenerateColumns(DataGridAutoGeneratingColumnEventArgs e);
        public event GenerateColumns OnGenerateColumns;

        public static readonly DependencyProperty ItemSourceProperty = DependencyProperty.Register("ItemSource", typeof(IEnumerable), typeof(ListTable), new PropertyMetadata(null));

        public IEnumerable ItemSource
        {
            get { return (IEnumerable)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }

        private void DataGridRow_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OnClick != null) OnClick(sender);
        }

        private void Data_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            if (OnGenerateColumns != null) OnGenerateColumns(e);
        }
    }
}
