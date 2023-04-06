using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourFiltering.xaml
    /// </summary>
    public partial class TourFiltering : Window, IDataErrorInfo
    {
        public static String SelectedCountry { get; set; }
        public static String SelectedCity { get; set; }
        private readonly LocationRepository _locationRepository;
        public static ObservableCollection<String> Countries { get; set; }

        public static ObservableCollection<String> Cities { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public string Error => null;


        private string _guestNum;
        private string _duration;

        public string TourGuestNum
        {
            get => _guestNum;
            set
            {
                if (value != _guestNum)
                {
                    _guestNum = value;
                    OnPropertyChanged();
                }
            }
        }

        public string TourDuration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged();
                }
            }
        }





        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public TourFiltering()
        {
            InitializeComponent();
            DataContext = this;
            _locationRepository = new LocationRepository();
            Countries = new ObservableCollection<string>(_locationRepository.GetAllCountries());
            //ComboBoxCountry.SelectedIndex = 0;

        }

        private readonly string[] _validatedProperties = { "TourGuestNum", "TourDuration" };

        public string this[string columnName]
        {
            get
            {
                if (columnName == "TourGuestNum")
                {
                    int num;
                    if (!int.TryParse(TourGuestNum, out num))
                        return "Max guest number is not correctly entered";
                }
                else if (columnName == "TourDuration")
                {
                    int num;
                    if (!int.TryParse(TourDuration, out num))
                        return "Duration is not correctly entered";

                }
                return null;
            }
        }

        public bool IsValid
        {
            get
            {
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
                        return false;
                }
                return true;
            }
        }




        private string _city;
        public string City
        {
            get => _city;
            set
            {
                if (value != _city)
                {
                    _city = value;
                    OnPropertyChanged();
                }
            }
        }



        private string _country;
        public string Country
        {
            get => _country;
            set
            {
                if (value != _country)
                {
                    _country = value;
                    OnPropertyChanged();
                }
            }
        }
        private void Button_Click_Filter(object sender, RoutedEventArgs e)
        {
            Guest2MainWindow.ToursMainList.Clear();
            Location location = _locationRepository.FindLocation(Country, City);

            int max = 0;
            if (!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))))
            {
                return;
            }
            foreach (Tour tour in Guest2MainWindow.ToursCopyList)
            {
                FilterTour(location, max, tour);
            }
            Close();
        }

        private void FilterTour(Location location, int max, Tour tour)
        { 
            if (tour.Language.ToLower().Contains(txtLanguage.Text.ToLower()) && (tour.Location.Country == Country || Country ==null) && (tour.Location.City == City || City == null) && tour.Duration.ToString().ToLower().Contains(txtDuration.Text.ToLower()) &&
                                (tour.MaxGuestNum - max >= 0 || txtGuestNum.Text.Equals("")))
            {
                Guest2MainWindow.ToursMainList.Add(tour);
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {

            Country = ComboBoxCountry.SelectedItem.ToString();
            Cities = new ObservableCollection<String>(_locationRepository.GetCities(Country));

            ComboBoxCity.IsEnabled = true;
            ComboBoxCity.ItemsSource = Cities;
            ComboBoxCity.SelectedIndex = 0;
            
        }

        private void ComboboxCity_DropDownClosed(object sender, EventArgs e)
        {
            City = ComboBoxCity.SelectedItem.ToString();
        }
    }
}
