using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
    public class RecommendationOnAccommodation: ISerializable
    {
        public int Id { get; set; } 
        public string Comment { get; set; }

        public int IdOwnerReview { get; set; }

        public LevelType Level { get; set; }

        public int IdOwner { get; set; }

        public int IdGuest { get; set; }

        public OwnerReview OwnerReview { get; set; }

        public RecommendationOnAccommodation(OwnerReview owner, string comment, int idOwnerReview, LevelType level,int idGuest)
        {
            OwnerReview= owner;
            Comment = comment;
            IdOwnerReview = idOwnerReview;
            Level = level;
            IdGuest = idGuest;
        }

        public RecommendationOnAccommodation()
        {

        }


        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Comment,
                IdOwnerReview.ToString(),
                Level.ToString(),
                IdGuest.ToString()
                
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Comment = values[1];
            IdOwnerReview = int.Parse(values[2]);
            Level = (LevelType)Enum.Parse(typeof(LevelType), values[3]);
            IdGuest = int.Parse(values[4]);


        }
    }
}
