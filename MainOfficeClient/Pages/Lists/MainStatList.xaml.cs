using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using LiveCharts;
using LiveCharts.Wpf;
using ClientControlsLibrary;
using DBClassesLibrary;
using MainOfficeClient.Classes;
using System.Collections;
using System.Windows.Media;

namespace MainOfficeClient.Pages.Lists
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

    public partial class MainStatList : Page
    {
        List<string> Offices;
        Dictionary<string, Dictionary<string, List<Relatively>>> RelativelyData;
        Dictionary<string, Dictionary<string, (List<Income>, List<Tax>)[]>> LinearData;
        List<Income> Incomes;
        List<Tax> Taxes;
        string TotalName = "Загалом";
        public Func<ChartPoint, string> PointLabel { get; set; }
        private RegionOfficeProxy Chanel;
        private Chart CurrentChart;
        private string CurrentOffice;

        public MainStatList(List<string> offices)
        {
            Offices = offices;
            SetChanel();
            PointLabel = chartPoint => string.Format("{0} грн. ({1:P})", chartPoint.Y, chartPoint.Participation);
            InitializeComponent();
            DataContext = this;
            SetAllDataFromServers();
            SetChartMenu();
        }

        private void SetOfficeMenu(List<string>errorOffices)
        {
            List<SubMenuItem> listViewItems = new List<SubMenuItem>();
            foreach (var item in Offices)
            {
                SubMenuItem subMenuItem = new SubMenuItem();
                subMenuItem.Text = item;
                subMenuItem.VerticalAlignment = VerticalAlignment.Center;
                subMenuItem.Kind = MaterialDesignThemes.Wpf.PackIconKind.None;
                if(errorOffices.Contains(item)) 
                    subMenuItem.IsEnabled = false;
                else
                {
                    subMenuItem.MouseLeftButtonUp += subMenuItemClick;
                }
                listViewItems.Add(subMenuItem);
            }
            officeMenu.ItemsSource = listViewItems;
            officeMenu.SelectedIndex = 0;
            CurrentOffice = Offices[0];
        }

        private void subMenuItemClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SubMenuItem subMenuItem = (SubMenuItem)sender;
            officeMenu.SelectedIndex = Offices.FindIndex(str => str.Equals(subMenuItem.Text));
            CurrentOffice = Offices[officeMenu.SelectedIndex];
        }

        private void SetChanel()
        {
            try
            {
                Chanel = new RegionOfficeProxy();
            }
            catch (Exception e)
            {
                MainWindowComunication.ShowError(e.Message);
            }
        }

        private void SetAllDataFromServers()
        {
            LinearData = new Dictionary<string, Dictionary<string, (List<Income>, List<Tax>)[]>>();
            RelativelyData = new Dictionary<string, Dictionary<string, List<Relatively>>>();
            List<string> errorItems = new List<string>();
            foreach(var item in Offices)
            {
                try
                {
                    SetAllDataFromServer(item);
                }
                catch
                {
                    errorItems.Add(item);
                }

            }

            SetOfficeMenu(errorItems);
        }

        private void SetAllDataFromServer(string serverName)
        {
            List<Quarterincome> quarterincomes = null;
            List<Quartertax> quartertaxes = null;

            Chanel.SetUser(CurrentUser.Instance().Login, CurrentUser.Instance().Password, serverName);
            Parallel.Invoke(
                () => Incomes = Chanel.GetDailyIncomes(serverName),
                () => Taxes = Chanel.GetDailyTaxes(serverName),
                () => quarterincomes = Chanel.GetQuarterIncomes(serverName),
                () => quartertaxes = Chanel.GetQuarterTaxes(serverName)
            );
            SetRelativelyData(serverName,quarterincomes, quartertaxes);
            SetLinearData(serverName, Incomes, Taxes);
        }

        private void SetLinearData(string serverName,List<Income> incomes, List<Tax> taxes)
        {
            LinearData[serverName] = new Dictionary<string, (List<Income>, List<Tax>)[]>();

            for (int i = 0; i < incomes.Count; i++)
            {

                string currentYear = ((DateTime)incomes[i].Date).ToString("yyyy");
                int currentQuartal = GetQuartalByDate((DateTime)incomes[i].Date);
                if (!LinearData[serverName].TryGetValue(currentYear, out (List<Income>, List<Tax>)[] array))
                {
                    (List<Income>, List<Tax>)[] newArray = new (List<Income>, List<Tax>)[4];
                    for (int j = 0; j < newArray.Length; j++)
                    {
                        newArray[j].Item1 = new List<Income>();
                        newArray[j].Item2 = new List<Tax>();
                    }
                    LinearData[serverName][currentYear] = newArray;
                }

                LinearData[serverName][currentYear][currentQuartal].Item1.Add(incomes[i]);
                LinearData[serverName][currentYear][currentQuartal].Item2.Add(taxes[i]);
            }
        }

        private int GetQuartalByDate(DateTime date) => (date.Month - 1) / 3;

        private void SetRelativelyData(string serverName,List<Quarterincome> quarterincomes, List<Quartertax> quartertaxes)
        {
            RelativelyData[serverName] = new Dictionary<string, List<Relatively>>();
            foreach (var item in quarterincomes)
            {
                int quarterIndex = Convert.ToInt32(item.Quarter);
                if (RelativelyData[serverName].TryGetValue(Convert.ToString(item.Year), out List<Relatively> list))
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
                    RelativelyData[serverName][Convert.ToString(item.Year)] = list;
                }
            }

            foreach (var item in quartertaxes)
            {
                int quarterIndex = Convert.ToInt32(item.Quarter);
                if (RelativelyData[serverName].TryGetValue(Convert.ToString(item.Year), out List<Relatively> list))
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
                    RelativelyData[serverName][Convert.ToString(item.Year)] = list;
                }
            }

            double globalTotal = 0, globalActual = 0;

            foreach (var dataByYear in RelativelyData[serverName])
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
            RelativelyData[serverName][TotalName] = new List<Relatively>() { new Relatively() { Total = globalTotal, Actual = globalActual } };

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
            ChartMenuSelectionChanged(0);
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
                    Data = RelativelyData[CurrentOffice].ToDictionary(kvp => kvp.Key, kvp => kvp.Value as IEnumerable);
                    break;
                default:
                    Data = LinearData[CurrentOffice].ToDictionary(kvp => kvp.Key, kvp => kvp.Value as IEnumerable);
                    break;
            }
            List<ListViewItem> listViewItems = new List<ListViewItem>();
            foreach (var item in Data)
            {
                TextBlock textBlock = new TextBlock();

                textBlock.Text = item.Key;

                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = textBlock;
                listViewItems.Add(listViewItem);
            }
            yearsMenu.ItemsSource = listViewItems;
            yearsMenu.SelectedIndex = 0;
        }



        private void SetDailyGraphic(string year, int quartal)
        {
            SetLinearGraphic(year, quartal, Chart.Comparatively);
        }

        private (double, double) GetTotalData(Income income, Tax tax)
        {
            return (Convert.ToDouble(income.Totalincome), Convert.ToDouble(tax.Totaltax));
        }

        private (double, double) GetData(Income income, Tax tax)
        {
            return (Convert.ToDouble(income.Incomeperday), Convert.ToDouble(tax.Taxday));
        }

        private void SetLinearGraphic(string year, int quartal, Chart chart)
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
                if (quartal != 0 && quartalIndex != quartal - 1) continue;

                var currentQuartalData = LinearData[CurrentOffice][year][quartalIndex];
                for (int j = 0; j < currentQuartalData.Item1.Count; j++)
                {
                    (double, double) tuple = (double.NaN, double.NaN);
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
            SetLinearGraphic(year, quartal, Chart.TotalComparatively);
        }

        private bool CheckQuartelByYear(string year, int quarter)
        {
            if (quarter < 0 && !year.Equals(TotalName)) return false;
            if (!RelativelyData[CurrentOffice].TryGetValue(year, out List<Relatively> result)) return false;
            if (result == null) return false;

            return result.Count > quarter;
        }

        private void SetRelativelyGraphic(string year, int quarter)
        {
            if (!CheckQuartelByYear(year, quarter)) return;
            if (quarter == -1) quarter = 0;
            Relatively relatively;
            for (int i = quarter; i < 5; i++)
            {
                relatively = RelativelyData[CurrentOffice][year][i];
                if (relatively.Actual != 0 || relatively.Total != 0)
                {
                    RestSeries.Values = new ChartValues<double> { relatively.Total - relatively.Actual };
                    ActualSeries.Values = new ChartValues<double> { relatively.Actual };
                    break;
                }
            }

        }


        private void SetQuartelMenu(int index)
        {
            if (index == -1) return;
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
            var value = LinearData[CurrentOffice].ElementAt(index).Value;
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
            KeyValuePair<string, List<Relatively>> pair = RelativelyData[CurrentOffice].ElementAt(id);
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
            if (yearsMenu.SelectedIndex == -1) return;
            switch (CurrentChart)
            {
                case Chart.Relatively:
                    SetRelativelyGraphic(RelativelyData[CurrentOffice].ElementAt(yearsMenu.SelectedIndex).Key, ((ListView)sender).SelectedIndex);
                    break;
                case Chart.TotalComparatively:
                    SetTotalDailyGraphic(LinearData[CurrentOffice].ElementAt(yearsMenu.SelectedIndex).Key, ((ListView)sender).SelectedIndex);
                    break;
                case Chart.Comparatively:
                    SetDailyGraphic(LinearData[CurrentOffice].ElementAt(yearsMenu.SelectedIndex).Key, ((ListView)sender).SelectedIndex);
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

        private void yearsMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetQuartelMenu(((ListView)(sender)).SelectedIndex);
        }

        private void officeMenu_OnSelctionChanged(int id)
        {
            CurrentOffice = Offices[id];
            SetYearMenu();
        }
    }
}
