using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
    public class ReservationDisplacementRequest:ISerializable
    {
        public int Id { get; set; } 
        public AccommodationReservation reservation { get; set; }

        public int ReservationId { get; set; }
        public int IdUser { get; set; }

        public RequestType Type { get; set; }

        public ReservationDisplacementRequest(int id, AccommodationReservation reservation, int reservationId, int idUser, RequestType type)
        {
            Id = id;
            this.reservation = reservation;
            ReservationId = reservationId;
            IdUser = idUser;
            this.Type = type;
        }

        public ReservationDisplacementRequest()
        {
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                IdUser.ToString(),
                Type.ToString()
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            IdUser = int.Parse(values[2]);
            Type = (RequestType)Enum.Parse(typeof(RequestType), values[3]);

        }


    }
}
