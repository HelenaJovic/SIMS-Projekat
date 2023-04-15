using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
	public class CreateAccommodationViewModel : ViewModelBase
	{
		private readonly AccommodationService accommodationService;

		private readonly LocationRepository _locationRepository;

		private readonly ImageRepository _imageRepository;

		public static ObservableCollection<String> Countries { get; set; }

		public static User LoggedInUser { get; set; }

		public Action CloseAction { get; set; }


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


		

		private RelayCommand confirmCreate;
		public RelayCommand ConfirmCreate
		{
			get { return confirmCreate; }
			set
			{
				confirmCreate = value;
			}
		}

		private RelayCommand cancelCreate;
		public RelayCommand CancelCreate
		{
			get { return cancelCreate; }
			set
			{
				cancelCreate = value;
			}
		}

		public CreateAccommodationViewModel(User user)
		{
			LoggedInUser = user;
			accommodationService = new AccommodationService();
			_locationRepository = new LocationRepository();
			_imageRepository = new ImageRepository();
			Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
			Cities = new ObservableCollection<String>();
			InitializeCommands();
			

		}

		private void InitializeCommands()
		{
			CancelCreate = new RelayCommand(Execute_CancelCreate, CanExecute_Command);
			ConfirmCreate = new RelayCommand(Execute_ConfirmCreate, CanExecute_Command);
		}

		private bool CanExecute_Command(object parameter)
		{
			return true;
		}

		private void Execute_CancelCreate(object sender)
		{
			CloseAction();
		}

		private void Execute_ConfirmCreate(object sender)
		{
			Location location = _locationRepository.FindLocation(SelectedCountry, SelectedCity);
			Accommodation Accommodation1 = new Accommodation(AName, location.Id, location, (AccommodationType)Enum.Parse(typeof(AccommodationType), AccommodationType), int.Parse(MaxGuestNum), int.Parse(MinResevationDays), int.Parse(DaysBeforeCancel), LoggedInUser.Id);
			Accommodation savedAccommodation = accommodationService.Save(Accommodation1);
			_imageRepository.StoreImage(savedAccommodation, ImageUrl);
			OwnerMainWindowViewModel.Accommodations.Add(Accommodation1);
			CloseAction();
		}

		private string _name;
		public string AName
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


		private string _accommodationType;
		public string AccommodationType
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


		private string _minDaysReservation;
		public string MinResevationDays
		{
			get => _minDaysReservation;
			set
			{
				if (value != _minDaysReservation)
				{
					_minDaysReservation = value;
					OnPropertyChanged();
				}
			}
		}


		private string _daysBeforeCancel;
		public string DaysBeforeCancel
		{
			get => _daysBeforeCancel;
			set
			{
				if (value != _daysBeforeCancel)
				{
					_daysBeforeCancel = value;
					OnPropertyChanged();
				}
			}
		}


		private string _imageUrl;
		public string ImageUrl
		{
			get => _imageUrl;
			set
			{
				if (value != _imageUrl)
				{
					_imageUrl = value;
					OnPropertyChanged();
				}
			}
		}

		

		

		
	}
}
