using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Domain.Model
{
    public class TourRequest : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public int IdLocation { get; set; }
        public string Descripiton { get; set; }
        public string Language { get; set; }
        public int MaxGuestNum { get; set; }
        public RequestType Status { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public List<Image> Images { get; set; }

        public TourRequest ()
        {
            Images = new List<Image>();
        }

        public TourRequest(string name, Location location, string language, int maxGuestNum, DateOnly startDate, DateOnly endDate, int idLocation, string description)
        {
            Name = name;
            Location = location;
            Language = language;
            MaxGuestNum = maxGuestNum;
            StartDate = startDate;
            EndDate = endDate;
            IdLocation = idLocation;
            Descripiton=description;
            Status = RequestType.OnHold;

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Location.City,
                Location.Country,
                Language,
                MaxGuestNum.ToString(),
                StartDate.ToString(),
                EndDate.ToString(),
                IdLocation.ToString(),
                Descripiton,
                Status.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Location = new Location(values[2], values[3]);
            Language = values[4];
            MaxGuestNum = int.Parse(values[5]);
            StartDate = DateOnly.Parse(values[6]);
            EndDate = DateOnly.Parse(values[7]);
            IdLocation = int.Parse(values[8]);
            Descripiton = values[9];
            Status = (RequestType)Enum.Parse(typeof(RequestType), values[10]);
        }

       
    }
}
