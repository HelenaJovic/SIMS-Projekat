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
	public class ReservationMovingViewModel : ViewModelBase
	{
		public static ObservableCollection<AccommodationReservation> Reservations { get; set; }

		public static ObservableCollection<ReservationDisplacementRequest> Requests { get; set; }

		private readonly AccommodationReservationService accommodationReservationService;

		private readonly ReservationDisplacementRequestService reservationDisplacementRequestService;

		public static User LoggedInUser { get; set; }

		private ReservationDisplacementRequest _selectedRequest;
		public ReservationDisplacementRequest SelectedRequest
		{
			get { return _selectedRequest; }
			set
			{
				_selectedRequest = value;
				OnPropertyChanged(nameof(SelectedRequest));
			}
		}

	

		public ReservationMovingViewModel(User user)
		{
			accommodationReservationService = new AccommodationReservationService();
			reservationDisplacementRequestService = new ReservationDisplacementRequestService();
			InitializeProperties();
			InitializeCommands();
		}

		public void InitializeProperties()
		{
			Reservations = new ObservableCollection<AccommodationReservation>(accommodationReservationService.GetAll());
			Requests = new ObservableCollection<ReservationDisplacementRequest>(reservationDisplacementRequestService.GetAll());
		}

		public void InitializeCommands()
		{
			Check = new RelayCommand(Execute_Check, CanExecute_Command);
			Refuse = new RelayCommand(Execute_Refuse, CanExecute_Command);
			Accept = new RelayCommand(Execute_Accept, CanExecute_Command);
		}
		
		private bool CanExecute_Command(object parameter)
		{
			return true;
		}


		private void Execute_Check(object sender)
		{
			var selectedRequest=SelectedRequest;
		}

		private void Execute_Refuse(object sender)
		{
			var selectedRequest = SelectedRequest;
		}

		private void Execute_Accept(object sender)
		{
			var selectedRequest = SelectedRequest;
		}

		private RelayCommand check;
		public RelayCommand Check
		{
			get { return check; }
			set
			{
				check = value;
			}
		}

		private RelayCommand refuse;
		public RelayCommand Refuse
		{
			get { return refuse; }
			set
			{
				refuse = value;
			}
		}

		private RelayCommand accept;
		public RelayCommand Accept
		{
			get { return accept; }
			set
			{
				accept = value;
			}
		}

	}
}
