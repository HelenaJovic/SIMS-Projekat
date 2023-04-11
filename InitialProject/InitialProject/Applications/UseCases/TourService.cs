using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using InitialProject.Serializer;
using InitialProject.WPF.ViewModel;
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

        public TourService()
        {
            _tourRepository = Inject.CreateInstance<ITourRepository>();
            _voucherRepository = Inject.CreateInstance<IVoucherRepository>();
            _tours= new List<Tour>(_tourRepository.GetAll());
            _tourPointService= new TourPointService();
            _tourReservationService = new TourReservationService();
        }
        public List<Tour> GetUpcomingToursByUser(User user)
        {
            List<Tour> Tours = new List<Tour>();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            foreach (Tour tour in _tours)
            {
                if (tour.IdUser == user.Id && tour.Date.CompareTo(today) >= 0 && IsTimePassed(tour))
                {
                    Tours.Add(tour);
                }
            }
            return Tours;
        }

        public List<Tour> GetAllByUser(User user)
        {
            List<Tour> tours = new List<Tour>();
            tours = _tourRepository.GetByUser(user);
            return tours;
        }
        
        public Tour GetActiveTour(User user)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            Tour tour = new Tour();
            foreach (Tour t in _tourRepository.GetAll())
            {
                foreach(TourReservation tRes in _tourReservationService.GetByUser(user))
                {
                    if(t.Id == tRes.IdTour)
                    {
                        if (t.Date.CompareTo(today) == 0 && IsTimeActive(t))
                        {
                            tour = _tourRepository.GetById(tRes.IdTour);
                            /*_tourAttendenceRepository.Save(tourAttendance1);
                            _tourReservationService.Delete(tRes);
                            Guest2MainWindowViewModel.ReservedTours.Clear();
                            foreach (TourReservation tResserved in _tourReservationService.GetByUser(user))
                            {
                                Guest2MainWindowViewModel.ReservedTours.Add(tResserved);
                            }*/
                        }
                    }
                }    
            }
            return tour;
        }

        private bool IsTimeActive(Tour t)
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


        public void EndTour(Tour tour)
        {
            tour.Active = false;
            _tourRepository.Update(tour);
        }

        public bool IsUserAvaliable(User user)
        {
            _tours = new List<Tour>(_tourRepository.GetAll());
            foreach (Tour tour in _tours)
            {
                if (tour.IdUser == user.Id && tour.Active == true)
                    return false;
            }
            return true;
        }

        public Tour Save(Tour tour)
        {
            Tour savedTour = _tourRepository.Save(tour);
            return savedTour;
        }

        public List<Tour> GetAllByUserAndDate(User user, DateTime currentDay)
        {
            List<Tour> tours= new List<Tour>();
            tours = _tourRepository.GetAllByUserAndDate(user, currentDay);
            return tours;
        }

        public List<int> GetAllYears(User user)
        {
            List<int> years = new List<int>();
            foreach(Tour t in _tours)
            {
                if (!years.Contains(t.Date.Year))
                {
                    years.Add(t.Date.Year);
                }
            }
            return years;
        }

        public bool IsCancellationPossible(Tour tour)
        {
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            TimeOnly currentTime = TimeOnly.FromDateTime(DateTime.Now);
            DateOnly futureDate = today.AddDays(2);

            if (tour.Date.CompareTo(futureDate) > 0)
            {
                return true;
            }
            else if (tour.Date.CompareTo(futureDate) == 0)
            {
                if (tour.StartTime > currentTime)
                    return true;
                else
                    return false;
            }
            else
            {
                return false;
            }


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
            List<TourReservation> reservations = new List<TourReservation>(_tourReservationService.GetAll());
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);

            foreach (TourReservation tr in reservations) { 
                if(tr.IdTour == tour.Id)
                {
                    Voucher voucher = new Voucher(tr.IdUser, "Cancellation voucher", today.AddYears(1));
                    _voucherRepository.Save(voucher);
                }
            }
        }

    }
}
