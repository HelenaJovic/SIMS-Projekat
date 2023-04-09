using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class TourFilteringViewModel : ViewModelBase
    {
        private readonly LocationRepository _locationRepository;
        public static ObservableCollection<String> Countries { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        public Action CloseAction { get; set; }


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

        public TourFilteringViewModel()
        {
            _locationRepository = new LocationRepository();
            Countries = new ObservableCollection<string>(_locationRepository.GetAllCountries());
            Cities = new ObservableCollection<String>();
            //IsCityEnabled = false;
            FilterCommand = new RelayCommand(Execute_FilterCommand, CanExecute_Command);
            CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecute_Command);
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
                    //IsCityEnabled = true;
                    OnPropertyChanged(nameof(Cities));
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
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
        /*
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
        */
        

        private String _selectedCity;
        public String SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;

            }
        }

        private RelayCommand filterCommand;
        public RelayCommand FilterCommand
        {
            get { return filterCommand; }
            set
            {
                filterCommand = value;
            }
        }

        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get { return cancelCommand; }
            set
            {
                cancelCommand = value;
            }
        }

        

        private void Execute_CancelCommand(object obj)
        {
            CloseAction();
        }

        private void Execute_FilterCommand(object obj)
        {
            Guest2MainWindowViewModel.ToursMainList.Clear();
            Location location = _locationRepository.FindLocation(SelectedCountry, SelectedCity);
            
            if (TourGuestNum.Equals(""))
            {
                return;
            }
            foreach (Tour tour in Guest2MainWindowViewModel.ToursCopyList)
            {
                if (tour.Language.ToLower().Contains(TourGuestNum.ToLower()) && (tour.Location.Country == SelectedCountry || SelectedCountry ==null) && (tour.Location.City == SelectedCity || SelectedCity == null) && tour.Duration.ToString().ToLower().Contains(TourDuration.ToLower()) &&
                                (tour.MaxGuestNum - int.Parse(TourGuestNum) >= 0 || TourGuestNum.Equals("")))
                {
                    Guest2MainWindowViewModel.ToursMainList.Add(tour);
                }
            }
            CloseAction();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
