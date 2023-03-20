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
	/// Interaction logic for CreateAccommodation.xaml
	/// </summary>
	public partial class CreateAccommodation : Window
	{
		private readonly AccommodationRepository _accommodationRepository;

		public static User LoggedInUser { get; set; }

		public static String SelectedCountry { get; set; }

		public static String SelectedCity { get; set; }

		private readonly LocationRepository _locationRepository;

		private readonly ImageRepository _imageRepository;

		public static ObservableCollection<String> Countries { get; set; }

		public static ObservableCollection<String> Cities { get; set; }

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
			_accommodationRepository = new AccommodationRepository();
			_locationRepository = new LocationRepository();
			_imageRepository = new ImageRepository();
			Countries = new ObservableCollection<String>(_locationRepository.GetAllCountries());
			ComboBoxCountry.SelectedIndex = 0;
			ComboboxType.SelectedIndex = 0;
			
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
			Location location = _locationRepository.FindLocation(Country, City);
			Accommodation Accommodation1 = new Accommodation(AName, location.Id, location, (AccommodationType)Enum.Parse(typeof(AccommodationType), AccommodationType), int.Parse(MaxGuestNum), int.Parse(MinResevationDays), int.Parse(DaysBeforeCancel), LoggedInUser.Id);
			Accommodation savedAccommodation = _accommodationRepository.Save(Accommodation1);
			StoreImage(savedAccommodation);
			OwnerMainWindow.Accommodations.Add(Accommodation1);
			Close();
		}

		private void StoreImage(Accommodation savedAccommodation)
		{
			foreach (string urls in ImageUrl.Split(','))
			{
				Image image1 = new Image(urls, savedAccommodation.Id, 0);
				Image savedImage = _imageRepository.Save(image1);
			}
		}

		private void ComboBox_DropDownClosed(object sender, EventArgs e)
		{
			
			Country=ComboBoxCountry.SelectedItem.ToString();
			Cities = new ObservableCollection<String>(_locationRepository.GetCities(Country));

			ComboboxCity.ItemsSource=Cities;
			ComboboxCity.SelectedIndex=0;
			ComboboxCity.IsEnabled = true;
		}

		private void ComboboxCity_DropDownClosed(object sender, EventArgs e)
		{
			City = ComboboxCity.SelectedItem.ToString();
		}

		
	}
}
