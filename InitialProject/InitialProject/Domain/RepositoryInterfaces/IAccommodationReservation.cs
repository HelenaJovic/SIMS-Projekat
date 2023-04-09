using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
	public interface IAccommodationReservation : IRepository<AccommodationReservation>
	{
		public List<AccommodationReservation> GetByOwnerId(int id);
		public List<AccommodationReservation> GetByAccommodationId(int id);

		public List<AccommodationReservation> GetByUser(User user);

		public List<DateOnly> GetAllEndDates(int id);

		public string GetNameById(int id);

		public List<DateOnly> GetAllStartDates(int id);







	}
}
