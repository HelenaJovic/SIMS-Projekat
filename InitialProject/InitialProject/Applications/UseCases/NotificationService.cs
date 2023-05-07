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

		public NotificationService()
		{
			_notificationRepository = Inject.CreateInstance<INotificationRepository>();
			accommodationReservationService = new AccommodationReservationService();
		}

		public Notifications GenerateNotificationAboutGuestRating(User user, AccommodationReservation reservation)
		{
			DateOnly today = DateOnly.FromDateTime(DateTime.Now);
			string title = "Guest rating notice";
			string content = $"Please note that you have not rated a guest named {reservation.Guest.Username}. The deadline for evaluation is {5-(today.DayNumber - reservation.EndDate.DayNumber)} days and you can do so by clicking on the button on the right.";

			return new Notifications(user.Id, title, content, false);
		}

		public List<Notifications> NotifyOwner(User user)
		{
			List<AccommodationReservation> reservations = GetFilteredReservations(user);
			
			List<Notifications> myNotifications = _notificationRepository.GetByUserId(user.Id);

			DateOnly today = DateOnly.FromDateTime(DateTime.Now);


			foreach (AccommodationReservation res in reservations)
			{
				Notifications existingNotification = myNotifications.FirstOrDefault(n => n.Content == $"Please note that you have not rated a guest named {res.Guest.Username}. The deadline for evaluation is {5-(today.DayNumber - res.EndDate.DayNumber)} days and you can do so by clicking on the button on the right.");

				if (existingNotification == null)
				{
                   Notifications notif = GenerateNotificationAboutGuestRating(user,res);
				   Notifications savedNotif = _notificationRepository.Save(notif);
				   myNotifications.Add(notif);
				}
			}

			return myNotifications;
		}

		private List<AccommodationReservation> GetFilteredReservations(User user)
		{
			List<AccommodationReservation> reservations = accommodationReservationService.GetByOwnerId(user.Id);
			if(reservations.Count > 0)
			{
				accommodationReservationService.BindData(reservations);
			}
			List<AccommodationReservation> filteredReservations = new List<AccommodationReservation>();

			DateOnly today = DateOnly.FromDateTime(DateTime.Now);

			foreach (AccommodationReservation res in reservations)
			{
				if (accommodationReservationService.IsElegibleForReview(today, res) == false) continue;
				filteredReservations.Add(res);

			}

			return filteredReservations;
		}

		public List<Notifications> GetByUserId(int id)
		{
			return _notificationRepository.GetByUserId(id);
		}
	}
}
