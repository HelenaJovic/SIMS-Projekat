using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class NotificationsGuest2ViewModel : ViewModelBase
    {
        public static User LoggedInUser { get; set; }


        private readonly NotificationService notificationService;

        public delegate void EventHandler();

        public event EventHandler CheckAcceptedTourRequests;
        public event EventHandler CheckCreatedTours;


        private Notifications _selectedNotification;
        public Notifications SelectedNotification
        {
            get { return _selectedNotification; }
            set
            {
                _selectedNotification = value;
                OnPropertyChanged(nameof(SelectedNotification));
            }
        }

        private ObservableCollection<Notifications> _notifications;
        public ObservableCollection<Notifications> Notifications
        {
            get { return _notifications; }
            set
            {
                _notifications = value;
                OnPropertyChanged(nameof(Notifications));
            }
        }



        private RelayCommand checkOut;
        public RelayCommand CheckOut
        {
            get { return checkOut; }
            set
            {
                checkOut = value;
            }
        }



        public NotificationsGuest2ViewModel(User user)
        {
            LoggedInUser = user;
            notificationService = new NotificationService();
            Notifications = new ObservableCollection<Notifications>(notificationService.NotifyGuest2(user));
            CheckOut = new RelayCommand(Execute_CheckOut, CanExecute);
        }

        private bool CanExecute(object parameter)
        {
            return true;
        }

        private void Execute_CheckOut(object sender)
        {
            var selectedNotification = SelectedNotification;
            if (selectedNotification != null)
            {
                selectedNotification.IsRead = true;
                notificationService.Update(selectedNotification);


                if (selectedNotification.NotifType == NotificationType.CheckAcceptedTourRequest)
                {
                    CheckAcceptedTourRequests?.Invoke();
                }

                if (selectedNotification.NotifType == NotificationType.CheckCreatedTour)
                {
                    CheckCreatedTours?.Invoke();
                }
            }
        }

    }
}
