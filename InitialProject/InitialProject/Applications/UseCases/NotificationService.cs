using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
	public class NotificationService
	{
		private readonly INotificationRepository _notificationRepository;

		private readonly AccommodationReservationService accommodationReservationService;

		private readonly ReservationDisplacementRequestService reservationDisplacementRequestService;

		public NotificationService()
		{
			_notificationRepository = Inject.CreateInstance<INotificationRepository>();
			accommodationReservationService = new AccommodationReservationService();
			reservationDisplacementRequestService = new ReservationDisplacementRequestService();
		}

		public Notifications GenerateNotificationAboutGuestRating(User user, AccommodationReservation reservation)
		{
			DateOnly today = DateOnly.FromDateTime(DateTime.Now);
			string title = "Guest rating notice";
			string content = $"Please note that you have not rated a guest named {reservation.Guest.Username}. The deadline for evaluation is {5-(today.DayNumber - reservation.EndDate.DayNumber)} days and you can do so by clicking on the button on the right.";


			Notifications existingNotification = _notificationRepository.GetByUserId(user.Id).FirstOrDefault(n => n.Content == content);

			if (existingNotification != null)
			{
				return null;
			}

			return new Notifications(user.Id, title, content, NotificationType.RateGuest, false, today);
		}


		private Notifications GenerateNotificationsAboutRequests(User user)
		{
			DateOnly today = DateOnly.FromDateTime(DateTime.Now);
			string title = "Notification of the request to move the reservation";
			string content = "There are still requests to move reservations that are pending. Click the button next to it and approve or deny the request";

			Notifications existingNotification = _notificationRepository.GetByUserId(user.Id).FirstOrDefault(n => n.Content == content);

			if(existingNotification != null)
			{
				return null;
			}

			return new Notifications(user.Id, title, content, NotificationType.CheckRequests, false,today);
		}



		public List<Notifications> NotifyOwner1(User user)
		{
			List<AccommodationReservation> reservations = accommodationReservationService.GetFilteredReservations(user);

			List<Notifications> myNotifications = _notificationRepository.GetUnreadedAndTodaysNotifications(user.Id);

			DateOnly today = DateOnly.FromDateTime(DateTime.Now);

			

			foreach (AccommodationReservation res in reservations)
			{

                Notifications notif = GenerateNotificationAboutGuestRating(user,res);
				if(notif != null)
				{
					Notifications savedNotif = _notificationRepository.Save(notif);
					myNotifications.Add(savedNotif);
				}
				  
				
			}


			return myNotifications;
		}

		public List<Notifications> NotifyOwner2(User user)
		{
			List<Notifications> notifications = _notificationRepository.GetNotificationsAboutRequests(user.Id);

			List<ReservationDisplacementRequest> requests = reservationDisplacementRequestService.GetByOwnerId(user.Id);

			List<ReservationDisplacementRequest> onHoldRequests = requests.FindAll(r => r.Type == RequestType.OnHold);

			if(onHoldRequests.Count > 0)
			{
				Notifications notif = GenerateNotificationsAboutRequests(user);
				if(notif != null)
				{
					Notifications savedNotif = _notificationRepository.Save(notif);
					notifications.Add(savedNotif);
				}
			}

			return notifications;
		}

		public List<Notifications> NotifyOwner(User user)
		{
			var notifications1 = NotifyOwner1(user);
			var notifications2 = NotifyOwner2(user);
			var allNotifications = notifications1.Concat(notifications2).ToList();
			return allNotifications;
		}



		public List<Notifications> GetByUserId(int id)
		{
			return _notificationRepository.GetByUserId(id);
		}

		public List<Notifications> GetAll()
		{
			return _notificationRepository.GetAll();
		}

		public Notifications Update(Notifications notification)
		{
			return _notificationRepository.Update(notification);
			
		}
	}
}
