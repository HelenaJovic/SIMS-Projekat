using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class GuideRatingsViewModel : ViewModelBase
    {
        public User LoggedInUser {get;set;}
        public ObservableCollection<TourGuideReview> GuideReviews { get;set;}

        private readonly TourGuideReviewsService _tourGuideService;
        private readonly UserRepository _userRepository;
        private readonly TourPointService _tourPointService;

        public GuideRatingsViewModel(User user) 
        {
            LoggedInUser = user;
            _tourGuideService = new TourGuideReviewsService();
            _userRepository = new UserRepository();
            _tourPointService= new TourPointService();
            GuideReviews = new ObservableCollection<TourGuideReview>(_tourGuideService.GetAllByUser(LoggedInUser));
        }
        /*
        private bool _isCityEnabled;
        public bool IsCityEnabled
        {
            get { return _isCityEnabled; }
            set
            {
                _isCityEnabled = value;
                OnPropertyChanged(nameof(IsCityEnabled));
            }
        }*/
    }
}
