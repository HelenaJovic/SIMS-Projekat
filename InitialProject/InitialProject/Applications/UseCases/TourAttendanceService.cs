﻿using InitialProject.Repository;
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
        private readonly UserService _userService;
        public TourAttendanceService()
        {
            _tourAttendenceRepository = Inject.CreateInstance<ITourAttendanceRepository>();
            _userService = new UserService();
        }

        public TourAttendance Save(TourAttendance tourAttendance)
        {
            return _tourAttendenceRepository.Save(tourAttendance);
        }
        public List<TourAttendance> GetAll()
        {
            return _tourAttendenceRepository.GetAll();
        }

        public void Delete(TourAttendance tourattendance)
        {
            _tourAttendenceRepository.Delete(tourattendance);
        }

        public List<TourAttendance> GetAllByTourId(int id)
        {
            List<TourAttendance> tours = _tourAttendenceRepository.GetAll();
            return tours.FindAll(t => t.IdTour == id);
        }
        public List<TourAttendance> GetAllAttendedToursByUser(User user)
        {
            List<TourAttendance> tours = _tourAttendenceRepository.GetAll();
            return tours.FindAll(t => t.IdGuest == user.Id);
        }

        public TourAttendance Update(TourAttendance tourattendance)
        {
            return _tourAttendenceRepository.Update(tourattendance);
        }

        public List<TourAttendance> GetAllByGuide(User user)
        {
            return _tourAttendenceRepository.GetAllByGuide(user);
        }


        public double FindWithVoucher(Tour tour)
        {
            double n = 0;
            double with = 0;
            foreach (TourAttendance ta in _tourAttendenceRepository.GetAll())
            {
                if (tour.Id == ta.IdTour)
                {
                    n++;
                    if (ta.UsedVoucher == true)
                    {
                        with++;
                    }
                }
            }
            return CalculateRes(n, with);
        }

        private double CalculateRes(double n, double with)
        {
            if (n == 0)
            {
                return 0;
            }

            double res = 100 * (with / n);
            return res;
        }


        public int FindYoungest(Tour tour)
        {
            int i = 0;
            foreach (TourAttendance ta in GetAllByTourId(tour.Id))
            {
                User user = _userService.GetById(ta.IdGuest);
                if (user.Age < 18)
                {
                    i++;
                }
            }
            return i;
        }

        public int FindMediumAge(Tour tour)
        {
            int i = 0;
            foreach (TourAttendance ta in GetAllByTourId(tour.Id))
            {
                User user = _userService.GetById(ta.IdGuest);
                if (user.Age >= 18 && user.Age <= 50)
                {
                    i++;
                }
            }
            return i;
        }

        public int FindOldestAge(Tour tour)
        {
            int i = 0;
            foreach (TourAttendance ta in GetAllByTourId(tour.Id))
            {
                User user = _userService.GetById(ta.IdGuest);
                if (user.Age > 50)
                {
                    i++;
                }
            }
            return i;
        }

    }
}
