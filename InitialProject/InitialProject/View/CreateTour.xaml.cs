using InitialProject.Model;
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
using Image = InitialProject.Model.Image;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {
        public User LoggedInUser { get; set; }
        private readonly TourRepository _tourRepository;
        private readonly LocationRepository _locationRepository;
        private readonly TourPointRepository _tourPointRepository;
        private readonly ImageRepository _imageRepository;

        public static ObservableCollection<string> Cities { get; set; }
        public static ObservableCollection<string> Countries { get; set; }

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

        private int startTime;

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
            Cities = new ObservableCollection<string>(_locationRepository.GetAllCities());
            Countries = new ObservableCollection<string>(_locationRepository.GetAllCountries());
        }
        private void ConfirmCreate_Click(object sender, RoutedEventArgs e)
        {
            Location savedLocation = _locationRepository.GetByCity(City);
            
            switch (ComboBoxTime.SelectedIndex)
            {
                case 0:
                    startTime = 8;
                    break;
                case 1:
                    startTime = 10;
                    break;
                case 2:
                    startTime = 12;
                    break;
                case 3:
                    startTime = 14;
                    break;
                case 4:
                    startTime = 16;
                    break;
                case 5:
                    startTime = 18;
                    break;
            }

            Tour newTour = new Tour(TourName, savedLocation, TourLanguage, int.Parse(MaxGuestNum), DateOnly.Parse(Date), startTime, int.Parse(Duration), int.Parse(MaxGuestNum), false, LoggedInUser.Id, savedLocation.Id) ;

            Tour savedTour = _tourRepository.Save(newTour);
            GuideMainWindow.Tours.Add(savedTour);

            string[] pointsNames = _points.Split(",");
            int order = 1;
            foreach (string name in pointsNames)
            {
                TourPoint newTourPoint = new TourPoint(name, false, order, savedTour.Id);
                TourPoint savedTourPoint = _tourPointRepository.Save(newTourPoint);
                order++;
            }

            string[] imagesNames = _imagesUrl.Split(",");

            foreach (string name in imagesNames)
            {
                Image newImage = new Image(name, 0, savedTour.Id);
                Image savedImage = _imageRepository.Save(newImage);
            }

            Close();
        }

        private void CancelCreate_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
