using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Shapes;
using Image = InitialProject.Model.Image;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window, INotifyPropertyChanged, IDataErrorInfo
    {
        public User LoggedInUser { get; set; }
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly TourPointRepository _tourPointRepository;
        private readonly ImageRepository _imageRepository;

        public static ObservableCollection<String> Cities { get; set; }
        public static ObservableCollection<String> Countries { get; set; }
        public static String SelectedCity { get; set; }
        public static String SelectedCountry { get; set; }
        public string Error => null;

        private string _name;
        private string _city;
        private string _country;
        private string _description;
        private string _language;
        private string _maxGuestNum;
        private string _points;
        private string _startDate;
        private string _startTime;
        private string _duration;
        private string _imagesUrl;


        private TimeOnly startTime;

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string TourName
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }
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
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }
        public string TourLanguage
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }
        public string MaxGuestNum
        {
            get => _maxGuestNum;
            set
            {
                if (value != _maxGuestNum)
                {
                    _maxGuestNum = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Points
        {
            get => _points;
            set
            {
                if (value != _points)
                {
                    _points = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Date
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }
        public string StartTime
        {
            get => _startTime;
            set
            {
                if (value != _startTime)
                {
                    _startTime = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Duration
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
        public string ImageUrls
        {
            get => _imagesUrl;
            set
            {
                if (value != _imagesUrl)
                {
                    _imagesUrl = value;
                    OnPropertyChanged();
                }
            }
        }

        public CreateTour(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _tourRepository = new TourRepository();
            _locationRepository = new LocationRepository();
            _tourPointRepository = new TourPointRepository();
            _imageRepository = new ImageRepository();
            Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
<<<<<<< HEAD
            
        }

        private readonly string[] _validatedProperties = {"TourName", "Description", "TourLanguage", "MaxGuestNum", "Points", "Duration", "ImageUrls" };
=======

        }

        private readonly string[] _validatedProperties = { "TourName", "Description", "TourLanguage", "MaxGuestNum", "Points", "Duration", "ImageUrls" };
>>>>>>> 30ff075a2a8f47c0f4681a3f612fab445bb49c07

        public string this[string columnName]
        {
            get
            {
                if (columnName == "TourName")
                {
                    if (string.IsNullOrEmpty(TourName))
                        return "Tour name is required";
                }
                else if (columnName == "Description")
                {
                    if (string.IsNullOrEmpty(Description))
                        return "Description is required";
                }
                else if (columnName == "TourLanguage")
                {
                    if (string.IsNullOrEmpty(TourLanguage))
                        return "Language is required";

                }
                else if (columnName == "MaxGuestNum")
                {
                    int num;
                    if (!int.TryParse(MaxGuestNum, out num))
                        return "Max guest number is not correctly entered";
                }
                else if (columnName == "Points")
                {
                    if (string.IsNullOrEmpty(Points))
                        return "First and the last point are required";
                }
                else if (columnName == "Duration")
                {
                    int num;
                    if (!int.TryParse(Duration, out num))
                        return "Duration is not correctly entered";
                }
                else if (columnName == "ImageUrls")
                {
                    if (string.IsNullOrEmpty(ImageUrls))
                        return "Image urls are required";
                }
                return null;
            }
        }

        public bool IsValid
        {
            get
            {
<<<<<<< HEAD
                foreach(var property in _validatedProperties)
                {
                    if (this[property] != null) 
=======
                foreach (var property in _validatedProperties)
                {
                    if (this[property] != null)
>>>>>>> 30ff075a2a8f47c0f4681a3f612fab445bb49c07
                        return false;
                }
                return true;
            }
        }
        private void ConfirmCreate_Click(object sender, RoutedEventArgs e)
        {
            if (IsValid)
            {
                Location savedLocation = _locationRepository.GetByCity(City);

                switch (ComboBoxTime.SelectedIndex)
                {
                    case 0:
<<<<<<< HEAD
                        startTime = new TimeOnly(8,0);
                        break;
                    case 1:
                        startTime = new TimeOnly(10,0);
                        break;
                    case 2:
                        startTime = new TimeOnly(12,0);
                        break;
                    case 3:
                        startTime = new TimeOnly(14,0);
                        break;
                    case 4:
                        startTime = new TimeOnly(16,0);
                        break;
                    case 5:
                        startTime = new TimeOnly(18,0);
=======
                        startTime = new TimeOnly(8, 0);
                        break;
                    case 1:
                        startTime = new TimeOnly(10, 0);
                        break;
                    case 2:
                        startTime = new TimeOnly(12, 0);
                        break;
                    case 3:
                        startTime = new TimeOnly(14, 0);
                        break;
                    case 4:
                        startTime = new TimeOnly(16, 0);
                        break;
                    case 5:
                        startTime = new TimeOnly(18, 0);
>>>>>>> 30ff075a2a8f47c0f4681a3f612fab445bb49c07
                        break;
                }

                Tour newTour = new Tour(TourName, savedLocation, TourLanguage, int.Parse(MaxGuestNum), DateOnly.Parse(Date), startTime, int.Parse(Duration), int.Parse(MaxGuestNum), false, LoggedInUser.Id, savedLocation.Id);

                Tour savedTour = _tourRepository.Save(newTour);
                GuideMainWindow.Tours.Add(savedTour);

                string[] pointsNames = _points.Split(",");
                int order = 1;
                foreach (string name in pointsNames)
                {
                    TourPoint newTourPoint = new TourPoint(name, false, false, order, savedTour.Id);
                    TourPoint savedTourPoint = _tourPointRepository.Save(newTourPoint);
                    savedTour.Points.Add(savedTourPoint);
                    order++;
                }

                string[] imagesNames = _imagesUrl.Split(",");

                foreach (string name in imagesNames)
                {
                    Image newImage = new Image(name, 0, savedTour.Id);
                    Image savedImage = _imageRepository.Save(newImage);
                    savedTour.Images.Add(savedImage);
                }

                Close();
            }
            else
            {
                MessageBox.Show("All fields are not valid");
            }

        }

        private void CancelCreate_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            Country = ComboBoxCountry.SelectedItem.ToString();
            Cities = new ObservableCollection<String>(_locationRepository.GetCities(Country));

<<<<<<< HEAD
            ComboBoxCity.ItemsSource=Cities;
            ComboBoxCity.SelectedIndex=0;
=======
            ComboBoxCity.ItemsSource = Cities;
            ComboBoxCity.SelectedIndex = 0;
>>>>>>> 30ff075a2a8f47c0f4681a3f612fab445bb49c07
        }

        private void ComboBoxCity_DropDownClosed(object sender, EventArgs e)
        {
            if (ComboBoxCountry.SelectedItem != null)
                City = ComboBoxCity.SelectedItem.ToString();
<<<<<<< HEAD
            
=======

>>>>>>> 30ff075a2a8f47c0f4681a3f612fab445bb49c07
        }
    }
}