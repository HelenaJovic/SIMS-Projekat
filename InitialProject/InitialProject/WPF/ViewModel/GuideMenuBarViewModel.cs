using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class GuideMenuBarViewModel : ViewModelBase
    {
        private RelayCommand main_page;
        public RelayCommand MainPageCommand
        {
            get => main_page;
            set
            {
                if (value != main_page)
                {
                    main_page = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand upcoming_tours;
        public RelayCommand UpcomingToursCommand
        {
            get => upcoming_tours;
            set
            {
                if (value != upcoming_tours)
                {
                    upcoming_tours = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand create;
        public RelayCommand CreateTourCommand
        {
            get => create;
            set
            {
                if (value != create)
                {
                    create = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand tourTrack;
        public RelayCommand TourTrackingCommand
        {
            get => tourTrack;
            set
            {
                if (value != tourTrack)
                {
                    tourTrack = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand mostVisited;
        public RelayCommand MostVisitedCommand
        {
            get => mostVisited;
            set
            {
                if (value != mostVisited)
                {
                    mostVisited = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand finishedTours;
        public RelayCommand FinishedToursCommand
        {
            get => finishedTours;
            set
            {
                if (value != finishedTours)
                {
                    finishedTours = value;
                    OnPropertyChanged();
                }

            }
        }

        public User LoggedInUser;
        public Action CloseAction;

        public GuideMenuBarViewModel(User user)
        {
            MainPageCommand = new RelayCommand(Execute_MainPage, CanExecute_Command);
            UpcomingToursCommand = new RelayCommand(Execute_UpComingTours, CanExecute_Command);
            CreateTourCommand = new RelayCommand(Execute_CreateTour, CanExecute_Command);
            TourTrackingCommand = new RelayCommand(Execute_TourTracking, CanExecute_Command); 
            MostVisitedCommand = new RelayCommand(Execute_MostVisited, CanExecute_Command);
            FinishedToursCommand = new RelayCommand(Execute_FinishedTours, CanExecute_Command);
            LoggedInUser = user;
        }

        private void Execute_FinishedTours(object obj)
        {
            FinishedTours finishedTours = new FinishedTours(LoggedInUser);
            finishedTours.Show();
        }

        private void Execute_MostVisited(object obj)
        {
            TheMostVisitedTour mostVisited = new TheMostVisitedTour(LoggedInUser);
            mostVisited.Show();
            //CloseAction();
        }

        private void Execute_TourTracking(object obj)
        {
            TourTracking tourTracking = new TourTracking(LoggedInUser);
            tourTracking.Show();
            //CloseAction();
        }

        private void Execute_CreateTour(object obj)
        {
            CreateTour createTour = new CreateTour(LoggedInUser);
            createTour.Show();
            //CloseAction();
        }

        private void Execute_UpComingTours(object obj)
        {
            GuideMainWindow guideMain = new GuideMainWindow(LoggedInUser);
            guideMain.Show();
            //CloseAction();
        }

        private void Execute_MainPage(object obj)
        {
            GuideProfile guideProfile = new GuideProfile(LoggedInUser);
            guideProfile.Show();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
