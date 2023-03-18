using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace InitialProject.View
{
	/// <summary>
	/// Interaction logic for OwnerMainWindow.xaml
	/// </summary>
	public partial class OwnerMainWindow : Window
	{
		public static ObservableCollection<Accommodation> Accommodations { get; set; }

		public static Accommodation SelectedAccommodation { get; set; }

		public static User LoggedInUser { get; set; }

		private readonly AccommodationRepository _accommodationRepository;

		public static ObservableCollection<AccommodationReservation> Reservations { get; set; }

		public static ObservableCollection<AccommodationReservation> AllReservations { get; set; }

		public static ObservableCollection<AccommodationReservation> FilteredReservations { get; set; }

		public static ObservableCollection<GuestReview> Reviews { get; set; }

		public static AccommodationReservation SelectedReservation { get; set; }

		private readonly AccommodationReservationRepository _reservationsRepository;

		private readonly UserRepository _userRepository;

		private readonly GuestReviewRepository _guestReviewRepository;

		private readonly LocationRepository _locationRepository;
		public OwnerMainWindow(User user)
		{
			InitializeComponent();
			DataContext = this;
			LoggedInUser = user;
			_accommodationRepository = new AccommodationRepository();
			_reservationsRepository = new AccommodationReservationRepository();
			_userRepository = new UserRepository();
			_guestReviewRepository = new GuestReviewRepository();
			_locationRepository = new LocationRepository();
			Accommodations = new ObservableCollection<Accommodation>(_accommodationRepository.GetByUser(LoggedInUser));
			AllReservations = new ObservableCollection<AccommodationReservation>(_reservationsRepository.GetAll());
			FilteredReservations = new ObservableCollection<AccommodationReservation>();
			Reviews = new ObservableCollection<GuestReview>(_guestReviewRepository.GetAll());

			BindData();
			FilterReservations();

			Loaded += Window_Loaded;

		}

		private static void FilterReservations()
		{
			Reservations = new ObservableCollection<AccommodationReservation>((AllReservations.ToList().FindAll(c => c.Accommodation.IdUser == LoggedInUser.Id)));


			DateOnly today = DateOnly.FromDateTime(DateTime.Now);

			foreach (AccommodationReservation res in Reservations)
			{
				if (IsElegibleForReview(today, res) == false) continue;
				FilteredReservations.Add(res);

			}
		}

		private void BindData()
		{
			foreach (Accommodation accommodation in Accommodations)
			{
				accommodation.Location = _locationRepository.GetById(accommodation.IdLocation);
			}

			foreach (AccommodationReservation res in AllReservations)
			{
				res.Guest = _userRepository.GetById(res.IdGuest);
				res.Accommodation = _accommodationRepository.GetById(res.IdAccommodation);
			}
		}

		private static bool IsElegibleForReview(DateOnly today, AccommodationReservation res)
		{


			bool toAdd = true;
			foreach (GuestReview review in Reviews)
			{

				if (res.Id == review.IdReservation)
				{
					toAdd = false;
					break;
				}

			}

			return res.EndDate < today && today.DayNumber - res.EndDate.DayNumber <= 5 && toAdd;
		}


		private void Window_Loaded(object sender, RoutedEventArgs e )
		{
			if (FilteredReservations.Count > 0)
			{
				MessageBox.Show("Neki gosti nisu ocenjeni. Ukoliko zelite da ih ocenite otidjite na tab Guests for evaluation");
				
			}
		}

		private void AddAccommodation_Click(object sender, RoutedEventArgs e)
		{
			CreateAccommodation createAccommodation = new CreateAccommodation(LoggedInUser);
			createAccommodation.Show();
		}

		private void RateGuests_Click(object sender, RoutedEventArgs e)

		{
			if (SelectedReservation == null)
			{ 
				MessageBox.Show("Potrebno je izbrati gosta kog zelite da ocenite!");
			}
			else
			{
				RateGuests rateGuests = new RateGuests(LoggedInUser, SelectedReservation);
				rateGuests.Show();
			}
		}

		private void ShowMore_Click(object sender, RoutedEventArgs e)
		{
			ViewGallery viewGallery = new ViewGallery(SelectedAccommodation);
			viewGallery.Show();
		}
	}
}
