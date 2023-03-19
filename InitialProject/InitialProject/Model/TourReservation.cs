using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Model
{
    public class TourReservation : ISerializable
    {
        public int Id { get; set; }
        public int IdTour { get; set; }
        public int IdUser { get; set; }
        public int GuestNum { get; set; }
        public int IdLocation { get; set; }

        public TourReservation() { }

        public TourReservation(int id, int idTour, int idUser, int GuestNum)
        {
            this.Id=id;
            this.IdTour=idTour;
            this.IdUser=idUser;
            this.GuestNum=GuestNum;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IdTour.ToString(),
                IdUser.ToString(),
                GuestNum.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            IdTour = int.Parse(values[1]);
            IdUser = int.Parse(values[2]);
            GuestNum = int.Parse(values[3]);
        }
    }
}
