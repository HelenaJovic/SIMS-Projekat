using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class ComplexTourRequestService
    {
        private readonly IComplexTourRequestRepository _complexTourRequestRepository;
        //private readonly LocationService _locationService;

        public ComplexTourRequestService()
        {
            _complexTourRequestRepository = Inject.CreateInstance<IComplexTourRequestRepository>();
            //_locationService = new LocationService();
        }

        public List<ComplexTourRequests> GetAll()
        {
            return _complexTourRequestRepository.GetAll();
        }
        /*
        public List<TourRequest> BindData(List<TourRequest> requests)
        {
            foreach (TourRequest tr in requests)
            {
                tr.Location = _locationService.GetById(tr.IdLocation);
            }
            return requests;
        }*/

        public void Delete(ComplexTourRequests complexTourRequest)
        {
            _complexTourRequestRepository.Delete(complexTourRequest);
        }

        public ComplexTourRequests Save(ComplexTourRequests complexTourRequest)
        {
            return _complexTourRequestRepository.Save(complexTourRequest);
        }

        public ComplexTourRequests Update(ComplexTourRequests complexTourRequest)
        {
            return _complexTourRequestRepository.Update(complexTourRequest);
        }

        public ComplexTourRequests GetById(int id)
        {
            return _complexTourRequestRepository.GetById(id);
        }

    }
}
