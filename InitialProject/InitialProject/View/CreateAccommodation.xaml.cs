using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
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
	/// Interaction logic for CreateAccommodation.xaml
	/// </summary>
	public partial class CreateAccommodation : Window
	{
		private readonly AccommodationRepository _repository;

		public static User LoggedInUser { get; set; }

		private readonly LocationRepository _locationRepository;

		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public CreateAccommodation(User user)
		{

			InitializeComponent();
			DataContext = this;
			LoggedInUser = user;
			_repository = new AccommodationRepository();
			_locationRepository = new LocationRepository();
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

		private void CancelCreate_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void ConfirmCreate_Click(object sender, RoutedEventArgs e)
		{
			Location Location1 = new Location(City, Country);
			Location savedLocation = _locationRepository.Save(Location1);
			Accommodation Accommodation1 = new Accommodation(AName,savedLocation, (AccommodationType)Enum.Parse(typeof(AccommodationType), AccommodationType), int.Parse(MaxGuestNum), int.Parse(MinResevationDays), int.Parse(DaysBeforeCancel), LoggedInUser.Id);
			Accommodation savedAccommodation = _repository.Save(Accommodation1);
			OwnerMainWindow.Accommodations.Add(Accommodation1);
			Close();
		}
	}
}
