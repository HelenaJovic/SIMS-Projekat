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
    public class TourReservationService
    {
        private readonly TourReservationRepository _tourReservationRepository;
        List<TourReservation> _toursReservation;
        private readonly UserRepository _userRepository;
        private readonly TourAttendanceRepository _tourAttendenceRepository;
        public TourReservationService() 
        {
            _tourReservationRepository = new TourReservationRepository();
            _userRepository = new UserRepository();
            _toursReservation = new List<TourReservation>(_tourReservationRepository.GetAll());
        }

        public List<TourReservation> GetByUser(User user)
        {
            return _tourReservationRepository.GetByUser(user);
        }

        public void Delete(TourReservation tourReservation)
        {
            _tourReservationRepository.Delete(tourReservation);
        }

        public List<User> GetUsersByTour(Tour tour)
        {
            List<User> users = new List<User>();
            User user = new User();
            foreach (TourReservation reservation in _toursReservation)
            {
                if (reservation.IdTour == tour.Id)
                {
                    user = _userRepository.GetById(reservation.IdUser);
                    users.Add(user);
                }
            }
            return users;
        }

        public List<TourReservation> GetAll() {
            List<TourReservation> tourReservations= new List<TourReservation>();
            tourReservations = _tourReservationRepository.GetAll();
            return tourReservations;
        }

        

    }
}
