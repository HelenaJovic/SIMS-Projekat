﻿using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModel
{
	public class ReservationMovingViewModel : ViewModelBase
	{
		public static ObservableCollection<AccommodationReservation> Reservations { get; set; }

		public static ObservableCollection<ReservationDisplacementRequest> Requests { get; set; }

		private readonly AccommodationReservationService accommodationReservationService;

		private readonly ReservationDisplacementRequestService reservationDisplacementRequestService;

		private readonly IMessageBoxService messageBoxService;

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
			messageBoxService = new MessageBoxService();
			InitializeProperties();
			InitializeCommands();
		}

		public void InitializeProperties()
		{
			Reservations = new ObservableCollection<AccommodationReservation>(accommodationReservationService.GetAll());
			Requests = new ObservableCollection<ReservationDisplacementRequest>(reservationDisplacementRequestService.GetAll());
			foreach (ReservationDisplacementRequest request in reservationDisplacementRequestService.GetAll())
			{
				if (request.Type == RequestType.Rejected || request.Type == RequestType.Approved)
				{
					Requests.Remove(request);
				}
			}

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
			var selectedRequest = SelectedRequest;

			reservationDisplacementRequestService.BindPaticularData(SelectedRequest);

			List<AccommodationReservation> reservations = accommodationReservationService.GetByAccommodationId(SelectedRequest.Reservation.IdAccommodation);

			List<AccommodationReservation> overlappingReservations = accommodationReservationService.GetOverlappingReservations(SelectedRequest.Reservation.IdAccommodation, SelectedRequest.NewStartDate, SelectedRequest.NewEndDate, reservations);

			overlappingReservations.Remove(accommodationReservationService.GetById(SelectedRequest.ReservationId));

			if (overlappingReservations.Count == 0)
			{
				messageBoxService.ShowMessage("Izabrani termin je slobodan. Mozete izvrsiti pomeranje rezervacije");
			}
			else
			{
				messageBoxService.ShowMessage("Izabrani termin nije slobodan.Ne  mozete izvrsiti pomeranje rezervacije");
			}
		}




		private void Execute_Refuse(object sender)
		{
			var selectedRequest = SelectedRequest;
			selectedRequest.Type = RequestType.Rejected;
			reservationDisplacementRequestService.Update(selectedRequest);
			Requests.Remove(selectedRequest);
		}

		private void Execute_Accept(object sender)
		{
			var selectedRequest = SelectedRequest;
			AccommodationReservation reservation = accommodationReservationService.GetById(selectedRequest.ReservationId);
			reservation.StartDate = selectedRequest.NewStartDate;
			reservation.EndDate = selectedRequest.NewEndDate;
			selectedRequest.Type = RequestType.Approved;
			reservationDisplacementRequestService.Update(selectedRequest);
			accommodationReservationService.Update(reservation);
			Requests.Remove(selectedRequest);

			RefreshReservations();

		}

		private void RefreshReservations()
		{
			Reservations.Clear();
			foreach (AccommodationReservation accommodationReservation in accommodationReservationService.GetAll())
			{
				Reservations.Add(accommodationReservation);
			}
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