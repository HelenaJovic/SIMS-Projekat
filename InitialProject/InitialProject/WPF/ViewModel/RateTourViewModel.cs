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
        public TourAttendance SelectedAttendedTour { get; set; }
        public static User User { get; set; }
        private readonly TourGuideReviewRepository tourGuideReviewRepository;
        private readonly ImageRepository _imageRepository;
        private readonly TourAttendanceService _tourAttendenceService;
        private readonly IMessageBoxService _messageBoxService;
        public Action CloseAction { get; set; }
        public RateTourViewModel(User user, TourAttendance tourAttendance)
        {
            SelectedAttendedTour = tourAttendance;
            User = user;
            tourGuideReviewRepository = new TourGuideReviewRepository();
            _imageRepository = new ImageRepository();
            _tourAttendenceService = new TourAttendanceService();
            _messageBoxService = new MessageBoxService();
            InitializeCommands();

        }

        private void InitializeCommands()
        {
            SubmitCommand = new RelayCommand(Execute_SubmitCommand, CanExecute_Command);
            GoSelectTourCommand =  new RelayCommand(Execute_GoSelectTourCommand, CanExecute_Command);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_GoSelectTourCommand(object obj)
        {
            CloseAction();
        }

        private void Execute_SubmitCommand(object obj)
        {
            if (SelectedAttendedTour != null)
            {
                if (SelectedAttendedTour.Rated==false)
                {
                    AcceptedRatingTour();
                    CloseAction();
                }
                else
                {
                    _messageBoxService.ShowMessage("This attended tour was already rated, you can rate, you can rate some unrated ones");
                }
            }
            else
            {
                _messageBoxService.ShowMessage("You need to select attended tour you want to rate");
            }
        }

        private void AcceptedRatingTour()
        {
            SelectedAttendedTour.Rated = true;
            _tourAttendenceService.Update(SelectedAttendedTour);
            TourGuideReview tourGuideReview = new TourGuideReview(User.Id, SelectedAttendedTour.IdGuide, SelectedAttendedTour.IdTourPoint, int.Parse(GuideKnowledge), int.Parse(GuideLanguage), int.Parse(InterestingTour), Comment, SelectedAttendedTour.IdTour);
            TourGuideReview savedTourGuideRewiew = tourGuideReviewRepository.Save(tourGuideReview);
            _imageRepository.StoreImageTourGuideReview(savedTourGuideRewiew, ImageUrl);
        }

        private string _imageUrl;
        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                if (value != _imageUrl)
                {
                    _imageUrl = value;
                    OnPropertyChanged();
                }
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

        private RelayCommand goSelectTourCommand;
        public RelayCommand GoSelectTourCommand
        {
            get { return goSelectTourCommand; }
            set
            {
                goSelectTourCommand = value;
            }
        }

        private string _guideKnowledge;
        public string GuideKnowledge
        {
            get => _guideKnowledge;
            set
            {
                if (value != _guideKnowledge)
                {
                    _guideKnowledge = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _guideLanguage;
        public string GuideLanguage
        {
            get => _guideLanguage;
            set
            {
                if (value != _guideLanguage)
                {
                    _guideLanguage = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _interestingTour;
        public string InterestingTour
        {
            get => _interestingTour;
            set
            {
                if (value != _interestingTour)
                {
                    _interestingTour = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _comment;
        public string Comment
        {
            get => _comment;
            set
            {
                if (value != _comment)
                {
                    _comment = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _photos;
        public string Photos
        {
            get => _photos;
            set
            {
                if (value != _photos)
                {
                    _photos = value;
                    OnPropertyChanged();
                }
            }
        }
    }
}
