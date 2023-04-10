using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
	internal class AccommodationReservationService
	{
		private readonly AccommodationReservationRepository accommodationReservationRepository;

		private readonly GuestReviewRepository guestReviewRepository;

		List<GuestReview> guestReviews;

		List<AccommodationReservation> reservations1;

		DateOnly today;
		public AccommodationReservationService()
		{
			accommodationReservationRepository = new AccommodationReservationRepository();
			guestReviewRepository = new GuestReviewRepository();
			reservations1 = new List<AccommodationReservation>(accommodationReservationRepository.GetAll());
			guestReviews = new List<GuestReview>();
		    today = DateOnly.FromDateTime(DateTime.Now);

		}

		public List<AccommodationReservation> GetAll()
		{
			List<AccommodationReservation> reservations = new List<AccommodationReservation>();
			reservations=accommodationReservationRepository.GetAll();
			return reservations;
		}

		public List<DateOnly> GetAllStartDates(int id)
		{
			List<DateOnly> dates = new List<DateOnly>();
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
			foreach (AccommodationReservation a in reservations1)
			{
				if (a.Id== id)
				{
					return a.StartDate;
				}
			}
			throw new ArgumentException("The specified reservation was not found in the collection.");
		}

		public List<DateOnly> GetAllEndDates(int id)
		{
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

		public bool IsElegibleForReview(DateOnly today, AccommodationReservation res)
		{
			List<GuestReview> guestReviews1;
			guestReviews1 = guestReviewRepository.GetAll();

			bool toAdd = true;
			foreach (GuestReview review in guestReviews1)
			{

				if (res.Id == review.IdReservation)
				{
					toAdd = false;
					break;
				}

			}

			return res.EndDate < today && today.DayNumber - res.EndDate.DayNumber <= 5 && toAdd;
		}
	}
}
