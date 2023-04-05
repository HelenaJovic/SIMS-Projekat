using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
    public class AccommodationReservation : ISerializable
    {

        public int Id { get; set; }

        public int IdGuest { get; set; }

        public User Guest { get; set; }

        public int IdAccommodation { get; set; }
        public Accommodation Accommodation { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        public int DaysNum { get; set; }



        public AccommodationReservation()
        {

        }


        public AccommodationReservation(User guest, int idGuest, Accommodation accommodation, int idAccommodation, DateOnly startDate, DateOnly endDate, int daysNum)
        {
            Guest = guest;
            IdGuest = idGuest;
            Accommodation = accommodation;
            IdAccommodation = idAccommodation;
            StartDate = startDate;
            EndDate = endDate;
            DaysNum = daysNum;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IdGuest.ToString(),
                IdAccommodation.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                DaysNum.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            IdGuest = int.Parse(values[1]);
            IdAccommodation = int.Parse(values[2]);
            StartDate = DateOnly.Parse(values[3]);
            EndDate = DateOnly.Parse(values[4]);
            DaysNum = int.Parse(values[5]);

        }
    }
}
