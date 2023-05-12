using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace InitialProject.WPF.ViewModel
{
    public class GuideHomePageViewModel : ViewModelBase
    {
        public User LoggedInUser { get; set; }
        public Tour TopTour { get; set; }
        public int NumOfUpcomingTours { get; set; }
        public string AvarageGrade { get; set; }


        private RelayCommand logOut;
        public RelayCommand LogOutCommand
        {
            get => logOut;
            set
            {
                if (value != logOut)
                {
                    logOut = value;
                    OnPropertyChanged();
                }
            }
        }

        public delegate void EventHandler1();

        public event EventHandler1 RatingsEvent;

        private readonly TourService _tourService;
        private readonly TourGuideReviewsService _tourGuideReviewService;

        public GuideHomePageViewModel(User user)
         {
            LoggedInUser = user;
            _tourService = new TourService();
            _tourGuideReviewService = new TourGuideReviewsService();
            LogOutCommand = new RelayCommand(Execute_LogOut, CanExecute_Command);
            InitializeProperties();
        }

        private void InitializeProperties()
        {
            TopTour = _tourService.GetTopTour(LoggedInUser);
            NumOfUpcomingTours = _tourService.GetNumOfUpcomingTours(LoggedInUser);
            AvarageGrade = _tourGuideReviewService.GetAvarageGrade(LoggedInUser).ToString("F2");
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_LogOut(object obj)
        {
            SignInForm signInForm = new SignInForm();
            signInForm.Show(); 

            foreach (Window window in Application.Current.Windows)
            {
                if (window is GuideFrame)
                {
                    window.Close();
                }
            }
        }

    }
}
