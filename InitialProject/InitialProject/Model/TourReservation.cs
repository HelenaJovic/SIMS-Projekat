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
<<<<<<< HEAD
        public string TourName { get; set; }    
=======
        public string TourName { get; set; }
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
        public int IdUser { get; set; }
        public int GuestNum { get; set; }
        public int IdLocation { get; set; }
        public int FreeSetsNum { get; set; }
<<<<<<< HEAD

        

        public TourReservation() { }

        public TourReservation(int idTour, string TourName,int idUser, int GuestNum, int freeSetsNum)
        {
            this.IdTour=idTour;
            this.TourName=TourName;
            this.IdUser=idUser;
            this.GuestNum=GuestNum;
            this.FreeSetsNum=freeSetsNum;
=======
        public int IdTourPoint { get; set; }
        public string UserName { get; set; }



        public TourReservation() { }

        public TourReservation(int idTour, string TourName, int idUser, int GuestNum, int freeSetsNum, int idTourPoint, string userName)
        {
            this.IdTour = idTour;
            this.TourName = TourName;
            this.IdUser = idUser;
            this.GuestNum = GuestNum;
            this.FreeSetsNum = freeSetsNum;
            this.IdTourPoint = idTourPoint;
            this.UserName = userName;
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
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
<<<<<<< HEAD
=======
                IdTourPoint.ToString(),
                UserName
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
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
<<<<<<< HEAD
=======
            IdTourPoint = int.Parse(values[6]);
            UserName = values[7];
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346

        }
    }
}
