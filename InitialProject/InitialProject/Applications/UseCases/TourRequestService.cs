using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using InitialProject.Serializer;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.Applications.UseCases
{
    public class TourRequestService
    {
        private readonly ITourRequestsRepository _tourRequestRepository;

        public TourRequestService()
        {
            _tourRequestRepository = Inject.CreateInstance<ITourRequestsRepository>();
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public List<TourRequest> GetAllUnaccepted()
        {
            List<TourRequest> requests = new List<TourRequest>();
            foreach (TourRequest request in _tourRequestRepository.GetAll())
            {
                if(request.Status == RequestType.OnHold)
                {
                    requests.Add(request);
                }
            }
            return requests;
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return _tourRequestRepository.Save(tourRequest);
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            return _tourRequestRepository.Update(tourRequest);
        }
    }
}
