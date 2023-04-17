using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.WPF.View;
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

        public static ObservableCollection<ReservationDisplacementRequest> RequestsList { get; set; }
        public static ObservableCollection<OwnerReview> RateOwnerList { get; set; }


        public OwnerReview SelectedRate { get; set; }

        public static ObservableCollection<GuestReview> RatesList { get; set; }

        public static ObservableCollection<GuestReview> FilteredRates { get; set; }

        public static ObservableCollection<Accommodation> AccommodationsCopyList { get; set; }

        private readonly IMessageBoxService messageBoxService;

        public Accommodation SelectedAccommodation { get; set; }
        public AccommodationReservation SelectedReservation { get; set; }

        public ReservationDisplacementRequest SelectedRequest { get; set; }
        public User LoggedInUser { get; set; }

        private readonly AccommodationRepository _accommodationRepository;

        private readonly AccommodationReservationRepository _reservationRepository;

        private readonly ReservationDisplacementRequestRepository reservationDisplacementRequest;

        private readonly LocationRepository _locationRepository;

        private readonly AccommodationReservationRepository accommodationReservationRepository;
        
        private readonly OwnerReviewRepository ownerReviewRepository;

        private readonly AccommodationReservationService accommodationReservationService;
        public static ObservableCollection<String> Countries { get; set; }

        private readonly GuestReviewRepository guestReviewRepository;

        private readonly GuestReviewService guestReviewService;
        



        public Guest1MainWindowViewModel(User user, IMessageBoxService _messageBoxService)
		{
			
            _accommodationRepository= new AccommodationRepository();
            accommodationReservationService = new AccommodationReservationService();
            ownerReviewRepository=new OwnerReviewRepository();
            reservationDisplacementRequest=new ReservationDisplacementRequestRepository();
            accommodationReservationRepository=new AccommodationReservationRepository();
            messageBoxService = _messageBoxService;
            _reservationRepository =new AccommodationReservationRepository();
            _locationRepository = new LocationRepository();
            guestReviewRepository= new GuestReviewRepository();
            guestReviewService = new GuestReviewService();
            InitializeProperties(user);
			      InitializeCommands();
            CheckUpdateCondition();
            FilteringRates();

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

        private RelayCommand notifications;
        public RelayCommand Notifications
        {
            get { return notifications; }
            set
            {
                notifications = value;
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

        private RelayCommand rateReservation;

        public RelayCommand RateReservation
        {
            get { return rateReservation; }
            set
            {
                rateReservation = value;
            }
        }

        private RelayCommand changeReservation;

        public RelayCommand ChangeDate
        {
            get { return changeReservation; }
            set
            {
                changeReservation = value;
            }
        }
        private RelayCommand accommodations;
        public RelayCommand AccommodationsO
        {
            get { return accommodations; }
            set
            {
                accommodations = value;
            }
        }
        private RelayCommand reservations;
        public RelayCommand ReservationsO
        {
            get { return reservations; }
            set
            {
                reservations = value;
            }
        }

        private RelayCommand reviews;
        public RelayCommand YourReviewsO
        {
            get { return reviews; }
            set
            {
                reviews = value;
            }
        }

        private RelayCommand showGallery;
        public RelayCommand ShowMoreOwnerReview
        {
            get { return showGallery; }
            set
            {
                showGallery = value;
            }
        }

        private RelayCommand _requestes;
        public RelayCommand SeeRequestes
        {
            get => _requestes;
            set
            {
                if (value != _requestes)
                {
                    _requestes = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand request;
        public RelayCommand Request
        {
            get => request;
            set
            {
                if (value != request)
                {
                    request = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand user;
        public RelayCommand UserProfil
        {
            get => user;
            set
            {
                if (value != user)
                {
                    user = value;
                    OnPropertyChanged();
                }
            }
        }


        private void InitializeCommands()
        {
            ReserveAccommodation = new RelayCommand(Execute_ReserveAccommodation, CanExecute_Command);
            ViewGallery = new RelayCommand(Execute_ViewGallery, CanExecute_Command);
            FilterAccommodation = new RelayCommand(Execute_FilterAccommodation, CanExecute_Command);
            RestartFiltering = new RelayCommand(Execute_RestartFiltering, CanExecute_Command);
            CancelReservation = new RelayCommand(Execute_CancelReservation, CanExecute_Command);
            RateReservation = new RelayCommand(Execute_RateReservation, CanExecute_Command);
            ChangeDate = new RelayCommand(Execute_ChangeReservation, CanExecute_Command);
            Notifications = new RelayCommand(Execute_Notifications, CanExecute_Command);
            SeeRequestes = new RelayCommand(Execute_SeeRequestes, CanExecute_Command);
            UserProfil= new RelayCommand(Execute_UserProfile, CanExecute_Command);
            ShowMoreOwnerReview= new RelayCommand(Execute_ShowMoreOwnerReview, CanExecute_Command);
            TabCommands();
            
        }

        private void Execute_UserProfile(object obj)
        {
            throw new NotImplementedException();
        }

        private void TabCommands()
        {
            AccommodationsO = new RelayCommand(Execute_Accommodations, CanExecute_Command);
            ReservationsO = new RelayCommand(Execute_Reservations, CanExecute_Command);
            YourReviewsO = new RelayCommand(Execute_Reviews, CanExecute_Command);
            Request = new RelayCommand(Execute_Request, CanExecute_Command);
        }

        private void Execute_Request(object obj)
        {
            SelectedIndex = 3;
        }

        private void Execute_Reviews(object obj)
        {
            SelectedIndex = 2;
        }

        private void Execute_Reservations(object obj)
        {
            SelectedIndex = 1;
        }

        private void Execute_Accommodations(object obj)
        {
            SelectedIndex = 0;
        }

        private int _selectedIndex { get; set; }
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (_selectedIndex != value)
                {
                    _selectedIndex = value;
                    OnPropertyChanged(nameof(SelectedIndex));
                }
            }
        }

        private void Execute_SeeRequestes(object obj)
        {
            SelectedIndex = 3;
        }

        private int latestNotificationIndex = -1;

        private void Execute_Notifications(object obj)
        {
            bool hasNewNotifications = false;

            hasNewNotifications = IsNotified(hasNewNotifications);

            if (!hasNewNotifications)
            {
                messageBoxService.ShowMessage("Nema novih obavestenja!");
            }
        }

        private bool IsNotified(bool hasNewNotifications)
        {
            for (int i = latestNotificationIndex + 1; i < RequestsList.Count; i++)
            {
                ReservationDisplacementRequest r = RequestsList[i];

                if (r.Type == RequestType.Approved)
                {
                    messageBoxService.ShowMessage("Vlasnik je odobrio zahtev za pomeranje rezervacije " + r.Reservation.Accommodation.Name);
                    hasNewNotifications = true;
                }

                else if (r.Type == RequestType.Rejected)
                {
                    messageBoxService.ShowMessage("Vlasnik nije odobrio zahtev za pomeranje rezervacije " + r.Reservation.Accommodation.Name);
                    hasNewNotifications = true;
                }

                latestNotificationIndex = i;
            }

            return hasNewNotifications;
        }


        private void Execute_ChangeReservation(object obj)
        {
            if (SelectedReservation!= null && IsRequested() && IsRequestPossible())
            {
                ChangeReservationDate changeDate = new ChangeReservationDate(LoggedInUser, SelectedReservation, SelectedRequest, messageBoxService);
                changeDate.Show();
            }
            else if(SelectedReservation==null)
            {
                messageBoxService.ShowMessage("Morate prvo selektovati rezervaciju za koju zelite da promenite datum!");
            }

        }
        private bool IsRequestPossible()
        {
            if(SelectedReservation.StartDate<DateOnly.FromDateTime(DateTime.Today) || SelectedReservation.EndDate<DateOnly.FromDateTime(DateTime.Today))
            {
                messageBoxService.ShowMessage("Ne mozete pomeriti rezervaciju koja jos uvek traje ili koja se zavrsila ");
                return false;
            }
            return true;
        }
        private bool IsRequested()
        {
            foreach (ReservationDisplacementRequest r in RequestsList)
            {
                if (SelectedReservation.Id == r.ReservationId)
                {
                    messageBoxService.ShowMessage("Vec ste poslali zahtev za pomeranje ove rezervacije!");
                    return false;
                }

            }

            return true;
        }

        private void Execute_RateReservation(object obj)
        {
            if (SelectedReservation != null)
            {
                foreach (OwnerReview r in RateOwnerList)
                {
                    if (r.ReservationId == SelectedReservation.Id)
                    {
                        messageBoxService.ShowMessage("Vec ste ocenili ovaj smestaj! Pogledajte u REVIEWS vasu ocenu !");
                        return;
                    }
                    
                        
                        
                    

                }
                CheckRateMethod();

            }
            else
            {
                messageBoxService.ShowMessage("Morate prvo izabrati rezervaciju koju ocenjujete!");
            }
}

        private void CheckRateMethod()
        {
            int daysSinceEnd = DaysSinceEnd();

            if (daysSinceEnd < 0)
            {
                messageBoxService.ShowMessage("Ne mozete oceniti smestaj, jer jos uvek niste isti napustili!");
            }
            else if (daysSinceEnd <= 5)
            {
                RateOwner rateOwner = new RateOwner(LoggedInUser, SelectedReservation);
                rateOwner.Show();
            }
            else
            {
                messageBoxService.ShowMessage("Smestaj se ne može oceniti, jer je prosao rok za ocenjivanje!");

            }
        }

        private int DaysSinceEnd()
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly endDate = accommodationReservationService.endDate(SelectedReservation.Id);
            DateTimeOffset todayOffset = new DateTimeOffset(today.Year, today.Month, today.Day, 0, 0, 0, TimeSpan.Zero);
            DateTimeOffset startOffset = new DateTimeOffset(endDate.Year, endDate.Month, endDate.Day, 0, 0, 0, TimeSpan.Zero);
            TimeSpan timeSinceStart = todayOffset - startOffset;
            int daysSinceEnd = timeSinceStart.Days;
            return daysSinceEnd;
        }

        private void Execute_CancelReservation(object obj)
        {
            if (SelectedReservation != null)
            {
                int daysSinceStart, minDaysCancellation;
                InitializeFieldsForCanceling(out daysSinceStart, out minDaysCancellation);

                if (daysSinceStart >= minDaysCancellation)
                {
                    _reservationRepository.Delete(SelectedReservation);
                    AccommodationsReservationList.Remove(SelectedReservation);
                }
                else
                {
                    messageBoxService.ShowMessage("Rezervacija se ne može otkazati, jer je prosao rok za otkazivanje!");

                }
            }
            else
            {
                messageBoxService.ShowMessage("Morate prvo izabrati rezervaciju koju otkazujete!");
            }
        }

        private void InitializeFieldsForCanceling(out int daysSinceStart, out int minDaysCancellation)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            DateOnly startDate = accommodationReservationService.startDate(SelectedReservation.Id);
            DateTimeOffset todayOffset = new DateTimeOffset(today.Year, today.Month, today.Day, 0, 0, 0, TimeSpan.Zero);
            DateTimeOffset startOffset = new DateTimeOffset(startDate.Year, startDate.Month, startDate.Day, 0, 0, 0, TimeSpan.Zero);
            TimeSpan timeSinceStart = startOffset - todayOffset;
            daysSinceStart = timeSinceStart.Days;
            minDaysCancellation = SelectedReservation.Accommodation.DaysBeforeCancel;
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

        private void CheckUpdateCondition()
        {
            foreach (AccommodationReservation a in AccommodationsReservationList)
            {
                foreach (ReservationDisplacementRequest r in RequestsList)
                {
                    if (a.Id == r.ReservationId && r.Type==RequestType.Approved)
                    {
                        a.StartDate = r.NewStartDate;
                        a.EndDate = r.NewEndDate;
                        accommodationReservationRepository.Update(a);
                     }
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

            bool matchesName, matchesCountry, matchesCity, matchesType, matchesGuestNum, matchesReservationNum;

            FilteringConditions(a, location, out matchesName, out matchesCountry, out matchesCity, out matchesType, out matchesGuestNum, out matchesReservationNum);

            if (matchesName && matchesCountry && matchesCity && matchesType && matchesGuestNum && matchesReservationNum)
            {
                a.Location = _locationRepository.GetById(a.IdLocation);
                AccommodationsMainList.Add(a);
            }

        }

        private void FilteringConditions(Accommodation a, Location location, out bool matchesName, out bool matchesCountry, out bool matchesCity, out bool matchesType, out bool matchesGuestNum, out bool matchesReservationNum)
        {
            matchesName = txtName == null || a.Name.ToLower().Contains(txtName.ToLower());
            matchesCountry = SelectedCountry == null || location.Country == SelectedCountry;
            matchesCity = SelectedCity == null || location.City == SelectedCity;
            matchesType = ComboboxType == null || a.Type.ToString() == ComboboxType;
            matchesGuestNum = txtGuestNum == null || a.MaxGuestNum >= Convert.ToInt32(txtGuestNum);
            matchesReservationNum = txtReservationNum == null || a.MinReservationDays <= Convert.ToInt32(txtReservationNum);
        }

        private void Execute_ViewGallery(object sender)
        {
            ViewAccommodationGallery viewAccommodationGallery = new ViewAccommodationGallery(SelectedAccommodation);
            viewAccommodationGallery.Show();
        }

        private void Execute_ShowMoreOwnerReview(object sender)
        {
            ViewOwnerReviewGallery viewOwnerReviewGallery = new ViewOwnerReviewGallery(SelectedRate);
            viewOwnerReviewGallery.Show();
        }

        private void Execute_ReserveAccommodation(object sender)
        {

            if (SelectedAccommodation != null)
            {
                CreateReservation createReservation = new CreateReservation(SelectedAccommodation, LoggedInUser, SelectedReservation, messageBoxService);
                createReservation.Show();
            }
            else messageBoxService.ShowMessage("Morate izabrati smestaj koji zelite da rezervisete! ");
            
        }
        


        private void InitializeProperties(User user)
        {
            LoggedInUser = user;
            AccommodationsMainList = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            AccommodationsCopyList = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            RateOwnerList = new ObservableCollection<OwnerReview>(ownerReviewRepository.GetByUser(user));
            RequestsList= new ObservableCollection<ReservationDisplacementRequest>(reservationDisplacementRequest.GetByUser(user));
            AccommodationsReservationList = new ObservableCollection<AccommodationReservation>(_reservationRepository.GetByUser(user));
            Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
            Cities = new ObservableCollection<String>();
            RatesList = new ObservableCollection<GuestReview>(guestReviewRepository.GetByUser(LoggedInUser));
            FilteredRates = new ObservableCollection<GuestReview>();
            IsCityEnabled = false;
            


            BindData();

        }

    

        private void BindData()
        {
            BindLocation();
            BindAccommodation();
            BindReservation();
            BindRequestReservation();
            BindRate();

        }

        private void BindRequestReservation()
        {
            foreach (ReservationDisplacementRequest accRes in RequestsList)
            {
                accRes.Reservation = accommodationReservationService.GetById(accRes.ReservationId);
            }
        }

        private void BindReservation()
        {
            foreach (OwnerReview accRes in RateOwnerList)
            {
                accRes.Reservation = accommodationReservationService.GetById(accRes.ReservationId);
            }
        }

        private void BindAccommodation()
        {
            foreach (AccommodationReservation accRes in AccommodationsReservationList)
            {
                accRes.Accommodation = _accommodationRepository.GetById(accRes.IdAccommodation);
            }
        }

        private void BindLocation()
        {
            foreach (Accommodation accommodation in AccommodationsMainList)
            {
                accommodation.Location = _locationRepository.GetById(accommodation.IdLocation);
            }
        }

        private void BindRate()
        {
            foreach(GuestReview guest in RatesList)
            {
                guest.Reservation= accommodationReservationService.GetById(guest.IdReservation);
            }
        }


        private bool CanExecute_Command(object parameter)
        {
            return true;
        }

        private void FilteringRates()
        {
            foreach(GuestReview guest in RatesList)
            {
                if(guestReviewService.IsElegibleForDisplay(guest))
                {
                    FilteredRates.Add(guest);
                }
            }
        }

    }
}
