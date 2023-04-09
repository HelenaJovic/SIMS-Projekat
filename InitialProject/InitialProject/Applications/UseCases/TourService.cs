using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class TourService
    {
        private readonly TourRepository _tourRepository;
        private readonly VoucherRepository _voucherRepository;
        List<Tour> _tours;
        private TourPointService _tourPointService;
        private TourReservationService _tourReservationService;

        public TourService()
        {
            _tourRepository = new TourRepository();
            _voucherRepository = new VoucherRepository();
            _tours= new List<Tour>(_tourRepository.GetAll());
            _tourPointService= new TourPointService();
            _tourReservationService= new TourReservationService();
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
