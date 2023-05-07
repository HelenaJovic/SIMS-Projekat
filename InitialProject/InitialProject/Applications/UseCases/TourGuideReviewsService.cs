using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
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
        private readonly ITourGuideReviewRepository _tourGuideRepository;
        private readonly UserService _userService;
        private readonly TourPointService _tourPointService;
        public TourGuideReviewsService()
        {
            _tourGuideRepository= Inject.CreateInstance<ITourGuideReviewRepository>();
            _userService = new UserService();
            _tourPointService= new TourPointService();
        }

        public List<TourGuideReview> GetAllByUser(User user)
        {
            List<TourGuideReview> _reviews = _tourGuideRepository.GetAllByUser(user);
            foreach(TourGuideReview review in _reviews)
            {
                review.Guest = _userService.GetById(review.IdGuest);
                review.TourPoint = _tourPointService.GetById(review.IdTourPoint);
            }
            return _reviews;
        }

        public TourGuideReview Update(TourGuideReview review)
        {
            return _tourGuideRepository.Update(review);
        }

        public double GetAvarageGrade(User user)
        {
            double sum = 0;
            double n = 0;

            foreach (TourGuideReview review in _tourGuideRepository.GetAllByUser(user))
            {
                sum += review.GuideLanguage;
                sum += review.GuideKnowledge;
                n += 2;
            }
            if(n == 0)
            {
                return 0;
            }
            return sum / n;
        }
    }
}
