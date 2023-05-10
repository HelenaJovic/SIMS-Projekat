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

		public GuestReview guestReview = new GuestReview();

		private readonly AccommodationReservationService accommodationReservationService;


		private readonly GuestReviewService guestReviewService;

		private readonly OwnerReviewService ownerReviewService;

		private readonly IMessageBoxService messageBoxService;

		public delegate void EventHandler();

		public event EventHandler Grades;



		public GuestReview GuestReview
		{
			get { return guestReview; }
			set
			{
				guestReview = value;
				OnPropertyChanged("GuestReview");
			}
		}

		/*private int _cleanlinessGrade;
		public int CleanlinessGrade
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

		private int _ruleGrade;
		public int RuleGrade
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
		*/


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
			GuestReview.Validate();

			if (SelectedReservation == null)
			{
				messageBoxService.ShowMessage("Potrebno je izabrati gosta kog zelite da ocenite");
			}
			else
			{
				if (GuestReview.IsValid)
				{

					accommodationReservationService.BindParticularData(SelectedReservation);
					GuestReview newReview = new GuestReview(LoggedInUser.Id, SelectedReservation.Id, guestReview.CleanlinessGrade, guestReview.RuleGrade, Comment1, SelectedReservation.IdGuest);
					GuestReview savedReview = guestReviewService.Save(newReview);
					FilteredReservations.Remove(SelectedReservation);
					Refresh();




					foreach (OwnerReview review in ownerReviewService.GetAll())
					{
						if (savedReview.IdReservation == review.ReservationId)
						{
							ReviewsForOwnerViewModel.FilteredReviews.Add(review);
						}
					}

				}

				else
				{
					OnPropertyChanged(nameof(GuestReview));
				}

			}
			
		}

		public void Refresh()
		{
			guestReview.CleanlinessGrade = 0;
			guestReview.RuleGrade = 0;
			Comment1 = "";
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
