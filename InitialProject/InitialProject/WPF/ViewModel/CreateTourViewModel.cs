using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Image = InitialProject.Domain.Model.Image;
using System.Windows.Input;
using InitialProject.Commands;
using System.Xml.Linq;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;

namespace InitialProject.WPF.ViewModel
{
    public class CreateTourViewModel : ViewModelBase
    {
        public User LoggedInUser { get; set; }
        private readonly TourService _tourService;
        private readonly TourPointService _tourPointService;
        private readonly ILocationRepository _locationRepository;
        private readonly IImageRepository _imageRepository;

        public static ObservableCollection<String> Countries { get; set; }
        public Action CloseAction { get; set; }


        private RelayCommand confirmCreate;
        public RelayCommand CreateTourCommand
        {
            get => confirmCreate;
            set
            {
                if (value != confirmCreate)
                {
                    confirmCreate = value;
                    OnPropertyChanged();
                }

            }
        } 

        private RelayCommand cancel;
        public RelayCommand CancelTourCommand
        {
            get => cancel; 
            set
            {
                if(value != cancel)
                {
                    cancel = value;
                    OnPropertyChanged();
                }

            }
        }


        public CreateTourViewModel(User user) {
            LoggedInUser = user;
            _locationRepository = Inject.CreateInstance<ILocationRepository>();
            _tourService = new TourService();
            _tourPointService = new TourPointService();
            _imageRepository = new ImageRepository();
            Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
            Cities = new ObservableCollection<String>();
            CreateTourCommand = new RelayCommand(Execute_CreateTour, CanExecute_Command);
            CancelTourCommand = new RelayCommand(Execute_CancelTour, CanExecute_Command);
        }

        private ObservableCollection<String> _cities;
        public ObservableCollection<String> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

        private bool _isCityEnabled;
        public bool IsCityEnabled
        {
            get { return _isCityEnabled; }
            set
            {
                _isCityEnabled = value;
                OnPropertyChanged(nameof(IsCityEnabled));
            }
        }

        private String _selectedCity;
        public String SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;

            }
        }

        private String _selectedCountry;
        public String SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    Cities = new ObservableCollection<String>(_locationRepository.GetCities(SelectedCountry));
                    IsCityEnabled = true;
                    OnPropertyChanged(nameof(Cities));
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }



        private string _name;
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

        private string _description;
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

        private string _language;
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

        private string _maxGuestNum;
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


        private string _points;
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

        private string _startDate;
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

        private string _startTime;
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


        private string _duration;
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

        private string _imagesUrl;
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

        private bool CanExecute_Command(object parameter)
        {
            return true;
        }

        private void Execute_CancelTour(object sender)
        {
            CloseAction();
        }

        private void Execute_CreateTour(object sender)
        {
            TimeOnly _startTime = ConvertTime(StartTime);
            Location location = _locationRepository.FindLocation(SelectedCountry,SelectedCity);

            Tour newTour = new Tour(TourName, location, TourLanguage, int.Parse(MaxGuestNum), DateOnly.Parse(Date), _startTime, int.Parse(Duration), int.Parse(MaxGuestNum), false, LoggedInUser.Id, location.Id);

            Tour savedTour = _tourService.Save(newTour);
            GuideMainWindowViewModel.Tours.Add(newTour);
            string[] pointsNames = _points.Split(",");
            int order = 1;
            foreach (string name in pointsNames)
            {
                TourPoint newTourPoint = new TourPoint(name, false, false, order, savedTour.Id);
                TourPoint savedTourPoint = _tourPointService.Save(newTourPoint);
                savedTour.Points.Add(savedTourPoint);
                order++;
            }
            string[] imagesNames = _imagesUrl.Split(",");
            
            foreach (string name in imagesNames)
            {
                Image newImage = new Image(name, 0, savedTour.Id,0);
                Image savedImage = _imageRepository.Save(newImage);
                savedTour.Images.Add(savedImage);
            }
            CloseAction();

        }

        public TimeOnly ConvertTime(string times)
        {
            StartTimes time = (StartTimes)Enum.Parse(typeof(StartTimes), times);
            TimeOnly startTime;
            switch (time)
            {
                case StartTimes._8AM:
                    startTime = new TimeOnly(8, 0);
                    break;
                case StartTimes._10AM:
                    startTime = new TimeOnly(10, 0);
                    break;
                case StartTimes._12PM:
                    startTime = new TimeOnly(12, 0);
                    break;
                case StartTimes._2PM:
                    startTime = new TimeOnly(14, 0);
                    break;
                case StartTimes._4PM:
                    startTime = new TimeOnly(16, 0);
                    break;
                case StartTimes._6PM:
                    startTime = new TimeOnly(18, 0);

                    break;
            }
            return startTime;
        }


    }
}
