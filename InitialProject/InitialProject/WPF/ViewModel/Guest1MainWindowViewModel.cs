using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class Guest1MainWindowViewModel : ViewModelBase
    {
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public static ObservableCollection<Accommodation> AccommodationsMainList { get; set; }
        public static ObservableCollection<AccommodationReservation> AccommodationsReservationList { get; set; }

        public static ObservableCollection<Accommodation> AccommodationsCopyList { get; set; }

        private readonly IMessageBoxService messageBoxService;

        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public User LoggedInUser { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _reservationRepository;
        private readonly LocationRepository _locationRepository;
        private readonly UserRepository _userRepository;
        private readonly AccommodationReservationService accommodationReservationService;
        private readonly AccommodationService accommodationService;

       

        public static ObservableCollection<String> Countries { get; set; }
        



        public Guest1MainWindowViewModel(User user, IMessageBoxService _messageBoxService)
		{
			accommodationService = new AccommodationService();
            _accommodationRepository= new AccommodationRepository();
            accommodationReservationService = new AccommodationReservationService();
			_userRepository = new UserRepository();
            messageBoxService = _messageBoxService;
            
            _reservationRepository =new AccommodationReservationRepository();
            _locationRepository = new LocationRepository();
            InitializeProperties(user);
			InitializeCommands();
			BindData();
			


		}
        private RelayCommand filterAccommodation;
        public RelayCommand FilterAccommodation
        {
            get { return filterAccommodation; }
            set
            {
                filterAccommodation = value;
            }
        }

        private RelayCommand cancelReservation;
        public RelayCommand CancelReservation
        {
            get { return cancelReservation; }
            set
            {
                cancelReservation = value;
            }
        }

        private RelayCommand restartFiltering;
        public RelayCommand RestartFiltering
        {
            get { return restartFiltering; }
            set
            {
                restartFiltering = value;
            }
        }

        private RelayCommand reserveAccommodation;
        public RelayCommand ReserveAccommodation
        {
            get { return reserveAccommodation; }
            set
            {
                reserveAccommodation = value;
            }
        }

        private RelayCommand wheneverWherever;

        public RelayCommand WheneverWherever
        {
            get { return wheneverWherever; }
            set
            {
                wheneverWherever = value;
            }
        }

        private RelayCommand viewGallery;

        public RelayCommand ViewGallery
        {
            get { return viewGallery; }
            set
            {
                viewGallery = value;
            }
        }

        private void InitializeCommands()
        {
            ReserveAccommodation = new RelayCommand(Execute_ReserveAccommodation, CanExecute_Command);
            ViewGallery = new RelayCommand(Execute_ViewGallery, CanExecute_Command);
            FilterAccommodation = new RelayCommand(Execute_FilterAccommodation, CanExecute_Command);
            RestartFiltering = new RelayCommand(Execute_RestartFiltering, CanExecute_Command);
            CancelReservation = new RelayCommand(Execute_CancelReservation, CanExecute_Command);

        }

        private void Execute_CancelReservation(object obj)
        {
            if (SelectedReservation != null)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);
                DateOnly startDate = accommodationReservationService.startDate(SelectedReservation.Id);
                DateTimeOffset todayOffset = new DateTimeOffset(today.Year, today.Month, today.Day, 0, 0, 0, TimeSpan.Zero);
                DateTimeOffset startOffset = new DateTimeOffset(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, TimeSpan.Zero);
                TimeSpan timeSinceStart = startOffset - todayOffset;
                int daysSinceStart = timeSinceStart.Days;

                // Check if the reservation can be canceled based on the minimum cancellation period
                int minDaysCancellation = SelectedReservation.Accommodation.DaysBeforeCancel;
                if (daysSinceStart >= minDaysCancellation)
                {
                    _reservationRepository.Delete(SelectedReservation);
                    AccommodationsReservationList.Remove(SelectedReservation);
                }
                else
                {
                    
                    
                        string messagee = $"Rezervacija se ne može otkazati, jer je prosao rok za otkazivanje!";
                        messageBoxService.ShowMessage(messagee);
                    
                }
            }
            else
            {
                messageBoxService.ShowMessage("Morate prvo izabrati rezervaciju koju otkazujete!");
            }
        }




        private void Execute_RestartFiltering(object sender)
        {
            AccommodationsMainList.Clear();
            foreach (Accommodation accommodation in AccommodationsCopyList)
            {
                accommodation.Location = _locationRepository.GetById(accommodation.IdLocation);
                AccommodationsMainList.Add(accommodation);
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
                    if (Cities.Count == 0)
                    {
                        IsCityEnabled = false;
                    }
                    else
                    {
                        IsCityEnabled = true;
                    }
                    OnPropertyChanged(nameof(Cities));
                    OnPropertyChanged(nameof(SelectedCountry));
                    OnPropertyChanged(nameof(IsCityEnabled));
                }
            }
        }



        private void Execute_FilterAccommodation(object sender)
        {
            AccommodationsMainList.Clear();
            
            int max = 0;
            int min = 0;

            if (!(int.TryParse(txtGuestNum, out max) || (txtGuestNum==null)) || !(int.TryParse(txtReservationNum, out min) || (txtReservationNum==null)))
            {
                return;
            }
            foreach (Accommodation a in AccommodationsCopyList)
            {
                CheckConditions(max, min, a);

            }
        }

        private string _txtName { get; set; }
        public string txtName
        {
            get { return _txtName; }
            set
            {
                if (_txtName != value)
                {
                    _txtName = value;
                    OnPropertyChanged("_txtName");
                }
            }
        }
        private string _txtGuestNum { get; set; }
        public string txtGuestNum
        {
            get { return _txtGuestNum; }
            set
            {
                if (_txtGuestNum != value)
                {
                    _txtGuestNum = value;
                    OnPropertyChanged("_txtGuestNum");
                }
            }
        }
        private string _txtReservationNum { get; set; }
        public string txtReservationNum
        {
            get { return _txtReservationNum; }
            set
            {
                if (_txtReservationNum != value)
                {
                    _txtReservationNum = value;
                    OnPropertyChanged("_txtReservationNum");
                }
            }
        }
        private string _ComboboxType { get; set; }
        public string ComboboxType
        {
            get { return _ComboboxType; }
            set
            {
                if (_ComboboxType != value)
                {
                    _ComboboxType = value;
                    OnPropertyChanged("_ComboboxType");
                }
            }
        }



        private void CheckConditions(int max, int min, Accommodation a)
        {
            Location location = _locationRepository.GetById(a.IdLocation);


            if ((txtName == null || a.Name.ToLower().Contains(txtName.ToLower())) &&
      (location.Country == SelectedCountry || SelectedCountry == null) &&
      (location.City == SelectedCity || SelectedCity == null) &&
      (ComboboxType == null || a.Type.ToString() == ComboboxType) &&
      ((txtGuestNum == null) || (a.MaxGuestNum >= Convert.ToInt32(txtGuestNum))) &&
      ((txtReservationNum == null) || (a.MinReservationDays <= Convert.ToInt32(txtReservationNum))))
            

            {
                a.Location = _locationRepository.GetById(a.IdLocation);
                AccommodationsMainList.Add(a);
            }

        }

        private void Execute_ViewGallery(object sender)
        {
            ViewAccommodationGallery viewAccommodationGallery = new ViewAccommodationGallery(SelectedAccommodation);
            viewAccommodationGallery.Show();
        }

        private void Execute_ReserveAccommodation(object sender)
        {
            
                if (SelectedAccommodation != null)
                {
                    CreateReservation createReservation = new CreateReservation(SelectedAccommodation, LoggedInUser, SelectedReservation,messageBoxService);
                    createReservation.Show();
                }
                else return;
            
        }
        


        private void InitializeProperties(User user)
        {
            LoggedInUser = user;
            AccommodationsMainList = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            AccommodationsCopyList = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            AccommodationsReservationList = new ObservableCollection<AccommodationReservation>(_reservationRepository.GetByUser(user));
            Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
            Cities = new ObservableCollection<String>();
            IsCityEnabled = false;

        }

        private void BindData()
        {
            foreach (Accommodation accommodation in AccommodationsMainList)
            {
                accommodation.Location = _locationRepository.GetById(accommodation.IdLocation);
            }
            foreach (AccommodationReservation accRes in AccommodationsReservationList)
            {
                accRes.Accommodation = _accommodationRepository.GetById(accRes.IdAccommodation);
            }

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

        private bool CanExecute_Command(object parameter)
        {
            return true;
        }

    }
}
