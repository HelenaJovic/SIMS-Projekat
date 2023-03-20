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
        public string TourName { get; set; }    
        public int IdUser { get; set; }
        public int GuestNum { get; set; }
        public int IdLocation { get; set; }
        public int FreeSetsNum { get; set; }

        

        public TourReservation() { }

        public TourReservation(int idTour, string TourName,int idUser, int GuestNum, int freeSetsNum)
        {
            this.IdTour=idTour;
            this.TourName=TourName;
            this.IdUser=idUser;
            this.GuestNum=GuestNum;
            this.FreeSetsNum=freeSetsNum;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IdTour.ToString(),
                TourName,
                IdUser.ToString(),
                GuestNum.ToString(),
                FreeSetsNum.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            IdTour = int.Parse(values[1]);
            TourName = values[2];
            IdUser = int.Parse(values[3]);
            GuestNum = int.Parse(values[4]);
            FreeSetsNum = int.Parse(values[5]);

        }
    }
}
