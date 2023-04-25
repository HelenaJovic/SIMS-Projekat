using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class RecommendationService
    {
        private readonly IRecommendationRepository recommendationRepository;

        public RecommendationService()
        {
            recommendationRepository = Inject.CreateInstance<IRecommendationRepository>();

            
        }

        public List<RecommendationOnAccommodation> GetByUser(User user)
        {
           return recommendationRepository.GetByUser(user);
        }

        public List<RecommendationOnAccommodation> GetAll()
        {
            return recommendationRepository.GetAll();
        }

        public RecommendationOnAccommodation Save(RecommendationOnAccommodation recommendation)
        {
            return recommendationRepository.Save(recommendation);
        }

        public void Delete(RecommendationOnAccommodation recommendation)
        {
             recommendationRepository.Delete(recommendation);
        }

        public RecommendationOnAccommodation Update(RecommendationOnAccommodation recommendation)
        {

            return recommendationRepository.Update(recommendation);
        }


    }
}
