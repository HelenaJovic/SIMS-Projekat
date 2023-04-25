using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModel
{
    public class GuideRatingsViewModel : ViewModelBase
    {
        public User LoggedInUser {get;set;}
        public ObservableCollection<TourGuideReview> GuideReviews { get;set;}

        private readonly TourGuideReviewsService _tourGuideService;
        public GuideRatingsViewModel(User user)
        {
            LoggedInUser = user;
            _tourGuideService = new TourGuideReviewsService();
            GuideReviews = new ObservableCollection<TourGuideReview>(_tourGuideService.GetAllByUser(LoggedInUser));
        }
        
        public void Changed()
        {
            foreach (TourGuideReview review in _tourGuideService.GetAllByUser(LoggedInUser))
            {
                _tourGuideService.Update(review);
            }
        }

       


    }
}
