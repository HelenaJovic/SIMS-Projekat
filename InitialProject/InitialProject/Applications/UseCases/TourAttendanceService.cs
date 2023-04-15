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
        /*
        public List<TourAttendance> MakeAttendedTours(User user)
        {
            List<TourAttendance> tourAttended = new List<TourAttendance>();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            foreach (Tour t in _tourService.GetAll())
            {
                foreach (TourReservation tRes in _tourReservationService.GetAll())
                {
                    if (t.Id == tRes.IdTour)
                    {
                        if (t.Date.CompareTo(today) <= 0 && IsTimePassed(t))
                        {
                            Tour tour = _tourService.GetById(tRes.IdTour);
                            TourAttendance tourAttendance1 = new TourAttendance(tRes.IdTour, tour, tRes.IdUser, tRes.IdTourPoint);
                            tourAttended.Add(tourAttendance1);
                            _tourAttendenceRepository.Save(tourAttendance1);
                            _tourReservationService.Delete(tRes);
                            Guest2MainWindowViewModel.ReservedTours.Clear();
                            foreach(TourReservation tResserved in _tourReservationService.GetByUser(user))
                            {
                                Guest2MainWindowViewModel.ReservedTours.Add(tResserved);
                            }
                        }
                    }
                }
            }

            return tourAttended;
        }
        

        private bool IsTimePassed(Tour t)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
            if (t.Date == today)
            {
                if (t.StartTime >= currentTime) // ako bude trebalo za kasnije -> tour.StartTime.AddHours(time.Duration) <= currentTime
                    return false;
            }
            return true;
        }
        */

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
            foreach(TourAttendance tourAttendance in _tourAttendenceRepository.GetAll()) 
            {
                if(tourAttendance.IdGuest == user.Id)
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
