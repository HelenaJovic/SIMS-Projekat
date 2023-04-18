using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
	public class ReviewsForGuestsUCViewModel : ViewModelBase
	{
		public static User LoggedInUser { get; set; }

		public static AccommodationReservation SelectedReservation { get; set; }

		public static ObservableCollection<AccommodationReservation> AllReservations { get; set; }

		public static ObservableCollection<AccommodationReservation> Reservations { get; set; }

		public static ObservableCollection<AccommodationReservation> FilteredReservations { get; set; }

		private readonly AccommodationReservationService accommodationReservationService;

		private readonly GuestReviewService guestReviewService;

		private readonly OwnerReviewService ownerReviewService;

		private readonly IMessageBoxService messageBoxService;

		public delegate void EventHandler();

		public event EventHandler Grades;



		private string _cleanlinessGrade;
		public string CleanlinessGrade
		{
			get => _cleanlinessGrade;
			set
			{
				if (value != _cleanlinessGrade)
				{
					_cleanlinessGrade = value;
					OnPropertyChanged();
				}
			}
		}

		private string _ruleGrade;
		public string RuleGrade
		{
			get => _ruleGrade;
			set
			{
				if (value != _ruleGrade)
				{
					_ruleGrade = value;
					OnPropertyChanged();
				}
			}
		}

		private int _selectedGrade1;
		public int SelectedGrade1
		{
			get { return _selectedGrade1; }
			set
			{
				_selectedGrade1 = value;
				OnPropertyChanged(nameof(SelectedGrade1));
			}
		}

		private int _selectedGrade2;
		public int SelectedGrade2
		{
			get { return _selectedGrade2; }
			set
			{
				_selectedGrade2 = value;
				OnPropertyChanged(nameof(SelectedGrade2));
			}
		}

		private string _comment;
		public string Comment1
		{
			get => _comment;
			set
			{
				if (value != _comment)
				{
					_comment = value;
					OnPropertyChanged();
				}
			}
		}

		private RelayCommand confirmGrade;
		public RelayCommand ConfirmGrade
		{
			get { return confirmGrade; }
			set
			{
				confirmGrade = value;
			}
		}

		private RelayCommand yourGrades;
		public RelayCommand YourGrades
		{
			get { return yourGrades; }
			set
			{
				yourGrades = value;
			}
		}


		public ReviewsForGuestsUCViewModel(User owner)
		{
			
			accommodationReservationService = new AccommodationReservationService();
			guestReviewService = new GuestReviewService();
			ownerReviewService = new OwnerReviewService();
			messageBoxService = new MessageBoxService();

		    InitializeProperties(owner);
			FilterReservations();

			ConfirmGrade = new RelayCommand(Execute_ConfirmGrade, CanExecute);
			YourGrades = new RelayCommand(Execute_YourGrades, CanExecute);
		}

		private void InitializeProperties(User owner)
		{
			LoggedInUser = owner;
			FilteredReservations = new ObservableCollection<AccommodationReservation>();
			AllReservations = new ObservableCollection<AccommodationReservation>(accommodationReservationService.GetAll());
			Reservations = new ObservableCollection<AccommodationReservation>(accommodationReservationService.GetByOwnerId(LoggedInUser.Id));
		}

		private void Execute_YourGrades(object sender)
		{
			Grades?.Invoke();
		}


		private void Execute_ConfirmGrade(object sender)
		{
			if (SelectedReservation == null)
			{
				messageBoxService.ShowMessage("Potrebno je izabrati gosta kog zelite da ocenite");
			}
			else
			{
				GuestReview newReview = new GuestReview(LoggedInUser.Id, SelectedReservation.Id, int.Parse(CleanlinessGrade), int.Parse(RuleGrade), Comment1, SelectedReservation.IdGuest);
				GuestReview savedReview = guestReviewService.Save(newReview);
				FilteredReservations.Remove(SelectedReservation);



				foreach (OwnerReview review in ownerReviewService.GetAll())
				{
					if (savedReview.IdReservation == review.ReservationId)
					{
						ReviewsForOwnerViewModel.FilteredReviews.Add(review);
					}
				}

			}
			
		}

		private bool CanExecute(object parameter)
		{
			return true;
		}

		private void FilterReservations()
		{

			

			DateOnly today = DateOnly.FromDateTime(DateTime.Now);

			foreach (AccommodationReservation res in Reservations)
			{
				if (accommodationReservationService.IsElegibleForReview(today, res) == false) continue;
				FilteredReservations.Add(res);

			}
		}


	}
}
