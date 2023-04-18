using InitialProject.Repository;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InitialProject.Injector;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Serializer;

namespace InitialProject.Applications.UseCases
{
    public class TourAttendanceService
    {
        private readonly ITourAttendanceRepository _tourAttendenceRepository;
        public TourAttendanceService()
        {
            _tourAttendenceRepository = Inject.CreateInstance<ITourAttendanceRepository>();
        }

        public TourAttendance Save(TourAttendance tourAttendance)
        {
            return _tourAttendenceRepository.Save(tourAttendance);
        }

        public List<TourAttendance> GetAllAttendedTours()
        {
            return _tourAttendenceRepository.GetAll();
        }
       
        public List<TourAttendance> GetAll()
        {
            return _tourAttendenceRepository.GetAll();
        }

        public void Delete(TourAttendance tourattendance)
        {
            _tourAttendenceRepository.Delete(tourattendance);
        }
        public List<TourAttendance> GetAllAttendedToursByUser(User user)
        {
            List<TourAttendance> tours = new List<TourAttendance>();

            foreach (TourAttendance tourAttendance in _tourAttendenceRepository.GetAll())
            {
                if (tourAttendance.IdGuest == user.Id)
                {
                    tours.Add(tourAttendance);
                }
            }
            return tours;
        }

        public TourAttendance Update(TourAttendance tourattendance)
        {
            return _tourAttendenceRepository.Update(tourattendance);
        }
    }
}
