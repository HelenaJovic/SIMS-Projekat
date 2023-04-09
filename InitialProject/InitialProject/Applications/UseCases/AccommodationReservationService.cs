using InitialProject.Domain.Model;
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
		private readonly AccommodationReservationRepository accommodationReservationRepository;

		private readonly GuestReviewService guestReviewService;

		private readonly AccommodationService accommodationService;

		private readonly UserRepository userRepository;

		List<GuestReview> guestReviews;

		List<AccommodationReservation> reservations;

		DateOnly today;
		public AccommodationReservationService()
		{
			accommodationService = new AccommodationService();
			accommodationReservationRepository = new AccommodationReservationRepository();
			guestReviewService = new GuestReviewService();
			userRepository = new UserRepository();
			guestReviews = guestReviewService.GetAll();
		    today = DateOnly.FromDateTime(DateTime.Now);
			reservations = accommodationReservationRepository.GetAll();
			BindData();
			
		}


		public void BindData()
		{
			
			foreach (AccommodationReservation res in reservations)
			{
				res.Guest = userRepository.GetById(res.IdGuest);
				res.Accommodation = accommodationService.GetById(res.IdAccommodation);
			}

			
		}

	    public List<AccommodationReservation> GetAll()
		{
			List<AccommodationReservation> reservations = new List<AccommodationReservation>();
			reservations=accommodationReservationRepository.GetAll();
			return reservations;
		}

		public bool IsElegibleForReview(DateOnly today, AccommodationReservation res)
		{
			

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
			return accommodationReservationRepository.GetById(id);
		}
	}
}
