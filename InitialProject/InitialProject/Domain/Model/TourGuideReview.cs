using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace InitialProject.Domain.Model
{
    public class TourGuideReview : ISerializable
    {
        public int Id { get; set; }
        public User Guest { get; set; }
        public int IdGuest { get; set; }
        public int IdGuide { get; set; }
        public TourPoint TourPoint{ get; set; }
        public int IdTourPoint { get; set; }
        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int InterestingTour { get; set; }
        public string Comment { get; set; }
        public List<Image> Images { get; set; }
        public bool IsValid { get; set; }
        public int IdTour { get; set; }

        public TourGuideReview()
        {

        }
        public TourGuideReview(int idGuest, int idGuide, int idTourPoint, int guideKnowledge, int guideLanguage, int interestingTour, string comment, int idTour)

        {
            IdGuest = idGuest;
            IdGuide = idGuide;
            IdTourPoint= idTourPoint;
            GuideKnowledge = guideKnowledge;
            GuideLanguage=guideLanguage;
            InterestingTour=interestingTour;
            Comment=comment;
            IsValid = false;
            IdTour=idTour;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            IdGuest= int.Parse(values[1]);
            IdGuide = int.Parse(values[2]);
            IdTourPoint = int.Parse(values[3]);
            GuideKnowledge = int.Parse(values[4]);
            GuideLanguage = int.Parse(values[5]);
            InterestingTour = int.Parse(values[6]);
            Comment = values[7];
            IsValid = bool.Parse(values[8]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IdGuest.ToString(),
                IdGuide.ToString(),
                IdTourPoint.ToString(),
                GuideKnowledge.ToString(),
                GuideLanguage.ToString(),
                InterestingTour.ToString(),
                Comment,
                IsValid.ToString()
            };

            return csvValues;
        }
    }
}
