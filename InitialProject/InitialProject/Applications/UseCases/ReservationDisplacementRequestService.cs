using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class ReservationDisplacementRequestService
    {
        private readonly IReservationDisplacementRequestRepository reservationDisplacementRequestRepository;
   
        public ReservationDisplacementRequestService()
        {
            reservationDisplacementRequestRepository = Inject.CreateInstance<IReservationDisplacementRequestRepository>();
      
        }

        public List<ReservationDisplacementRequest> GetAll()
        {
            List<ReservationDisplacementRequest> requests = reservationDisplacementRequestRepository.GetAll();
            
            return requests;
        }

    }
}
