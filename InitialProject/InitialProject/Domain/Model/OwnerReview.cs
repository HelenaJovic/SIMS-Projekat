using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
	public class OwnerReview : ISerializable
    {
		public int Id { get; set; }

		public int OwnerCorrectness { get; set; }

		public int CleanlinessGrade { get; set; }

		public string Comment { get; set; }

		public int ReservationId { get; set; }

		public AccommodationReservation Reservation { get; set; }

		public OwnerReview()
		{

		}

		public OwnerReview(int ownerCorrectness, int cleanliness, string comment, int reservationId, AccommodationReservation reservation)
		{
			OwnerCorrectness = ownerCorrectness;
			CleanlinessGrade = cleanliness;
			Comment = comment;
			ReservationId = reservationId;
			Reservation = reservation;

		}

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                OwnerCorrectness.ToString(),
                CleanlinessGrade.ToString(),
                Comment,
                ReservationId.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            OwnerCorrectness= int.Parse(values[1]);
            CleanlinessGrade= int.Parse(values[2]);
            Comment = values[3];
            ReservationId = int.Parse(values[4]);

        }
    }
}
