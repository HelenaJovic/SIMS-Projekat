using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class TourGuideReviewsService
    {
        TourGuideReviewRepository _tourGuideRepository;
        UserRepository _userRepository;
        TourPointService _tourPointService;
        public TourGuideReviewsService()
        {
            _tourGuideRepository= new TourGuideReviewRepository();
            _userRepository= new UserRepository();
            _tourPointService= new TourPointService();
        }

        public List<TourGuideReview> GetAllByUser(User user)
        {
            List<TourGuideReview> _reviews = _tourGuideRepository.GetAllByUser(user);
            foreach(TourGuideReview review in _reviews)
            {
                review.Guest = _userRepository.GetById(review.IdGuest);
                review.TourPoint = _tourPointService.GetById(review.IdTourPoint);
            }
            return _reviews;
        }
    }
}
