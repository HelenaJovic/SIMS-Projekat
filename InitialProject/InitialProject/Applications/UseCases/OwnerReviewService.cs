using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
	internal class OwnerReviewService
	{
		private readonly OwnerReviewRepository ownerReviewRepository;

		private readonly GuestReviewService guestReviewService;

		private readonly AccommodationReservationService accommodationReservationService;

		List<OwnerReview> ownerReviews;

		

		public OwnerReviewService()
		{
		
			accommodationReservationService = new AccommodationReservationService();
			ownerReviewRepository = new OwnerReviewRepository();
			guestReviewService=new GuestReviewService();
			ownerReviews = ownerReviewRepository.GetAll();
			
			BindData();
		}

		

		public bool IsElegibleForDisplay(OwnerReview ownerReview)
		{
			List<GuestReview> guestReviews = guestReviewService.GetAll();

			bool toAdd=false;
			foreach (GuestReview guestReview in guestReviews)
			{
				if(guestReview.IdReservation == ownerReview.ReservationId)
				{
					toAdd=true;
				}
			}
			
			return toAdd;
		}

		private void BindData()
		{
			

			foreach (OwnerReview ownerReview in ownerReviews)
			{
				ownerReview.Reservation = accommodationReservationService.GetById(ownerReview.ReservationId);
				
			}

			
		}

		public List<OwnerReview> GetReviewsByOwnerId(int id)
		{
			List<OwnerReview> reviews = new List<OwnerReview>();

			foreach (OwnerReview ownerReview in ownerReviews)
			{
				if (ownerReview.Reservation.Accommodation.IdUser == id)
				{
					reviews.Add(ownerReview);
				}
			}
			return reviews;
		}

		public List<OwnerReview> GetAll()
		{
			return ownerReviewRepository.GetAll();
		}


	}
}
