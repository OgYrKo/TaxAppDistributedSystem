using Client.Classes;
using Client.Controls;
using Client.Enums;
using Client.Windows.Dialogs;
using DBClassesLibrary;
using InterfacesLibrary;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Client.Pages.Cards
{

    public partial class LandCard : Page, INotifyPropertyChanged
    {

        private IFullData full_data_channel;
        private ILandProperties land_properties_channel;
        private IAddressFunc address_func_channel;
        private ISpecialpurpose special_purpose_channel;
        private ILandFunc land_func_channel;
        private Landplotfull landplot;
        private List<Ownershiptype> ownershiptypes;
        private List<City> cities;
        private List<Street> streets;
        private List<Addressfull> full_addresses;
        private ObservableCollection<Contractorslandfull> full_contractorslands;
        private List<AddititonalClass> locations;
        private List<Specialpurposesection> specialpurposesections;
        private List<SpecialPurposeNumChapter> specialPurposeNumChapters;
        private List<SpecialPurposeNumSubGroup> specialPurposeNumSubGroups;
        private bool section_chnged_flag = false;
        private bool chapter_chnged_flag = false;
        private bool subgroup_chnged_flag = false;
        private string in_city_string = "В межах населеного пункту";

        private CardState State;
        private EditableStatus cardEditableStatus;
        public EditableStatus CardEditableStatus
        {
            get => cardEditableStatus;
            set
            {
                cardEditableStatus = value;
                OnPropertyChanged(nameof(IsEditable));
            }
        }
        public bool IsEditable { get => CardEditableStatus == EditableStatus.Editable ? true : false; }
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler OnSave;
        public event EventHandler OnClose;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public LandCard(Landplotfull land)
        {
            State = CardState.Exist;
            CardEditableStatus = EditableStatus.NonEditable;
            InitializeConnections();
            InitializeLists(land.Landplotkey);
            InitializeComponent();
            InitializeBindings();
            SetSubMenuButtons();
            DataContext = this;
            FillFields((int)land.Landplotkey);
        }

        public LandCard()
        {
            State = CardState.New;
            CardEditableStatus = EditableStatus.Editable;
            InitializeConnections();
            InitializeLists(null);
            InitializeComponent();
            InitializeBindings();
            SetSubMenuButtons();
            DataContext = this;
            Key.Text = Convert.ToString(land_func_channel.GetNextIdForLandplot());
        }

        public void InitializeConnections()
        {
            Connection<IFullData> connection = new Connection<IFullData>("IFullData");
            full_data_channel = connection.channelWithUser;

            Connection<ILandProperties> connection_to_land_properties = new Connection<ILandProperties>("ILandProperties");
            land_properties_channel = connection_to_land_properties.channelWithUser;

            Connection<IAddressFunc> connection_to_address_func = new Connection<IAddressFunc>("IAddressFunc");
            address_func_channel = connection_to_address_func.channelWithUser;

            Connection<ISpecialpurpose> connection_to_special_purpose = new Connection<ISpecialpurpose>("ISpecialpurpose");
            special_purpose_channel = connection_to_special_purpose.channelWithUser;

            Connection<ILandFunc> connection_to_land_func = new Connection<ILandFunc>("ILandFunc");
            land_func_channel = connection_to_land_func.channelWithUser;
        }

        private void InitializeLists(int? Landplotkey)
        {
            ownershiptypes = land_properties_channel.GetAllOwnershiptype();
            cities = address_func_channel.GetAllCities();
            streets = address_func_channel.GetAllStreets();
            full_contractorslands = new ObservableCollection<Contractorslandfull>(full_data_channel.GetAllContractorslandfullWithFilter(null, null, new List<int?>() { Landplotkey }));
            specialpurposesections = special_purpose_channel.GetAllSections();
            ConvertNumerableSpecialPurposeChapter(special_purpose_channel.GetAllChapters(), out specialPurposeNumChapters);
            ConvertNumerableSpecialPurposeSubGroup(special_purpose_channel.GetAllSubGroups(), out specialPurposeNumSubGroups);
        }

        private void InitializeBindings()
        {
            SetItemsSource(OwnershipType, ownershiptypes, "Type", "Ownershiptypekey");
            SetItemsSource(City, cities, "City1", "Citykey");
            SetItemsSource(Street, streets, "Street1", "Streetkey");
            locations = new List<AddititonalClass>() { new AddititonalClass(){ Key = 1, Value = in_city_string },
                                                    new AddititonalClass() { Key = 2, Value = "За межами населеного пункту" } };
            SetItemsSource(Location, locations, "Value", "Key");
            OwnersData.ItemsSource = full_contractorslands;

            SetSpecialPurposeItems(Section, specialpurposesections, "Sectionkey");
            SetSpecialPurposeItems(Chapter, specialPurposeNumChapters, "Key");
            SetSpecialPurposeItems(SubGroup, specialPurposeNumSubGroups, "Key");
        }

        private void SetItemsSource<T>(ComboField comboBox, List<T> list, string displayMemberPath, string SelectedValuePath)
        {
            comboBox.ItemsSource = list;
            comboBox.DisplayMemberPath = displayMemberPath;
            comboBox.SelectedValuePath = SelectedValuePath;
        }

        private void ConvertNumerableSpecialPurposeChapter(List<Specialpurposechapter> special_purpose_chapter, out List<SpecialPurposeNumChapter> num_special_purpose_chapter)
        {
            num_special_purpose_chapter = new List<SpecialPurposeNumChapter>();
            for (int i = 0; i < special_purpose_chapter.Count; i++)
            {
                num_special_purpose_chapter.Add(new SpecialPurposeNumChapter(i, special_purpose_chapter[i]));
            }
        }
        private void ConvertNumerableSpecialPurposeSubGroup(List<Specialpurposesubgroup> special_purpose_item, out List<SpecialPurposeNumSubGroup> num_special_purpose_num)
        {
            num_special_purpose_num = new List<SpecialPurposeNumSubGroup>();
            for (int i = 0; i < special_purpose_item.Count; i++)
            {
                num_special_purpose_num.Add(new SpecialPurposeNumSubGroup(i, special_purpose_item[i]));
            }
        }

        private void SetSpecialPurposeItems<T>(ComboBox comboBox, List<T> list, string SelectedValuePath)
        {
            comboBox.ItemsSource = list;
            comboBox.SelectedValuePath = SelectedValuePath;
        }

        private void SetSubMenuButtons()
        {
            SubMenuItem[] subMenuItems = (SubMenuItem[])subMenu.ItemsSource;
            switch (CardEditableStatus)
            {
                case EditableStatus.Editable:
                    subMenuItems[0].Kind = PackIconKind.ContentSave;
                    subMenuItems[0].Text = "Зберегти";
                    subMenuItems[1].IsEnabled = false;
                    break;
                case EditableStatus.NonEditable:
                    subMenuItems[0].Kind = PackIconKind.Edit;
                    subMenuItems[0].Text = "Редагувати";
                    subMenuItems[1].IsEnabled = true;
                    break;
            }
        }

        private void SaveAndEditClick(object sender, MouseButtonEventArgs e)
        {

            switch (CardEditableStatus)
            {
                case EditableStatus.Editable:
                    Save();
                    break;
                case EditableStatus.NonEditable:
                    CardEditableStatus = EditableStatus.Editable;
                    break;
            }

            SetSubMenuButtons();
        }



        private void Delete(object sender, MouseButtonEventArgs e)
        {
            CheckDialog checkDialog = new CheckDialog("Видалити земельну ділянку?");
            if (checkDialog.ShowDialog() == true)
            {
                string response = land_func_channel.DeleteLand(new List<int>() { Convert.ToInt32(landplot.Landplotkey) });
                MainWindowComunication.IsAnswerCorrect(response);
                MainWindowComunication.ClosePage(this);
                if (OnClose != null) OnClose(null, null);
            }

        }

        private void FillFields(int Landplotkey)
        {
            //TODO check something about visibility
            //ChangeOwners.Visibility = Visibility.Visible;


            landplot = full_data_channel.GetLand(Landplotkey);
            Key.Text = Convert.ToString(landplot.Landplotkey);
            LandName.Text = landplot.Landplotname;
            CadastrialNumber.Text = landplot.Cadastralnumber;
            Owners.Text = landplot.Counterpartynames;
            Square.Text = Convert.ToString(landplot.Square);
            Area.Text = Convert.ToString(landplot.Area);
            StandartValuation.Text = Convert.ToString(landplot.Standartvaluation);
            MonetaryValuation.Text = Convert.ToString(landplot.Monetaryvaluation);
            City.SelectedValue = landplot.Citykey;
            Street.SelectedValue = landplot.Streetkey;
            HouseNum.Text = landplot.Housenum;
            Location.SelectedValue = landplot.Location == true ? 1 : 2;
            OwnershipType.SelectedValue = landplot.Ownershiptypekey;
            Section.SelectedValue = landplot.Sectionkey;
            ExternalComment.Text = landplot.Extrainformation;
            if (landplot.Chapterkey != null && landplot.Sectionkey != null)
                Chapter.SelectedValue = specialPurposeNumChapters.Find(value => value.Chapterkey == landplot.Chapterkey && value.Sectionkey == landplot.Sectionkey).Key;
            if (landplot.Chapterkey != null && landplot.Groupkey != null)
                SubGroup.SelectedValue = specialPurposeNumSubGroups.Find(value => value.Chapterkey == landplot.Chapterkey && value.Groupkey == landplot.Groupkey).Key;
        }

        private Landplot GetLandplotValues()
        {
            bool check_flag = false;
            Brush brush = Key.Color;
            Landplot new_landplot = new Landplot();
            new_landplot.Landplotkey = Convert.ToInt32(Key.Text);
            if (LandName.Text != "")
            {
                new_landplot.Landplotname = LandName.Text;
                LandName.Color = brush;
            }
            else
            {
                LandName.Color = Brushes.Red;
                check_flag = true;
            }

            string pattern = @"^\d{10}:\d{2}:\d{3}:\d{4}$";
            if (!Regex.IsMatch(CadastrialNumber.Text, pattern))
            {
                CadastrialNumber.Text = "";
                CadastrialNumber.Color = Brushes.Red;
                check_flag = true;
            }
            else
            {
                new_landplot.Cadastralnumber = CadastrialNumber.Text;
                CadastrialNumber.Color = brush;
            }

            if ((OwnershipType.SelectedValue as int?) != null)
            {
                new_landplot.Ownershiptypekey = Convert.ToInt32(OwnershipType.SelectedValue as int?);
                OwnershipType.Color = brush;
            }
            else
            {
                check_flag = true;
                OwnershipType.Color = Brushes.Red;

            }
            if (Area.Text != "") new_landplot.Area = Convert.ToInt16(Area.Text);
            else
            {
                Area.Text = "";
                Area.Color = Brushes.Red;
                check_flag = true;
            }

            if (new_landplot.Area < 1 || new_landplot.Area > 13)
            {
                Area.Text = "";
                Area.Color = Brushes.Red;
                check_flag = true;
            }
            else
            {
                Area.Color = brush;
            }

            //TODO add Benefit
            if ((City.SelectedValue as int?) != null)
            {
                City.Color = brush;
                new_landplot.Citykey = Convert.ToInt32(City.SelectedValue as int?);
            }
            else
            {
                City.Color = Brushes.Red;
                check_flag = true;
            }
            if ((Street.SelectedValue as int?) != null)
            {
                new_landplot.Streetkey = Convert.ToInt32(Street.SelectedValue as int?);
                Street.Color = brush;

            }
            else
            {
                Street.Color = Brushes.Red;
                check_flag = true;
            }
            if (HouseNum.Text != "")
            {
                HouseNum.Color = brush;
                new_landplot.Housenum = HouseNum.Text;
            }
            else
            {
                HouseNum.Color = Brushes.Red;
                check_flag = true;
            }
            if (Location.SelectedValue == null)
                new_landplot.Location = false;
            else
                new_landplot.Location = Location.SelectedValue.Equals(in_city_string) ? true : false;

            new_landplot.Extrainformation = ExternalComment.Text;
            if (check_flag) throw new Exception("Не всі поля заповнені!");

            return new_landplot;
        }

        private Specialpurposeland GetSpecialpurposelandValues(int Landplotkey)
        {
            Brush brush = Key.Color;
            Specialpurposeland new_specialpurposeland = null;
            if (SubGroup.SelectedValue as int? != null)
            {
                new_specialpurposeland = new Specialpurposeland();
                new_specialpurposeland.Key = land_properties_channel.GetNextIdForSpecialpurposeland();
                new_specialpurposeland.Landplotkey = Landplotkey;
                var sub_group = specialPurposeNumSubGroups.Find(value => value.Key == (SubGroup.SelectedValue as int?));
                new_specialpurposeland.Specialpurposechapter = sub_group.Chapterkey;
                new_specialpurposeland.Specialpurposesubgroup = sub_group.Groupkey;
            }
            else
            {
                throw new Exception("Оберіть цільове призначення!");
            }
            return new_specialpurposeland;
        }

        private Squarelandplot GetSquarelandplotValues(int Landplotkey)
        {
            Brush brush = Key.Color;
            Squarelandplot new_squarelandplot = null;
            if (Square.Text != "")
            {
                new_squarelandplot = new Squarelandplot();
                new_squarelandplot.Squarelandplotkey = land_properties_channel.GetNextIdForSquarelandplot();
                new_squarelandplot.Landplotkey = Landplotkey;
                new_squarelandplot.Square = Convert.ToSingle(Square.Text);
                Square.Color = brush;
            }
            else
            {
                Square.Color = Brushes.Red;
                throw new Exception("Площа не може бути пустою!");
            }
            return new_squarelandplot;
        }

        private Monetaryvaluation GetMonetaryvaluationValues(int Landplotkey)
        {
            Brush brush = Key.Color;
            Monetaryvaluation new_monetaryvaluation = new Monetaryvaluation();
            if (MonetaryValuation.Text != "")
            {
                new_monetaryvaluation.Monetaryvaluationkey = land_properties_channel.GetNextIdForMonetaryvaluation();
                new_monetaryvaluation.Landplotkey = Landplotkey;
                new_monetaryvaluation.Monetaryvaluation1 = Convert.ToDecimal(MonetaryValuation.Text);
                MonetaryValuation.Color = brush;
            }
            else
            {
                MonetaryValuation.Color = Brushes.Red;

                throw new Exception("Поле РГО не може бути пустим");
            }
            return new_monetaryvaluation;
        }

        private Standartvaluation GetStandartvaluationValues(int Landplotkey)
        {
            Brush brush = Key.Color;
            Standartvaluation new_standartvaluation = new Standartvaluation();
            if (StandartValuation.Text != "")
            {
                new_standartvaluation.Standartvaluationkey = land_properties_channel.GetNextIdForStandartvaluation();
                new_standartvaluation.Landplotkey = Landplotkey;
                new_standartvaluation.Standartvaluation1 = Convert.ToDecimal(StandartValuation.Text);
                StandartValuation.Color = brush;
            }
            else
            {
                StandartValuation.Color = Brushes.Red;
                throw new Exception("Поле НГО не може бути пустим");
            }
            return new_standartvaluation;
        }


        private string AddLand()
        {
            try
            {
                int LandplotKey = Convert.ToInt32(Key.Text);
                string error = "OK";
                Specialpurposeland new_specialpurposeland=new Specialpurposeland();
                Squarelandplot new_squarelandplot=new Squarelandplot();
                Monetaryvaluation new_monetaryvaluation=new Monetaryvaluation();
                Standartvaluation new_standartvaluation=new Standartvaluation();
                Landplot new_landplot=new Landplot();
                try
                {
                    new_specialpurposeland = GetSpecialpurposelandValues(LandplotKey);
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
                try
                {
                    new_squarelandplot = GetSquarelandplotValues(LandplotKey);
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
                try
                {
                    new_monetaryvaluation = GetMonetaryvaluationValues(LandplotKey);
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
                try
                {
                    new_standartvaluation = GetStandartvaluationValues(LandplotKey);
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
                try
                {
                    new_landplot = GetLandplotValues();
                }
                catch (Exception e)
                {
                    error = e.Message;
                }


                if (MainWindowComunication.IsAnswerCorrect(error))
                {
                    string response = land_func_channel.AddLand(new_landplot);
                    if (MainWindowComunication.IsAnswerCorrect(response))
                    {
                        AddLandFunc(new_specialpurposeland, new_squarelandplot, new_monetaryvaluation, new_standartvaluation);
                    }
                }
                return error;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private string EditLand()
        {
            try
            {
                int LandplotKey = Convert.ToInt32(Key.Text);
                string error = "OK";
                Specialpurposeland new_specialpurposeland = new Specialpurposeland();
                Squarelandplot new_squarelandplot = new Squarelandplot();
                Monetaryvaluation new_monetaryvaluation = new Monetaryvaluation();
                Standartvaluation new_standartvaluation = new Standartvaluation();
                Landplot new_landplot = new Landplot();
                try
                {
                    new_specialpurposeland = GetSpecialpurposelandValues(LandplotKey);
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
                try
                {
                    new_squarelandplot = GetSquarelandplotValues(LandplotKey);
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
                try
                {
                    new_monetaryvaluation = GetMonetaryvaluationValues(LandplotKey);
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
                try
                {
                    new_standartvaluation = GetStandartvaluationValues(LandplotKey);
                }
                catch (Exception e)
                {
                    error = e.Message;
                }
                try
                {
                    new_landplot = GetLandplotValues();
                }
                catch (Exception e)
                {
                    error = e.Message;
                }


                if (MainWindowComunication.IsAnswerCorrect(error))
                {
                    land_func_channel.UpdateLand(new_landplot);
                    AddLandFunc(new_specialpurposeland, new_squarelandplot, new_monetaryvaluation, new_standartvaluation);
                }
                return error;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        private void AddLandFunc(Specialpurposeland new_specialpurposeland, Squarelandplot new_squarelandplot, Monetaryvaluation new_monetaryvaluation, Standartvaluation new_standartvaluation)
        {

            if (new_specialpurposeland != null)
            {
                if (landplot == null)
                    land_properties_channel.AddSpecialpurposeland(new_specialpurposeland);
                else if (
                    !new_specialpurposeland.Specialpurposesubgroup.Equals(landplot.Groupkey)
                    ||
                    !new_specialpurposeland.Specialpurposechapter.Equals(landplot.Chapterkey)
                    )
                    land_properties_channel.AddSpecialpurposeland(new_specialpurposeland);
            }

            if (new_squarelandplot != null)
            {
                if (landplot == null)
                    land_properties_channel.AddSquarelandplot(new_squarelandplot);

                else if (!new_squarelandplot.Square.Equals(landplot.Square))
                    land_properties_channel.AddSquarelandplot(new_squarelandplot);

            }

            if (new_monetaryvaluation != null)
            {
                if (landplot == null)
                    land_properties_channel.AddMonetaryvaluation(new_monetaryvaluation);

                else if (!new_monetaryvaluation.Monetaryvaluation1.Equals(landplot.Monetaryvaluation))
                    land_properties_channel.AddMonetaryvaluation(new_monetaryvaluation);

            }

            if (new_standartvaluation != null)
            {
                if (landplot == null)
                    land_properties_channel.AddStandartvaluation(new_standartvaluation);

                else if (!new_standartvaluation.Standartvaluation1.Equals(landplot.Standartvaluation))
                    land_properties_channel.AddStandartvaluation(new_standartvaluation);

            }
        }


        private void Save()
        {
            switch (State)
            {
                case CardState.New:
                    if (!AddLand().Equals("OK")) return;
                    State = CardState.Exist;
                    break;
                case CardState.Exist:
                    if (!EditLand().Equals("OK")) return;
                    break;
            }
            landplot = full_data_channel.GetLand(Convert.ToInt32(Key.Text));
            CardEditableStatus =  EditableStatus.NonEditable;
            if (OnSave != null) OnSave(landplot, null);
        }

        

        //private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    try
        //    {
        //        if (CardActions.SelectedIndex == -1) return;
        //        if (new_flag) AddLand();
        //        CheckDialog checkDialog;
        //        switch (((ListViewItem)((ListView)sender).SelectedItem).LandName)
        //        {
        //            //case "EditOrSave":
        //            //    if (!edit_flag && !new_flag) Save();
        //            //    Editable();
        //            //    break;
        //            //case "Delete":
        //            //    checkDialog = new CheckDialog("Видалити земельну ділянку?");
        //            //    if (checkDialog.ShowDialog() == true)
        //            //    {
        //            //        string s = land_func_channel.DeleteLand(new List<int>() { Convert.ToInt32(landplot.Landplotkey) });
        //            //        GoToLandList();
        //            //    }
        //            //    break;
        //            case "ChangeOwners":
        //                if ((new OwnersDialog(Convert.ToInt32(landplot.Landplotkey))).ShowDialog() == true)
        //                {
        //                    FillFields(Convert.ToInt32(landplot.Landplotkey));
        //                    full_contractorslands = new ObservableCollection<Contractorslandfull>(full_data_channel.GetAllContractorslandfullWithFilter(null, null, new List<int?>() { landplot.Landplotkey }));

        //                }
        //                break;
        //            case "Tax":
        //                checkDialog = new CheckDialog("Виконати нарахування податку на землю?");
        //                if (checkDialog.ShowDialog() == true)
        //                    land_func_channel.ChargeTaxesByLand(Convert.ToInt32(landplot.Landplotkey));
        //                break;
        //            default:
        //                break;
        //        }
        //        CardActions.SelectedIndex = -1;
        //        if (new_flag)
        //        {
        //            FillFields(Convert.ToInt32(Key.Text));
        //            new_flag = false;
        //        }
        //    }
        //    catch
        //    {
        //        CardActions.SelectedIndex = -1;
        //    }
        //}

        private void City_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<int?> l = new List<int?>() { City.SelectedValue as int? };
            full_addresses = full_data_channel.GetAllAddressesWithFilter(l, null, null, null);
            SetItemsSource(Street, full_addresses, "Street", "Streetkey");
        }

        private void Section_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chapter_chnged_flag || subgroup_chnged_flag) return;
            section_chnged_flag = true;

            char? nullable_char = Section.SelectedValue as char?;
            List<char> chosen_sectionskeys = null;
            if (nullable_char != null)
                chosen_sectionskeys = new List<char>() { nullable_char.Value };
            ConvertNumerableSpecialPurposeChapter(special_purpose_channel.GetAllChaptersWithFilter(null, chosen_sectionskeys, null), out specialPurposeNumChapters);
            SetSpecialPurposeItems(Chapter, specialPurposeNumChapters, "Key");

            List<short> chosen_chapterkeys = new List<short>();
            foreach (var chosen_chapterkey in specialPurposeNumChapters) chosen_chapterkeys.Add(chosen_chapterkey.Chapterkey);
            ConvertNumerableSpecialPurposeSubGroup(special_purpose_channel.GetAllSubGroupsWithFilter(null, chosen_chapterkeys, null), out specialPurposeNumSubGroups);
            SetSpecialPurposeItems(SubGroup, specialPurposeNumSubGroups, "Key");

            Chapter.SelectedValue = null;
            SubGroup.SelectedValue = null;

            section_chnged_flag = false;
        }

        private void Chapter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (subgroup_chnged_flag || section_chnged_flag) return;
            chapter_chnged_flag = true;


            List<short> chosen_chapterkeys = null;
            if ((Chapter.SelectedValue as int?) != null)
                chosen_chapterkeys = new List<short>() { specialPurposeNumChapters.Find(value => value.Key == (Chapter.SelectedValue as int?)).Chapterkey };
            ConvertNumerableSpecialPurposeSubGroup(special_purpose_channel.GetAllSubGroupsWithFilter(chosen_chapterkeys, null, null), out specialPurposeNumSubGroups);
            SetSpecialPurposeItems(SubGroup, specialPurposeNumSubGroups, "Key");

            Section.SelectedValue = specialPurposeNumChapters.Find(value => value.Key == (Chapter.SelectedValue as int?)).Sectionkey;
            SubGroup.SelectedValue = null;
            chapter_chnged_flag = false;
        }

        private void SubGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (chapter_chnged_flag || section_chnged_flag) return;
            subgroup_chnged_flag = true;


            var sub_group = specialPurposeNumSubGroups.Find(value => value.Key == (SubGroup.SelectedValue as int?));
            var chapter = specialPurposeNumChapters.Find(value => value.Chapterkey == sub_group.Chapterkey);

            Chapter.SelectedValue = chapter.Key;
            Section.SelectedValue = specialPurposeNumChapters.Find(value => value.Chapterkey == chapter.Chapterkey).Sectionkey;
            subgroup_chnged_flag = false;
        }
        private void CadastrialNumber_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != ":" && IsNumber(e.Text) == false)
            {
                e.Handled = true;
            }
        }
        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (IsNumber(e.Text) == false)
            {
                e.Handled = true;
            }
        }
        private void Decimal_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (e.Text != "." && IsNumber(e.Text) == false)
            {
                e.Handled = true;
            }
            else if (e.Text == ".")
            {
                if (((TextBox)sender).Text.IndexOf(e.Text) > -1)
                {
                    e.Handled = true;
                }
            }
        }
        private bool IsNumber(string Text)
        {
            int output;
            return int.TryParse(Text, out output);
        }

        private void ChargeTax(object sender, MouseButtonEventArgs e)
        {

            CheckDialog checkDialog = new CheckDialog("Виконати нарахування податку на землю?");
            if (checkDialog.ShowDialog() == true)
                land_func_channel.ChargeTaxesByLand(Convert.ToInt32(landplot.Landplotkey));


            
        }

        private void ChangeOwners(object sender, MouseButtonEventArgs e)
        {
            if ((new OwnersDialog(Convert.ToInt32(landplot.Landplotkey))).ShowDialog() == true)
            {
                FillFields(Convert.ToInt32(landplot.Landplotkey));
                full_contractorslands = new ObservableCollection<Contractorslandfull>(full_data_channel.GetAllContractorslandfullWithFilter(null, null, new List<int?>() { landplot.Landplotkey }));

            }
        }
    }
}
