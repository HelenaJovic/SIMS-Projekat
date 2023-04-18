using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
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
        private readonly ITourReservationRepository _tourReservationRepository;
<<<<<<< HEAD
        List<TourReservation> _toursReservation;
        private readonly UserService _userService;

=======
        private readonly UserService _userService;
>>>>>>> 847b4e61cbc36cda5dd8573467dc9a22aadbbf76
        public TourReservationService()
        {
            _tourReservationRepository = Inject.CreateInstance<ITourReservationRepository>();
            _userService = new UserService();
<<<<<<< HEAD
            _toursReservation = new List<TourReservation>(_tourReservationRepository.GetAll());
=======
>>>>>>> 847b4e61cbc36cda5dd8573467dc9a22aadbbf76
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
            foreach (TourReservation reservation in _tourReservationRepository.GetAll())
            {
                if (reservation.IdTour == tour.Id)
                {
                    user = _userService.GetById(reservation.IdUser);
                    users.Add(user);
                }
            }
            return users;
        }

        public List<TourReservation> GetByTour(int id)
        {
            return _tourReservationRepository.GetByTour(id);
        }
        public TourReservation Update(TourReservation tourReservation)
        {
            return _tourReservationRepository.Update(tourReservation);
        }
        public List<TourReservation> GetAll()
        {
            List<TourReservation> tourReservations = new List<TourReservation>();
            tourReservations = _tourReservationRepository.GetAll();
            return tourReservations;
        }

        public TourReservation Save(TourReservation tourReservation)
        {
            return _tourReservationRepository.Save(tourReservation);
        }


    }
}
