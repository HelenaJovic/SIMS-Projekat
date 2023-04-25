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
		public static ObservableCollection<Accommodation> Accommodations { get; set; }

		private readonly AccommodationService accommodationService;

		public static Accommodation SelectedAccommodation { get; set; }

		public static User LoggedInUser { get; set; }

		public delegate void EventHandler1();

		public delegate void EventHandler2();

		public delegate void EventHandler3();

		public delegate void EventHandler4();

		public event EventHandler1 AddEvent;

		public event EventHandler2 DeleteEvent;

		public event EventHandler3 ViewMoreEvent;

		public event EventHandler4 StatisticsEvent;



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
			accommodationService= new AccommodationService();
			Accommodations = new ObservableCollection<Accommodation>(accommodationService.GetByUser(LoggedInUser));
			InitializeCommands();
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

		}

		private void Execute_Statistics(object sender)
		{

		}

		private void Execute_AddAccommodation(object sender)
		{
			AddEvent?.Invoke();
		}

		private void Execute_ViewMore(object sender)
		{
			ViewMoreEvent?.Invoke();
		}
	}
}
