using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class TourAttendenceService
    {
        private readonly TourAttendanceRepository _tourAttendenceRepository;
        public TourAttendenceService() 
        {
            _tourAttendenceRepository = new TourAttendanceRepository();
        }

        public TourAttendance Save(TourAttendance tourAttendance)
        {
            return _tourAttendenceRepository.Save(tourAttendance);
        }

        public List<TourAttendance> GetAllAttendedTours(User user)
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

        
    }
}
