using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.WPF.ViewModel
{
    public class GuideRatingsViewModel : ViewModelBase
    {
        public User LoggedInUser {get;set;}
        public TourGuideReview SelectedRating { get;set;}
        public ObservableCollection<TourGuideReview> GuideReviews { get;set;}

        private readonly TourGuideReviewsService _tourGuideService;
        private readonly MessageBoxService _messageBoxService;

        private RelayCommand report;
        public RelayCommand ReportCommand
        {
            get => report;
            set
            {
                if (value != report)
                {
                    report = value;
                    OnPropertyChanged(nameof(ReportCommand));
                }

            }
        }


        public GuideRatingsViewModel(User user)
        {
            LoggedInUser = user;
            _tourGuideService = new TourGuideReviewsService();
            _messageBoxService= new MessageBoxService();
            GuideReviews = new ObservableCollection<TourGuideReview>(_tourGuideService.GetAllByUser(LoggedInUser));
            ReportCommand = new RelayCommand(Execute_Report, CanExecute_Command);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_Report(object obj)
        {
            if (SelectedRating != null)
            {
                if (SelectedRating.IsValid == true)
                {
                    MakeInvalid();
                }
                else
                {
                    _messageBoxService.ShowMessage("You have already reported this review!");
                }
            }
            else
            {
                _messageBoxService.ShowMessage("Select a review you want to report!");
            }
        }

        private void MakeInvalid()
        {
            SelectedRating.IsValid = false;
            _tourGuideService.Update(SelectedRating);
            GuideReviews.Clear();
            foreach(TourGuideReview review in _tourGuideService.GetAllByUser(LoggedInUser))
            {
                GuideReviews.Add(review);
            }
            _messageBoxService.ShowMessage("Successfully reported!");
        }

    }
}
