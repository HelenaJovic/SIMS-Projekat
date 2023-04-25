using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
    public class GuestReview : ISerializable
    {
        public int Id { get; set; }

        public int IdOwner { get; set; }

        public int IdReservation { get; set; }

        public AccommodationReservation Reservation { get; set; }

        public int CleanlinessGrade { get; set; }

        public int RuleGrade { get; set; }

        public string GuestComment { get; set; }

        public int IdGuest { get; set; }

        public GuestReview()
        {

        }

        public GuestReview(int idOwner, int idReservation, int cleanlinessGrade, int ruleGrade, string comment,int idGuest)
        {
            IdOwner = idOwner;
            IdReservation = idReservation;
            CleanlinessGrade = cleanlinessGrade;
            RuleGrade = ruleGrade;
            GuestComment = comment;
            IdGuest = idGuest;

        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            IdOwner = int.Parse(values[1]);
            IdReservation = int.Parse(values[2]);
            CleanlinessGrade = int.Parse(values[3]);
            RuleGrade = int.Parse(values[4]);
            GuestComment = values[5];
            IdGuest=int.Parse(values[6]);


        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IdOwner.ToString(),
                IdReservation.ToString(),
                CleanlinessGrade.ToString(),
                RuleGrade.ToString(),
                GuestComment,
                IdGuest.ToString(),


            };
            return csvValues;
        }
    }
}
