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

		public event EventHandler1 AddEvent;

		public event EventHandler2 DeleteEvent;



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
		}

		private bool CanExecute(object parameter)
		{
			return true;
		}

		private void Execute_Delete(object sender)
		{

		}

		private void Execute_AddAccommodation(object sender)
		{
			AddEvent?.Invoke();
		}
	}
}
