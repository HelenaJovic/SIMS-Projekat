using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class Guest1NotificationsViewModel: ViewModelBase
    {
		public static User LoggedInUser { get; set; }


		private readonly NotificationService notificationService;

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

		public Guest1NotificationsViewModel(User owner)
		{
			LoggedInUser = owner;
			notificationService = new NotificationService();
			_notifications = new ObservableCollection<Notifications>(notificationService.NotifyOwner(owner));
		}
	}
}
