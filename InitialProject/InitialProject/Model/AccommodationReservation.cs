using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class AccommodationReservation:ISerializable
    {
       public int Id { get; set; }
       public int IdGuest { get; set; }
        
       public DateTime StartDate { get; set; }
       public DateTime EndDate { get; set; }
       
       public int DaysNum { get; set; }

        public AccommodationReservation()
        {

        }

        public AccommodationReservation(int id, int idguest, DateTime startDate, DateTime endDate,int daysNum)
        {
            Id = id;
            IdGuest = idguest;
            this.StartDate = startDate;
            this.EndDate = endDate;
            this.DaysNum = daysNum;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IdGuest.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                DaysNum.ToString()
                
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            IdGuest=int.Parse(values[1]);
            StartDate = DateTime.Parse(values[2]);
            EndDate = DateTime.Parse(values[3]);
            DaysNum = int.Parse(values[4]);

        }
    }
}
