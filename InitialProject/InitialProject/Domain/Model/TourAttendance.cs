using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml.Linq;

namespace InitialProject.Domain.Model
{
    public class TourAttendance : ISerializable
    {
        public int Id { get; set; }
        public int IdTour { get; set; }
        public Tour Tour { get; set; }
        public int IdGuest { get; set; }
        public int IdTourPoint { get; set; }

        public TourAttendance() { }

        public TourAttendance(int idTour, int idGuest, int idTourPoint)
        {
            IdTour = idTour;
            IdGuest = idGuest;
            IdTourPoint = idTourPoint;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            IdTour = int.Parse(values[1]);
            IdGuest = int.Parse(values[2]);
            IdTourPoint = int.Parse(values[3]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IdTour.ToString(),
                IdGuest.ToString(),
                IdTourPoint.ToString(),
            };
            return csvValues;
        }
    }
}
