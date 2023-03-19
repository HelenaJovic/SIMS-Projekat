using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.Model
{
    public class Tour : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public string Descripiton { get; set; }
        public string Language { get; set; }
        public int MaxGuestNum { get; set; }
        public List<TourPoint> Points { get; set; }
        public DateOnly Date { get; set; }
        public int StartTime { get; set; }
        public int Duration { get; set; }
        public List<Image> Images { get; set; }
        public int FreeSetsNum { get; set; }
        public bool Active { get; set; }
        public int IdUser { get; set; }


        public Tour()
        {
            Points = new List<TourPoint>();
            Images = new List<Image>();
        }

        public Tour(string name, Location location, string language, int maxGuestNum, DateOnly startDate, int startTime, int duration, int freeSetsNum, bool active, int idUser)
        {
            Name = name;
            Location = location;
            Language = language;
            MaxGuestNum = maxGuestNum;
            Date = startDate;
            StartTime = startTime;
            Duration = duration;
            FreeSetsNum = freeSetsNum;
            Active = active;
            IdUser = idUser;
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
                Date.ToString(),
                StartTime.ToString(),
                Duration.ToString(),
                FreeSetsNum.ToString(),
                Active.ToString(),
                IdUser.ToString()
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
            Date = DateOnly.Parse(values[6]);
            StartTime = int.Parse(values[7]);
            Duration = int.Parse(values[8]);
            FreeSetsNum = int.Parse(values[9]);
            Active = bool.Parse(values[10]);
            IdUser = int.Parse(values[11]);
        }
    }
}