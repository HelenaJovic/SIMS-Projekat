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
        public int IdGuide { get; set; }
        public int IdTour { get; set; }
        public int GuideKnowledge { get; set; }
        public int GuideLanguage { get; set; }
        public int InterestingTour { get; set; }
        public string Comment { get; set; }
        public List<Image> Images { get; set; }

        public TourGuideReview()
        {

        }
        public TourGuideReview(int idGuide, int idTour, int guideKnowledge, int guideLanguage, int interestingTour, string comment)
        {
            IdGuide = idGuide;
            IdTour = idTour;
            GuideKnowledge = guideKnowledge;
            GuideLanguage=guideLanguage;
            InterestingTour=interestingTour;
            Comment=comment;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            IdGuide = int.Parse(values[1]);
            IdTour = int.Parse(values[2]);
            GuideKnowledge = int.Parse(values[3]);
            GuideLanguage = int.Parse(values[4]);
            InterestingTour = int.Parse(values[5]);
            Comment = values[6];
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                IdGuide.ToString(),
                IdTour.ToString(),
                GuideKnowledge.ToString(),
                GuideLanguage.ToString(),
                InterestingTour.ToString(),
                Comment
            };

            return csvValues;
        }
    }
}
