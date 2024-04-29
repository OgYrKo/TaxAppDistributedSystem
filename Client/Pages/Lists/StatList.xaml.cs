using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using Client.Controls;
using DBClassesLibrary;
using InterfacesLibrary;
using Client.Classes;
using System.Collections;
using System.Windows.Media;

namespace Client.Pages.Lists
{
    enum Chart
    {
        Relatively,
        TotalComparatively,
        Comparatively,
        MinTax,
        AvgTax,
        MaxTax
    }

    struct Relatively
    {
        public double Total;
        public double Actual;
    }

    public partial class StatList : Page
    {

        Dictionary<string, List<Relatively>> RelativelyData;
        Dictionary<string, (List<Income>, List<Tax>)[]> LinearData;
        List<Income> Incomes;
        List<Tax> Taxes;
        string TotalName = "Загалом";
        public Func<ChartPoint, string> PointLabel { get; set; }
        private IGraphics Chanel;
        private Chart CurrentChart;


        public StatList()
        {
            SetChanel();
            PointLabel = chartPoint => string.Format("{0} грн. ({1:P})", chartPoint.Y, chartPoint.Participation);
            InitializeComponent();
            DataContext = this;
            SetAllDataFromServer();
            SetChartMenu();
        }

        private void SetChanel()
        {
            try
            {
                Connection<IGraphics> connection = new Connection<IGraphics>("IGraphics");
                Chanel = connection.channel;
                Chanel.SetUser(CurrentUser.Instance().Login, CurrentUser.Instance().Password);
            }
            catch (Exception e)
            {
                MainWindowComunication.ShowError(e.Message);
            }
        }

        private void SetAllDataFromServer()
        {
            List<Quarterincome> quarterincomes = null;
            List<Quartertax> quartertaxes = null;

            Parallel.Invoke(
                () => Incomes = Chanel.GetDailyIncomes(),
                () => Taxes = Chanel.GetDailyTaxes(),
                () => quarterincomes = Chanel.GetQuarterIncomes(),
                () => quartertaxes = Chanel.GetQuarterTaxes()
            );
            SetRelativelyData(quarterincomes, quartertaxes);
            SetLinearData(Incomes, Taxes);
        }

        private void SetLinearData(List<Income> incomes, List<Tax> taxes)
        {
            LinearData = new Dictionary<string, (List<Income>, List<Tax>)[]>();

            for (int i = 0; i < incomes.Count; i++)
            {

                string currentYear = ((DateTime)incomes[i].Date).ToString("yyyy");
                int currentQuartal = GetQuartalByDate((DateTime)incomes[i].Date);
                if (!LinearData.TryGetValue(currentYear, out (List<Income>, List<Tax>)[] array))
                {
                    (List<Income>, List<Tax>)[] newArray = new (List<Income>, List<Tax>)[4];
                    for (int j = 0; j < newArray.Length; j++)
                    {
                        newArray[j].Item1 = new List<Income>();
                        newArray[j].Item2 = new List<Tax>();
                    }
                    LinearData[currentYear] = newArray;
                }

                LinearData[currentYear][currentQuartal].Item1.Add(incomes[i]);
                LinearData[currentYear][currentQuartal].Item2.Add(taxes[i]);
            }
        }

        private int GetQuartalByDate(DateTime date) => (date.Month - 1) / 3;

        private void SetRelativelyData(List<Quarterincome> quarterincomes, List<Quartertax> quartertaxes)
        {
            RelativelyData = new Dictionary<string, List<Relatively>>();
            foreach (var item in quarterincomes)
            {
                int quarterIndex = Convert.ToInt32(item.Quarter);
                if (RelativelyData.TryGetValue(Convert.ToString(item.Year), out List<Relatively> list))
                {
                    Relatively relatively = list[quarterIndex];
                    relatively.Actual = Convert.ToDouble(item.Quarteramount);
                    list[quarterIndex] = relatively;
                }
                else
                {
                    list = new List<Relatively>();
                    for (int i = 0; i < 5; i++)
                    {
                        double actual = 0;
                        if (i == quarterIndex)
                        {
                            actual = Convert.ToDouble(item.Quarteramount);
                        }
                        list.Add(new Relatively() { Total = 0, Actual = actual });
                    }
                    RelativelyData[Convert.ToString(item.Year)] = list;
                }
            }

            foreach (var item in quartertaxes)
            {
                int quarterIndex = Convert.ToInt32(item.Quarter);
                if (RelativelyData.TryGetValue(Convert.ToString(item.Year), out List<Relatively> list))
                {
                    Relatively relatively = list[quarterIndex];
                    relatively.Total = Convert.ToDouble(item.Quarteramount);
                    list[quarterIndex] = relatively;
                }
                else
                {
                    list = new List<Relatively>();
                    for (int i = 0; i < 5; i++)
                    {
                        double total = 0;
                        if (i == quarterIndex)
                        {
                            total = Convert.ToDouble(item.Quarteramount);
                        }
                        list.Add(new Relatively() { Total = total, Actual = 0 });
                    }
                    RelativelyData[Convert.ToString(item.Year)] = list;
                }
            }

            double globalTotal = 0, globalActual = 0;

            foreach (var dataByYear in RelativelyData)
            {
                double totalByYear = 0, actualByYear = 0;
                for (int i = 1; i < 5; i++)
                {
                    totalByYear += dataByYear.Value[i].Total;
                    actualByYear += dataByYear.Value[i].Actual;
                }
                dataByYear.Value[0] = new Relatively() { Actual = actualByYear, Total = totalByYear };
                globalTotal += totalByYear;
                globalActual += actualByYear;
            }
            RelativelyData[TotalName] = new List<Relatively>() { new Relatively() { Total = globalTotal, Actual = globalActual } };

        }

