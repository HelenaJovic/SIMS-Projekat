using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
	internal class GuestReviewService
	{
		private readonly GuestReviewRepository guestReviewReposiory;

		public GuestReviewService()
		{
			guestReviewReposiory = new GuestReviewRepository();
		}

		public List<GuestReview> GetAll()
		{
			List<GuestReview> reviews = new List<GuestReview>();
			reviews = guestReviewReposiory.GetAll();
			return reviews;
		}

		public GuestReview Save(GuestReview guestReview)
		{
			GuestReview savedGuestReview = guestReviewReposiory.Save(guestReview);
			return savedGuestReview;
		}
	}
}
