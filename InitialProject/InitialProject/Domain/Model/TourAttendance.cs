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
        public int IdGuide { get; set; }
        public int IdGuest { get; set; }
        public int IdTourPoint { get; set; }
        public bool UsedVoucher { get; set; }

        public TourAttendance() { }

        public TourAttendance(int idTour, int idGuide, int idGuest, int idTourPoint, bool usedVoucher)
        {
            IdTour = idTour;
            IdGuide = idGuide;
            IdGuest = idGuest;
            IdTourPoint = idTourPoint;
            UsedVoucher = usedVoucher;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            IdTour = int.Parse(values[1]);
            IdGuide = int.Parse(values[2]);
            IdGuest = int.Parse(values[3]);
            IdTourPoint = int.Parse(values[4]);
            UsedVoucher = bool.Parse(values[5]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IdTour.ToString(),
                IdGuide.ToString(),
                IdGuest.ToString(),
                IdTourPoint.ToString(),
                UsedVoucher.ToString()
            };
            return csvValues;
        }
    }
}