        private void SetChartMenu()
        {
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            foreach (Chart chart in Enum.GetValues(typeof(Chart)))
            {
                string itemName = GetNameByChartEnum(chart);
                if (itemName == null) continue;
                ListItem listItem = new ListItem();
                listItem.Text = itemName;
                ListViewItem listViewItem = (ListViewItem)listItem.Content;
                listViewItem.Margin = new Thickness(0);
                listViewItems.Add(listViewItem);
            }
            chartMenu.ItemsSource = listViewItems;
            chartMenu.SelectedIndex = 0;
        }

        private string GetNameByChartEnum(Chart chart)
        {
            switch (chart)
            {
                case Chart.Relatively:
                    return "Заповнення бюджету";
                case Chart.Comparatively:
                    return "Денні надходження";
                case Chart.TotalComparatively:
                    return "Зміни заповнення бюджету";
                default:
                    return null;

            }
        }

        private void SetYearMenu()
        {
            Dictionary<string, IEnumerable> Data;
            switch (CurrentChart)
            {
                case Chart.Relatively:
                    Data = RelativelyData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value as IEnumerable);
                    break;
                default:
                    Data = LinearData.ToDictionary(kvp => kvp.Key, kvp => kvp.Value as IEnumerable);
                    break;
            }
            List<SubMenuItem> listViewItems = new List<SubMenuItem>();
            foreach (var item in Data)
            {
                SubMenuItem subMenuItem = new SubMenuItem();
                subMenuItem.Text = item.Key;
                subMenuItem.Kind = MaterialDesignThemes.Wpf.PackIconKind.None;
                subMenuItem.MouseLeftButtonUp += subMenuItemClick;
                listViewItems.Add(subMenuItem);
            }
            yearsMenu.ItemsSource = listViewItems;
            yearsMenu.SelectedIndex = 0;
        }

        private void subMenuItemClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            List<string> list = new List<string>();
            switch (CurrentChart)
            {
                case Chart.Relatively:
                    list = RelativelyData.Keys.ToList();
                    break;
                default:
                    list = LinearData.Keys.ToList();
                    break;
            }

