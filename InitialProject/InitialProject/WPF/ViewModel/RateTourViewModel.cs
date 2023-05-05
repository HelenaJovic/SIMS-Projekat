using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Linq;

namespace InitialProject.WPF.ViewModel
{
    public class RateTourViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        public TourAttendance SelectedAttendedTour { get; set; }
        public static User User { get; set; }
        private readonly TourGuideReviewRepository tourGuideReviewRepository;
        private readonly ImageRepository _imageRepository;
        private readonly TourAttendanceService _tourAttendenceService;
        public TourGuideReview tourGuideReview = new TourGuideReview();
        
        public RateTourViewModel(User user, TourAttendance tourAttendance)
        {
            SelectedAttendedTour = tourAttendance;
            User = user;
            tourGuideReviewRepository = new TourGuideReviewRepository();
            _imageRepository = new ImageRepository();
            _tourAttendenceService = new TourAttendanceService();
            InitializeCommands();
        }

        public TourGuideReview TourGuideReviews
        {
            get { return tourGuideReview; }
            set
            {
                tourGuideReview = value;
                OnPropertyChanged("TourGuideReviews");
            }
        }

        private void InitializeCommands()
        {
            SubmitCommand = new RelayCommand(Execute_SubmitCommand, CanExecute_Command);
            GiveUpRatingCommand =  new RelayCommand(Execute_GiveUpRatingCommand, CanExecute_Command);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_GiveUpRatingCommand(object obj)
        {
            CloseAction();
        }

        private void Execute_SubmitCommand(object obj)
        {

            TourGuideReviews.Validate();

            if (TourGuideReviews.IsValid)
            {
                // Create a new OwnerReview object with the validated values and save it

                SelectedAttendedTour.Rated = true;
                _tourAttendenceService.Update(SelectedAttendedTour);
                TourGuideReview newTourGuideReview = new TourGuideReview(User.Id, SelectedAttendedTour.IdGuide, SelectedAttendedTour.IdTourPoint, TourGuideReviews.GuideKnowledge, TourGuideReviews.GuideLanguage, TourGuideReviews.InterestingTour, TourGuideReviews.Comment, SelectedAttendedTour.IdTour);
                TourGuideReview savedTourGuideRewiew = tourGuideReviewRepository.Save(newTourGuideReview);


                _imageRepository.StoreImageTourGuideReview(savedTourGuideRewiew, TourGuideReviews.ImageUrl);

                CloseAction();
            }
            else
            {
                // Update the view with the validation errors
                OnPropertyChanged(nameof(TourGuideReviews));
            }
        }
        
       

        private RelayCommand submitCommand;
        public RelayCommand SubmitCommand
        {
            get { return submitCommand; }
            set
            {
                submitCommand = value;
            }
        }


        private RelayCommand giveUpRatingCommand;
        public RelayCommand GiveUpRatingCommand
        {
            get { return giveUpRatingCommand; }
            set
            {
                giveUpRatingCommand = value;
            }
        }

    }
}
