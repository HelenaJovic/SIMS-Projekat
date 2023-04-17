using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModel
{
   
    public class GuideMainWindowViewModel :ViewModelBase
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        public Tour SelectedTour { get; set; }
        public User LoggedInUser { get; set; }
        private readonly TourService _tourService;

        public GuideMainWindowViewModel(User user)
        {
            LoggedInUser = user;
            _tourService = new TourService();
            Tours = new ObservableCollection<Tour>(_tourService.GetUpcomingToursByUser(LoggedInUser));

            CreateCommand = new RelayCommand(Execute_Create, CanExecute_Command);
            TourTrackingCommand = new RelayCommand(Execute_TourTracking, CanExecute_Command);
            MultiplyCommand = new RelayCommand(Execute_Multiply, CanExecute_Command);
            ViewGalleryCommand = new RelayCommand(Execute_ViewGallery, CanExecute_Command);
            CancelCommand = new RelayCommand(Execute_Cancel, CanExecute_Command);
        }

        private RelayCommand create;
        public RelayCommand CreateCommand
        {
            get { return create;  }
            set
            {
                create = value;
            }
        }

        private RelayCommand tracking;
        public RelayCommand TourTrackingCommand
        {
            get { return tracking; }
            set
            {
                tracking = value;
            }
        }

        private RelayCommand multiply;
        public RelayCommand MultiplyCommand
        {
            get { return multiply; }
            set
            {
                multiply = value;
            }
        }

        private RelayCommand viewGallery;
        public RelayCommand ViewGalleryCommand
        {
            get { return viewGallery; }
            set
            {
                viewGallery = value;
            }
        }


        private RelayCommand cancel;
        public RelayCommand CancelCommand
        {
            get { return cancel; }
            set
            {
                cancel = value;
            }
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_Create(object obj)
        {
            CreateTour createTour = new CreateTour(LoggedInUser);
            createTour.Show();
        }

        private void Execute_TourTracking(object obj)
        {
            TourTracking tourTracking = new TourTracking(LoggedInUser);
            tourTracking.Show();
        }

        private void Execute_Multiply(object obj)
        {
            if (SelectedTour != null)
            {
                AddDate addDate = new AddDate(SelectedTour);
                addDate.Show();
            }
            else
                MessageBox.Show("Choose a tour which you want to multiply");

        }

        private void Execute_ViewGallery(object obj)
        {
            ViewTourGalleryGuide viewTourGallery = new ViewTourGalleryGuide(SelectedTour);
            viewTourGallery.Show();
        }

        private void Execute_Cancel(object obj)
        {
            if (IsCancellationPossible(SelectedTour))
            {
                _tourService.CancelTour(SelectedTour);
                Tours.Remove(SelectedTour);
            }
            else
            {
                MessageBox.Show("You can't cancel tour less then 48h before");
            }
        }

        private bool IsCancellationPossible(Tour tour)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
            DateOnly futureDate = today.AddDays(2);

            if (tour.Date.CompareTo(futureDate) > 0)
            {
                return true;
            }
            else if (tour.Date.CompareTo(futureDate) == 0 && tour.StartTime > currentTime)
            {
                return true;
            }

            return false;
        }

    }
}
