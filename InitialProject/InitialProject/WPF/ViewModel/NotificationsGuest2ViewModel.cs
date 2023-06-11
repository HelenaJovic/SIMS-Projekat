using GalaSoft.MvvmLight.Command;
using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.WPF.View;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using User = InitialProject.Domain.Model.User;

namespace InitialProject.WPF.ViewModel
{
    public class NotificationsGuest2ViewModel : ViewModelBase
    {
        public static User LoggedInUser { get; set; }
        private readonly NotificationService notificationService;
        private readonly VoucherService voucherService;
        public delegate void EventHandler1(User user, TourRequest tourRequest);
        public event EventHandler1 CheckAcceptedTourRequests;
        public delegate void EventHandler2(User user, Tour tour);
        public event EventHandler2 CheckCreatedTours;
        public delegate void EventHandler3();
        public event EventHandler3 CheckVouchers;

        public RelayCommand<Notifications> NotificationSelectedCommand { get; private set; }

        private Notifications _selectedNotification;
        public Notifications SelectedNotification
        {
            get { return _selectedNotification; }
            set
            {
                _selectedNotification = value;
                OnPropertyChanged(nameof(SelectedNotification));
                HandleNotificationSelected(value);
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

        public NotificationsGuest2ViewModel(User user)
        {
            LoggedInUser = user;
            notificationService = new NotificationService();
            voucherService = new VoucherService();
            Notifications = new ObservableCollection<Notifications>(notificationService.NotifyGuest2(user));
            NotificationSelectedCommand = new RelayCommand<Notifications>(OnNotificationSelected);
        }

        private void OnNotificationSelected(Notifications selectedNotification)
        {
            SelectedNotification = selectedNotification;
        }

        private void HandleNotificationSelected(Notifications selectedNotification)
        {
            if (selectedNotification != null)
            {
                selectedNotification.IsRead = true;
                notificationService.Update(selectedNotification);

                if (selectedNotification.NotifType == NotificationType.CheckAcceptedTourRequest)
                {
                    TourRequest approvedTours = notificationService.GetTourRequestByNotification(selectedNotification);

                    CheckAcceptedTourRequests?.Invoke(LoggedInUser, approvedTours);
                }

                else if (selectedNotification.NotifType == NotificationType.CheckCreatedTour)
                {
                    Tour createdTour = notificationService.GetTourByNotification(selectedNotification);

                    CheckCreatedTours?.Invoke(LoggedInUser, createdTour);
                }
                else if(selectedNotification.NotifType == NotificationType.VoucherWon)
                {
                    DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                    DateOnly futureDate = today.AddMonths(6);
                    Voucher voucher = new Voucher(LoggedInUser.Id, "Won voucher", futureDate);

                    Voucher savedVoucher = voucherService.Save(voucher);
                    TourVouchersViewModel.VouchersMainList.Add(savedVoucher);
                    CheckVouchers?.Invoke();
                }
            }
        }

    }
}
