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
    class TourService
    {
        private readonly TourRepository _tourRepository;
        List<Tour> _tours;
        private TourPointService _tourPointService;

        public TourService()
        {
            _tourRepository = new TourRepository();
            _tours= new List<Tour>(_tourRepository.GetAll());
            _tourPointService= new TourPointService();
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
            DateOnly futureDate = today.AddDays(2);
            
            if (tour.Date.CompareTo(futureDate) >= 0)
            {
                return true;
            }
            else
                return false;
        }

        public void CancelTour(Tour tour)
        {
            _tourRepository.Delete(tour);
            //vauceri
        }

    }
}
