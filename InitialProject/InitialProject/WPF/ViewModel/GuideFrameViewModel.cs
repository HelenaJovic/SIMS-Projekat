using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.WPF.ViewModel
{
    class GuideFrameViewModel : ViewModelBase
    {
        private RelayCommand menu;
        public RelayCommand MenuBarCommand
       {
            get => menu;
            set
            {
                if (value != menu)
                {
                    menu = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand back;
        public RelayCommand BackCommand
        {
            get => back;
            set
            {
                if (value != back)
                {
                    back = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand home;
        public RelayCommand HomeCommand
        {
            get => home;
            set
            {
                if (value != home)
                {
                    home = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand demo;
        public RelayCommand DemoCommand
        {
            get => demo;
            set
            {
                if (value != demo)
                {
                    demo = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand user;
        public RelayCommand UserCommand
        {
            get => user;
            set
            {
                if (value != user)
                {
                    user = value;
                    OnPropertyChanged();
                }

            }
        }

        public User LoggedInUser { get; set; }
        private Page frame;
        public  Page FrameContent 
        {
              get { return frame; }
              set
              {
                  if (frame != value)
                  {
                    frame = value;
                    OnPropertyChanged(nameof(FrameContent));
                  }
              }
          }


        public TourService tourService;

        public GuideFrameViewModel(User user)
        {
            LoggedInUser= user;
            GuideHomePageViewModel profileVm = new GuideHomePageViewModel(LoggedInUser);
            FrameContent = new GuideHomePage(LoggedInUser, profileVm);
            tourService = new TourService();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            MenuBarCommand = new RelayCommand(Execute_MenuBar, CanExecute_Command);
            BackCommand = new RelayCommand(Execute_Back, CanExecute_Command);
            HomeCommand = new RelayCommand(Execute_Home, CanExecute_Command);
            UserCommand = new RelayCommand(Execute_User, CanExecute_Command);
            DemoCommand = new RelayCommand(Execute_Demo, CanExecute_Command);
        }

        private void Execute_User(object obj)
        {
            GuideProfileViewModel profileVm = new GuideProfileViewModel(LoggedInUser);
            FrameContent = new GuideProfile(LoggedInUser, profileVm);

            profileVm.RatingsEvent += OnRatings;
        }

        private void OnRatings()
        {
            FrameContent = new GuideRatings(LoggedInUser);
        }

        private void Execute_Demo(object obj)
        {
            //
        }

        private void Execute_Home(object obj)
        {
            GuideHomePageViewModel homeVm = new GuideHomePageViewModel(LoggedInUser);
            FrameContent = new GuideHomePage(LoggedInUser, homeVm);
            
        }

        private void Execute_Back(object obj)
        {
          //  GuideHomePageViewModel profileVm = new GuideHomePageViewModel(LoggedInUser);
          // FrameContent = new GuideHomePage(LoggedInUser, profileVm);
        }

        private void Execute_MenuBar(object obj)
        {
            var guideMenuBarVm = new GuideMenuBarViewModel(LoggedInUser);
            FrameContent = new GuideMenuBar(LoggedInUser, guideMenuBarVm);
           
            guideMenuBarVm.CreateTourEvent += OnCreate;
            guideMenuBarVm.TourTrackingEvent += OnTourTracking;
            guideMenuBarVm.UpcomingToursEvent += OnUpcomingTours;
            guideMenuBarVm.FinishedToursEvent += OnFinishedTours;
            guideMenuBarVm.MainPageEvent += OnMainPage;
            guideMenuBarVm.MostVisitedEvent += OnMostVisited;
        }

        private void OnCreate()
        {
            FrameContent = new CreateTour(LoggedInUser);
        }

        private void OnTourTracking()
        {
            TourTrackingViewModel tourTrackingVm = new TourTrackingViewModel(LoggedInUser);
            FrameContent = new TourTracking(LoggedInUser, tourTrackingVm);
            tourTrackingVm.TourPointsEvent += OnTourPoints;
        }

        private void OnTourPoints(Tour tour)
        {
            TourPointsViewModel tourPointsVm = new TourPointsViewModel(tour);
            FrameContent = new TourPoints(tour, tourPointsVm);

            tourPointsVm.GuestsEvent += OnGuests;
        }

        private void OnGuests(Tour tour, TourPoint point)
        {
            FrameContent = new TourGuests(tour,point);
        }

        private void OnUpcomingTours()
        {
            GuideMainWindowViewModel upcomingVm= new GuideMainWindowViewModel(LoggedInUser);
            FrameContent = new GuideMainWindow(LoggedInUser, upcomingVm);

            upcomingVm.MultiplyEvent += OnMultiply;
            upcomingVm.ViewGalleryEvent += OnViewGallery;
        }

        private void OnViewGallery(Tour tour)
        {
            FrameContent = new ViewTourGalleryGuide(tour);
        }

        private void OnMultiply(Tour tour)
        {
            FrameContent = new AddDate(tour);
        }

        private void OnFinishedTours()
        {
            FinishedToursViewModel finishedVm = new FinishedToursViewModel(LoggedInUser);
            FrameContent= new FinishedTours(LoggedInUser, finishedVm);

            finishedVm.StatisticsEvent += OnStatistics;
        }

        private void OnStatistics(Tour tour)
        {
            FrameContent = new TourStatistics(tour);
        }

        private void OnMainPage()
        {
            GuideHomePageViewModel profileVm = new GuideHomePageViewModel(LoggedInUser);
            FrameContent = new GuideHomePage(LoggedInUser, profileVm);

        }

        private void OnMostVisited()
        {
            FrameContent = new TheMostVisitedTour(LoggedInUser);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
