using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class TourService
    {
        private readonly ITourRepository _tourRepository;
        private readonly IVoucherRepository _voucherRepository;
        List<Tour> _tours;
        private TourPointService _tourPointService;
        private TourReservationService _tourReservationService;
        private TourAttendanceService _tourAttendenceService;


        public TourService()
        {
            _tourRepository = Inject.CreateInstance<ITourRepository>();
            _voucherRepository = Inject.CreateInstance<IVoucherRepository>();
            _tours= new List<Tour>(_tourRepository.GetAll());
            _tourPointService= new TourPointService();
            _tourReservationService = new TourReservationService();
            _tourAttendenceService = new TourAttendanceService();
        }
        public List<Tour> GetUpcomingToursByUser(User user)
        {
            List<Tour> Tours = new List<Tour>();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            foreach (Tour tour in _tourRepository.GetByUser(user))
            {
                if (tour.Date.CompareTo(today) >= 0 && IsTimePassed(tour))
                {
                    Tours.Add(tour);
                }
            }
            return Tours;
        }

        public List<Tour> GetAllByUser(User user)
        {
            return _tourRepository.GetByUser(user); ;
        }

        public List<Tour> GetActiveTour()
        {
            List<Tour> tours = new List<Tour>();
            foreach (Tour t in _tourRepository.GetAll())
            {
                ActiveTourCheck(tours, t);
            }
            return tours;
        }

        private void ActiveTourCheck(List<Tour> tours, Tour t)
        {
            foreach (TourAttendance tourAttendance in _tourAttendenceService.GetAllAttendedTours())
            {
                if (t.Id == tourAttendance.IdTour && t.Active==true)
                {
                    tours.Add(_tourRepository.GetById(t.Id));
                }
            }
        }

        public Tour Update(Tour tour)
        {
            return _tourRepository.Update(tour);
        }
        public string GetTourNameById(int id)
        {
            return _tourRepository.GetTourNameById(id);
        }

        public Tour GetById(int id)
        {
            return _tourRepository.GetById(id);
        }

        public bool IsTimePassed(Tour tour)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
            if (tour.Date == today)
            {
                if (tour.StartTime <= currentTime) // ako bude trebalo za kasnije -> tour.StartTime.AddHours(time.Duration) <= currentTime
                    return false;
            }
            return true;
        }

       

        public void StartTour(Tour tour)
        {
            tour.Active = true;
            _tourPointService.ActivateFirstPoint(tour);
            _tourRepository.Update(tour);
        }


        public Tour Save(Tour tour)
        {
            Tour savedTour = _tourRepository.Save(tour);
            return savedTour;
        }

        public List<Tour> GetAllByUserAndDate(User user, DateTime currentDay)
        {
            return _tourRepository.GetAllByUserAndDate(user, currentDay); ;
        }


        public Location GetLocationById(int id)
        {
            return _tourRepository.GetLocationById(id);
        }

        public List<Tour> GetAll()
        {
            return _tourRepository.GetAll();
        }

        public void CancelTour(Tour tour)
        {
            _tourRepository.Delete(tour);
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            foreach (TourReservation tr in _tourReservationService.GetAll())
            { 
                if(tr.IdTour == tour.Id)
                {
                    Voucher voucher = new Voucher(tr.IdUser, "Cancellation voucher", today.AddYears(1));
                    _voucherRepository.Save(voucher);
                }
            }
        }

    }
}