            SubMenuItem subMenuItem = (SubMenuItem)sender;
            yearsMenu.SelectedIndex = list.FindIndex(str => str.Equals(subMenuItem.Text));
        }

        private void SetDailyGraphic(string year, int quartal)
        {
            SetLinearGraphic(year, quartal, Chart.Comparatively);
        }

        private (double,double) GetTotalData(Income income,Tax tax)
        {
            return (Convert.ToDouble(income.Totalincome),Convert.ToDouble(tax.Totaltax));
        }

        private (double, double) GetData(Income income, Tax tax)
        {
            return (Convert.ToDouble(income.Incomeperday), Convert.ToDouble(tax.Taxday));
        }

        private void SetLinearGraphic(string year,int quartal, Chart chart)
        {
            // Создание коллекции серий
            SeriesCollection seriesCollection = new SeriesCollection();

            LineSeries series1 = new LineSeries
            {
                Title = "Налог",
                Values = new ChartValues<double>(),
                Stroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#28465A")
            };
            LineSeries series2 = new LineSeries
            {
                Title = "Сплата",
                Values = new ChartValues<double>()

            };
            List<string> dates = new List<string>();

            for (int quartalIndex = 0; quartalIndex < 4; quartalIndex++)
            {
                if (quartal != 0 && quartalIndex != quartal-1) continue;

                var currentQuartalData = LinearData[year][quartalIndex];
                for (int j = 0; j < currentQuartalData.Item1.Count; j++)
                {
                    (double, double) tuple=(double.NaN,double.NaN);
                    switch (chart)
                    {
                        case Chart.TotalComparatively:
                            tuple = GetTotalData(currentQuartalData.Item1[j], currentQuartalData.Item2[j]);
                            break;
                        case Chart.Comparatively:
                            tuple = GetData(currentQuartalData.Item1[j], currentQuartalData.Item2[j]);
                            break;
                        default:
                            tuple = (double.NaN, double.NaN);
                            break;
                    }
                    series1.Values.Add(tuple.Item1);
                    series2.Values.Add(tuple.Item2);
                    dates.Add(((DateTime)currentQuartalData.Item1[j].Date).ToString("dd.MM"));
                }
            }


            seriesCollection.Add(series1);
            seriesCollection.Add(series2);


            ///////////////////////////////////////////////////////


            Axis axis = new Axis();
            axis.Title = "Дні";
            axis.Labels = dates.ToArray();

            AxesCollection axes = new AxesCollection
            {
                axis
            };

            cartesianChart.AxisX = axes;
            cartesianChart.Series = seriesCollection;
        }

        private void SetTotalDailyGraphic(string year, int quartal)
        {
            SetLinearGraphic(year, quartal,Chart.TotalComparatively);
        }

        private bool CheckQuartelByYear(string year, int quarter)
        {
            if (quarter < 0) return false;
            if (!RelativelyData.TryGetValue(year, out List<Relatively> result)) return false;
            if (result == null) return false;

            return result.Count > quarter;
        }

        private void SetRelativelyGraphic(string year, int quarter)
        {
            if (!CheckQuartelByYear(year, quarter)) return;
            Relatively relatively;
            for (int i = quarter; i < 5; i++)
            {
                relatively = RelativelyData[year][i];
                if (relatively.Actual != 0 || relatively.Total != 0)
                {
                    RestSeries.Values = new ChartValues<double> { relatively.Total - relatively.Actual };
                    ActualSeries.Values = new ChartValues<double> { relatively.Actual };
                    break;
                }
            }

        }

        private void YearsMenuSelctionChanged(int index)
        {
            SetQuartelMenu(index);
        }

        private void SetQuartelMenu(int index)
        {
            quarterMenu.ItemsSource = null;
            switch (CurrentChart)
            {
                case Chart.Relatively:
                    SetRelativelyQuartalMenu(index);
                    break;
                case Chart.TotalComparatively:
                case Chart.Comparatively:
                    SetLinearQuartalMenu(index);
                    break;

            }
        }

        private void SetLinearQuartalMenu(int index)
        {
            var value = LinearData.ElementAt(index).Value;
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            for (int i = 0; i < 5; i++)
            {

                TextBlock textBlock = new TextBlock();

                if (i == 0)
                {
                    textBlock.Text = "Весь рік";
                }
                else
                {
                    if (value[i - 1].Item1.Count == 0) continue;
                    textBlock.Text = $"Квартал {i}";
                }

                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = textBlock;
                listViewItems.Add(listViewItem);
            }
            quarterMenu.ItemsSource = listViewItems;
            quarterMenu.SelectedIndex = 0;
        }

        private void SetRelativelyQuartalMenu(int id)
        {
            KeyValuePair<string, List<Relatively>> pair = RelativelyData.ElementAt(id);
            if (!pair.Key.Equals(TotalName))
            {

                List<Relatively> relativelies = pair.Value;
                List<ListViewItem> listViewItems = new List<ListViewItem>();
                for (int i = 0; i < relativelies.Count; i++)
                {
                    if (relativelies[i].Total == 0) continue;
                    TextBlock textBlock = new TextBlock();

                    if (i == 0)
                    {
                        textBlock.Text = "Весь рік";
                    }
                    else
                    {
                        textBlock.Text = $"Квартал {i}";
                    }

                    ListViewItem listViewItem = new ListViewItem();
                    listViewItem.Content = textBlock;
                    listViewItems.Add(listViewItem);
                }
                quarterMenu.ItemsSource = listViewItems;
                quarterMenu.SelectedIndex = 0;
            }
        }

        private void quarterMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (CurrentChart)
            {
                case Chart.Relatively:
                    SetRelativelyGraphic(RelativelyData.ElementAt(yearsMenu.SelectedIndex).Key, ((ListView)sender).SelectedIndex);
                    break;
                case Chart.TotalComparatively:
                    SetTotalDailyGraphic(LinearData.ElementAt(yearsMenu.SelectedIndex).Key, ((ListView)sender).SelectedIndex);
                    break;
                case Chart.Comparatively:
                    SetDailyGraphic(LinearData.ElementAt(yearsMenu.SelectedIndex).Key, ((ListView)sender).SelectedIndex);
                    break;
            }
        }

        private void ChartMenuSelectionChanged(int id)
        {
            CurrentChart = (Chart)id;
            switch (CurrentChart)
            {
                case Chart.Relatively:
                    pieChart.Visibility = Visibility.Visible;
                    cartesianChart.Visibility = Visibility.Collapsed;
                    break;
                case Chart.TotalComparatively:
                case Chart.Comparatively:
                    pieChart.Visibility = Visibility.Collapsed;
                    cartesianChart.Visibility = Visibility.Visible;
                    break;
            }
            SetYearMenu();
        }


    }
}
