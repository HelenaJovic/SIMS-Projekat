using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class TourStatisticsViewModel
    {
        public Tour SelectedTour { get; set; }
        public int Youngest { get; set; }
        public int MediumAge { get; set; }
        public int Oldest { get; set; }
        public String WithVoucher { get; set; }
        public String WithoutVoucher { get; set; }

        private readonly TourAttendanceService _tourAttendanceService;
        private readonly IUserRepository _userRepository;
        public TourStatisticsViewModel(Tour tour) 
        {
            SelectedTour = tour;
            _tourAttendanceService = new TourAttendanceService();
            _userRepository = Inject.CreateInstance< IUserRepository>();

            Youngest = FindYoungest(tour);
            MediumAge = FindMediumAge(tour);
            Oldest = FindOldestAge(tour);

            WithVoucher = FindWithVoucher(tour);
            WithoutVoucher = FindWithoutVoucher(tour);
        }

        public String FindWithVoucher(Tour tour)
        {
            double n = 0;
            double with = 0;
            foreach (TourAttendance ta in _tourAttendanceService.GetAll())
            {
                if (tour.Id == ta.IdTour)
                {
                    n++;
                    if(ta.UsedVoucher == true)
                    {
                        with++;
                    }
                }
            }
            if (n == 0)
            {
                return "0%";
            }
            String res = (100 * (with / n)).ToString() + "%";
            return res;
        }

        public String FindWithoutVoucher(Tour tour)
        {
            double n = 0;
            double with = 0;
            foreach (TourAttendance ta in _tourAttendanceService.GetAll())
            {
                if (tour.Id == ta.IdTour)
                {
                    n++;
                    if (ta.UsedVoucher == false)
                    {
                        with++;
                    }

                }
            }
            if (n == 0)
            {
                return "0%";
            }
            String res = (100 * (with / n)).ToString() + "%";
            return res;
        }

        public int FindYoungest(Tour tour)
        {
           int i = 0;
           foreach(TourAttendance ta in _tourAttendanceService.GetAll())
           {
                User user = _userRepository.GetById(ta.IdGuest);
                if (tour.Id == ta.IdTour && user.Age < 18 )
                {
                    i++;
                }
           }
            return i;
        }

        public int FindMediumAge(Tour tour)
        {
            int i = 0;
            foreach (TourAttendance ta in _tourAttendanceService.GetAll())
            {
                User user = _userRepository.GetById(ta.IdGuest);
                if (tour.Id == ta.IdTour && user.Age >= 18 && user.Age <= 50 )
                {
                    i++;
                }
            }
            return i;
        }

        public int FindOldestAge(Tour tour)
        {
            int i = 0;
            foreach (TourAttendance ta in _tourAttendanceService.GetAll())
            {
                User user = _userRepository.GetById(ta.IdGuest);
                if (tour.Id == ta.IdTour && user.Age > 50)
                {
                    i++;
                }
            }
            return i;
        }


    }
}
