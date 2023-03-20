﻿using InitialProject.Model;
using InitialProject.Serializer;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class AccommodationReservationRepository
    {

        private const string FilePath = "../../../Resources/Data/accommodationreservations.csv";


        private readonly Serializer<AccommodationReservation> _serializer;

        private List<AccommodationReservation> _accommodationReservations;

        public User LoggedInUser { get; set; }



        public AccommodationReservationRepository()
        {
            _serializer = new Serializer<AccommodationReservation>();
            _accommodationReservations = _serializer.FromCSV(FilePath);


        }

        public List<AccommodationReservation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public List<DateOnly> GetAllStartDates(int id)
        {
            List<DateOnly> dates = new List<DateOnly>();
            foreach (AccommodationReservation reservation in _accommodationReservations)
            { if (reservation.IdAccommodation == id)
                {
                    dates.Add(reservation.StartDate);
                }
            }
            return dates;
        }

        public List<DateOnly> GetAllEndDates(int id)
        {
            List<DateOnly> dates = new List<DateOnly>();
            foreach (AccommodationReservation reservation in _accommodationReservations)
            {
                if (reservation.IdAccommodation == id)
                {
                    dates.Add(reservation.EndDate);
                }
            }
            return dates;
        }

        public string GetNameById(int id)
        {
            Guest1MainWindow guest1MainWindow = new Guest1MainWindow(LoggedInUser);
            foreach (Accommodation accommodation in Guest1MainWindow.AccommodationsMainList)
            {
                if (accommodation.Id == id)
                {
                    return accommodation.Name;
                }
                return null;
            }
            return null;
        }

        public AccommodationReservation Save(AccommodationReservation accommodationReservation)
        {
            accommodationReservation.Id = NextId();
            _accommodationReservations = _serializer.FromCSV(FilePath);
            _accommodationReservations.Add(accommodationReservation);
            _serializer.ToCSV(FilePath, _accommodationReservations);
            return accommodationReservation;
        }

        public int NextId()
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            if (_accommodationReservations.Count < 1)
            {
                return 1;
            }
            return _accommodationReservations.Max(c => c.Id) + 1;
        }



        public void Delete(AccommodationReservation accommodationReservation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation founded = _accommodationReservations.Find(c => c.Id == accommodationReservation.Id);
            _accommodationReservations.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodationReservations);
        }

        public AccommodationReservation Update(AccommodationReservation accommodationReservation)
        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            AccommodationReservation current = _accommodationReservations.Find(c => c.Id == accommodationReservation.Id);
            int index = _accommodationReservations.IndexOf(current);
            _accommodationReservations.Remove(current);
            _accommodationReservations.Insert(index, accommodationReservation);       
            _serializer.ToCSV(FilePath, _accommodationReservations);
            return accommodationReservation;
        }

        public List<AccommodationReservation> GetByUser(User user)

        {
            _accommodationReservations = _serializer.FromCSV(FilePath);
            return _accommodationReservations.FindAll(a => a.IdGuest == user.Id);


        }
        


        public List<AccommodationReservation> GetByOwnerId(int id)
        {
            return _accommodationReservations.FindAll(c => c.Accommodation.IdUser == id);

        }
    }
}

