using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
	internal class AccommodationReservationService
	{
		private readonly IAccommodationReservationRepository accommodationReservationRepository;

		private readonly GuestReviewService guestReviewService;

		private readonly AccommodationService accommodationService;

		private readonly IUserRepository userRepository;

		

		

		
		public AccommodationReservationService()
		{
			userRepository = Inject.CreateInstance<IUserRepository>();
			accommodationReservationRepository = Inject.CreateInstance<IAccommodationReservationRepository>();
			accommodationService = new AccommodationService();
			guestReviewService = new GuestReviewService();
			

		}


		public void BindData(List<AccommodationReservation> reservations)
		{

			foreach (AccommodationReservation res in reservations)
			{
				res.Guest = userRepository.GetById(res.IdGuest);
				res.Accommodation = accommodationService.GetById(res.IdAccommodation);
			}

		}

		public void BindParticularData(AccommodationReservation reservation)
		{
			reservation.Guest = userRepository.GetById(reservation.IdGuest);
			reservation.Accommodation = accommodationService.GetById(reservation.IdAccommodation);
			
		}

		public List<AccommodationReservation> GetAll()
		{
			List<AccommodationReservation> reservations = new List<AccommodationReservation>();
			reservations = accommodationReservationRepository.GetAll();
			if(reservations.Count > 0)
			{
				BindData(reservations);
			}
			
			return reservations;
		}

		public bool IsElegibleForReview(DateOnly today, AccommodationReservation res)
		{
			List<GuestReview> guestReviews = guestReviewService.GetAll(); ;

			bool toAdd = true;
			foreach (GuestReview review in guestReviews)
			{

				if (res.Id == review.IdReservation)
				{
					toAdd = false;
					break;
				}

			}

			return res.EndDate < today && today.DayNumber - res.EndDate.DayNumber <= 5 && toAdd;
		}

		public AccommodationReservation GetById(int id)
		{
			AccommodationReservation reservation = accommodationReservationRepository.GetById(id);
			if(reservation != null)
			{
				BindParticularData(reservation);
			}
			
			return reservation;
		}

		public List<DateOnly> GetAllStartDates(int id)
		{
			List<DateOnly> dates = new List<DateOnly>();
			List<AccommodationReservation> reservations1;
			reservations1 = new List<AccommodationReservation>(accommodationReservationRepository.GetAll());
			foreach (AccommodationReservation reservation in reservations1)
			{
				if (reservation.IdAccommodation == id)
				{
					dates.Add(reservation.StartDate);
				}
			}
			return dates;
		}

		public DateOnly startDate(int id)
		{
			List<AccommodationReservation> reservations1;
			reservations1 = new List<AccommodationReservation>(accommodationReservationRepository.GetAll());
			foreach (AccommodationReservation a in reservations1)
			{
				if (a.Id== id)
				{
					return a.StartDate;
				}
			}
			throw new ArgumentException("The specified reservation was not found in the collection.");
		}

		public DateOnly endDate(int id)
		{
			List<AccommodationReservation> reservations1;
			reservations1 = new List<AccommodationReservation>(accommodationReservationRepository.GetAll());
			foreach (AccommodationReservation a in reservations1)
			{
				if (a.Id == id)
				{
					return a.EndDate;
				}
			}
			throw new ArgumentException("The specified reservation was not found in the collection.");
		}

		public List<DateOnly> GetAllEndDates(int id)
		{
			List<AccommodationReservation> reservations1;
			reservations1 = new List<AccommodationReservation>(accommodationReservationRepository.GetAll());
			List<DateOnly> dates = new List<DateOnly>();
			foreach (AccommodationReservation reservation in reservations1)
			{
				if (reservation.IdAccommodation == id)
				{
					dates.Add(reservation.EndDate);
				}
			}
			return dates;
		}

		public List<AccommodationReservation> GetByAccommodationId(int idAccommodation)
		{
			List<AccommodationReservation> reservations = accommodationReservationRepository.GetByAccommodationId(idAccommodation);
			if(reservations.Count != 0)
			{
				BindData(reservations);
			}
			return reservations;
		}

		public List<AccommodationReservation> GetOverlappingReservations(int accommodationId, DateOnly NewStartDate, DateOnly NewEndDate, List<AccommodationReservation> reservations)
		{
			List<AccommodationReservation> overlappingReservations = new List<AccommodationReservation>();
			 overlappingReservations = reservations.Where(r => r.IdAccommodation == accommodationId && r.EndDate >= NewStartDate && r.StartDate <= NewEndDate).ToList();

			return overlappingReservations;
		}

		public AccommodationReservation Update(AccommodationReservation accommodationReservation)
		{
			return accommodationReservationRepository.Update(accommodationReservation);
		}

		public List<AccommodationReservation> GetByOwnerId(int id)
		{
			List<AccommodationReservation> reservations = new List<AccommodationReservation>();
			List<AccommodationReservation> AllReservations = accommodationReservationRepository.GetAll();
			BindData(AllReservations);

			foreach(AccommodationReservation r in AllReservations)
			{
				if (r.Accommodation.IdUser == id)
				{
					reservations.Add(r);
				}
			}

			return reservations;
		}
	}
}
