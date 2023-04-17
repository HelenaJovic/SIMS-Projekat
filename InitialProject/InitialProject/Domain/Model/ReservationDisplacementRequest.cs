using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
    public class ReservationDisplacementRequest : ISerializable
    {
        public int Id { get; set; }
        public AccommodationReservation Reservation { get; set; }

        public int IdUser { get; set; }

        public int ReservationId { get; set; }

        public RequestType Type { get; set; }

        public DateOnly NewStartDate { get; set; }

        public DateOnly NewEndDate { get; set; }

        public String Comment { get; set; }

        public bool IsChecked { get; set; }

        public ReservationDisplacementRequest(AccommodationReservation reservation, int reservationId, RequestType type, DateOnly newStartDate, DateOnly newEndDate,int idGuest, string? Comment = null)
        {
         
            Reservation = reservation;
            ReservationId = reservationId;
            this.Type = type;
            NewStartDate = newStartDate;
            NewEndDate = newEndDate;
            Type = RequestType.OnHold;
            IdUser= idGuest;
          
            this.Comment = Comment ?? " ";
            
        }

        public ReservationDisplacementRequest()
        {
            Type = RequestType.OnHold;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                ReservationId.ToString(),
                Type.ToString(),
                NewStartDate.ToString(),
                NewEndDate.ToString(),
                IdUser.ToString(),
                Comment
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            ReservationId = int.Parse(values[1]);
            Type = (RequestType)Enum.Parse(typeof(RequestType), values[2]);
            NewStartDate = DateOnly.Parse(values[3]);
            NewEndDate = DateOnly.Parse(values[4]);
            IdUser= int.Parse(values[5]);
            Comment = values[6];

        }


    }
}