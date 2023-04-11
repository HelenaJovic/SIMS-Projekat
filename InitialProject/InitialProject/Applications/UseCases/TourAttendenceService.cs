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
        private readonly TourService _tourService;
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourRepository _tourRepository;
        public TourAttendenceService() 
        {
            _tourAttendenceRepository = new TourAttendanceRepository();
            _tourService = new TourService();
            _tourReservationRepository = new TourReservationRepository();
            _tourRepository = new TourRepository();
        }

        public List<TourAttendance> GetAllAttendedTours(User user)
        {
            List<TourAttendance> tourAttended = new List<TourAttendance>();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            foreach (Tour t in _tourRepository.GetAll())
            {
                foreach (TourReservation tRes in _tourReservationRepository.GetAll())
                {
                    if (t.Id == tRes.IdTour)
                    {
                        if (t.Date.CompareTo(today) <= 0 && IsTimePassed(t))
                        {
                            TourAttendance tourAttendance1 = new TourAttendance(tRes.IdTour, tRes.IdUser, tRes.IdTourPoint);
                            tourAttended.Add(tourAttendance1);
                            _tourAttendenceRepository.Save(tourAttendance1 );
                            _tourReservationRepository.Delete(tRes);
                            Guest2MainWindowViewModel.ReservedTours.Clear();
                            foreach(TourReservation tResserved in _tourReservationRepository.GetByUser(user))
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
    }
}
