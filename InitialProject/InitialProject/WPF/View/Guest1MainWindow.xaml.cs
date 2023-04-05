
using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainWindow : Window
    {
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public static ObservableCollection<Accommodation> AccommodationsMainList { get; set; }
        public static ObservableCollection<AccommodationReservation> AccommodationsReservationList { get; set; }

        public static ObservableCollection<Accommodation> AccommodationsCopyList { get; set; }

        public Accommodation SelectedAccommodation{ get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public User LoggedInUser { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _reservationRepository;
        private readonly LocationRepository _locationRepository;

        public static ObservableCollection<String> Countries { get; set; }
        public static ObservableCollection<String> Cities { get; set; }


        public static String SelectedCountry { get; set; }
        public static String SelectedCity { get; set; }

        public static AccommodationType SelectedType { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _accommodationType;
        public string AccommType
        {
            get => _accommodationType;
            set
            {
                if (value != _accommodationType)
                {
                    _accommodationType = value;
                    OnPropertyChanged();
                }
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





        public Guest1MainWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _reservationRepository = new AccommodationReservationRepository();
            AccommodationsMainList = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            AccommodationsCopyList = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            AccommodationsReservationList=new ObservableCollection<AccommodationReservation>(_reservationRepository.GetByUser(user));
            Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());


            BindData();

        }

        private void BindData()
        {
            foreach (Accommodation accommodation in AccommodationsMainList)
            {
                accommodation.Location = _locationRepository.GetById(accommodation.IdLocation);
            }
            foreach(AccommodationReservation accRes in AccommodationsReservationList)
            {
                accRes.Accommodation = _accommodationRepository.GetById(accRes.IdAccommodation);
            }

        }


        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            if (Tab.SelectedIndex == 0)
            {
                if (SelectedAccommodation != null)
                {
                    CreateReservation createReservation = new CreateReservation(SelectedAccommodation, LoggedInUser, SelectedReservation);
                    createReservation.Show();
                }
                else return;
            }
        }
        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            Guest1MainWindow.AccommodationsMainList.Clear();
            int max = 0;
            int min = 0;

            if (!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))) || !(int.TryParse(txtReservationNum.Text, out min) || (txtReservationNum.Text.Equals(""))))
            {
                return;
            }
            foreach (Accommodation a in Guest1MainWindow.AccommodationsCopyList)
            {
                CheckConditions(max, min, a);

            }

            
        }

        private void CheckConditions(int max, int min, Accommodation a)
        {
            Location location = _locationRepository.GetById(a.IdLocation);
            if (a.Name.ToLower().Contains(txtName.Text.ToLower()) && (location.Country == SelectedCountry || SelectedCountry == null) && (location.City == SelectedCity || SelectedCity == null) && ComboboxType.SelectedItem == null &&
(a.MaxGuestNum - max >= 0 || txtGuestNum.Text.Equals("")) && (a.MinReservationDays - min <= 0 || txtReservationNum.Text.Equals("")))
            {
                a.Location = _locationRepository.GetById(a.IdLocation);
                Guest1MainWindow.AccommodationsMainList.Add(a);
            }
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            Country = ComboBoxCountry.SelectedItem.ToString();
            Cities = new ObservableCollection<String>(_locationRepository.GetCities(Country));

            ComboboxCity.ItemsSource = Cities;
            ComboboxCity.SelectedIndex = 0;
            ComboboxCity.IsEnabled = true;
        }

        private void ComboboxCity_DropDownClosed(object sender, EventArgs e)
        {
            City = ComboboxCity.SelectedItem.ToString();
        }

       

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            AccommodationsMainList.Clear();
            foreach (Accommodation a in AccommodationsCopyList)
            {
                a.Location = _locationRepository.GetById(a.IdLocation);
                AccommodationsMainList.Add(a);
            }
        }

		private void ViewGallery_Click(object sender, RoutedEventArgs e)
		{
            ViewAccommodationGallery viewAccommodationGallery = new ViewAccommodationGallery(SelectedAccommodation);
            viewAccommodationGallery.Show();
		}

        private void ChangeDateOfReservation_Click(object sender, RoutedEventArgs e)
        {
    
        }

        private void CancelReservation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Rate_Click(object sender, RoutedEventArgs e)
        {
            RateOwner rateOwner=new RateOwner();
            rateOwner.Show();
        }

        private void WheneverWherever_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SuperGuestInstructions_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
