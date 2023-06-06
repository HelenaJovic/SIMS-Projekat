using System;
using System.Collections.Generic;
using InitialProject.Domain.Model;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using InitialProject.Applications.UseCases;
using InitialProject.Commands;

namespace InitialProject.WPF.ViewModel
{
	public class AccommodationUCViewModel : ViewModelBase
	{ 

       private readonly AccommodationService accommodationService;

		private readonly AccommodationReservationService accommodationReservationService;

		private readonly LocationService locationService;

		public static Accommodation SelectedAccommodation { get; set; }

		public static User LoggedInUser { get; set; }

		public static Location BussiestLocation { get; set; }

		public static List<Location> WorstLocations { get; set; }

		public static Dictionary<Location, int> locationDictionary { get; set; }


		public static ObservableCollection<Accommodation> Accommodations { get; set; }

	

		public delegate void EventHandler1();

		public delegate void EventHandler2();


		public event EventHandler1 AddEvent;

		public event EventHandler2 DeleteEvent;

		public event Action<Accommodation> StatisticsEvent;

		public event Action<Accommodation> ViewMoreEvent;



		private RelayCommand delete;
		public RelayCommand Delete
		{
			get { return delete; }
			set
			{
				delete = value;
			}
		}

		private RelayCommand addAccommodation;
		public RelayCommand AddAccommodation
		{
			get { return addAccommodation; }
			set
			{
				addAccommodation = value;
			}
		}

		private RelayCommand viewMore;
		public RelayCommand ViewMore
		{
			get { return viewMore; }
			set
			{
				viewMore = value;
			}
		}

		private RelayCommand statistics;
		public RelayCommand Statistics
		{
			get { return statistics; }
			set
			{
				statistics = value;
			}
		}
		public AccommodationUCViewModel(User user)
		{
			LoggedInUser = user;
			accommodationService = new AccommodationService();
			locationService = new LocationService();
			accommodationReservationService = new AccommodationReservationService();
			InitializeProperties();
			InitializeCommands();
		}

		private void InitializeProperties()
		{
			Accommodations = new ObservableCollection<Accommodation>(accommodationService.GetByUser(LoggedInUser));
			locationDictionary = locationService.CalculateReservationCountByLocation(accommodationService.GetByUser(LoggedInUser), accommodationReservationService.GetByOwnerId(LoggedInUser.Id));
			BussiestLocation = locationService.FindBusiestLocation(locationDictionary);
			WorstLocations = locationService.FindWorstLocations(locationDictionary);
		}

		public void InitializeCommands()
		{
			Delete = new RelayCommand(Execute_Delete, CanExecute);
			AddAccommodation = new RelayCommand(Execute_AddAccommodation, CanExecute);
			ViewMore = new RelayCommand(Execute_ViewMore, CanExecute);
			Statistics = new RelayCommand(Execute_Statistics, CanExecute);
		}

		private bool CanExecute(object parameter)
		{
			return true;
		}

		private void Execute_Delete(object sender)
		{
			for (int i = Accommodations.Count - 1; i >= 0; i--)
			{
				var accommodation = Accommodations[i];
				if (WorstLocations.Contains(accommodation.Location))
				{
					accommodationService.Delete(accommodation);
					Accommodations.RemoveAt(i);
				}
			}

		}

		private void Execute_Statistics(object sender)
		{
			var selectedAccommodation = SelectedAccommodation;


			StatisticsEvent?.Invoke(selectedAccommodation);

		}

		private void Execute_AddAccommodation(object sender)
		{
			AddEvent?.Invoke();
		}

		private void Execute_ViewMore(object sender)
		{
			var selectedAccommodation = SelectedAccommodation;

			
			ViewMoreEvent?.Invoke(selectedAccommodation);
		}
	}
}
