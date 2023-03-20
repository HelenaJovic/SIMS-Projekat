using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;

namespace InitialProject.Repository
{
    internal class TourPointRepository
    {
        private const string FilePath = "../../../Resources/Data/tourpoints.csv";

        private readonly Serializer<TourPoint> _serializer;

        private List<TourPoint> _tourpoints;

        public TourPointRepository()
        {
            _serializer = new Serializer<TourPoint>();
            _tourpoints = _serializer.FromCSV(FilePath);
        }

        public List<TourPoint> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public TourPoint Save(TourPoint tourpoint)
        {
            tourpoint.Id = NextId();
            _tourpoints = _serializer.FromCSV(FilePath);
            _tourpoints.Add(tourpoint);
            _serializer.ToCSV(FilePath, _tourpoints);
            return tourpoint;
        }

        public int NextId()
        {
            _tourpoints = _serializer.FromCSV(FilePath);
            if (_tourpoints.Count < 1)
            {
                return 1;
            }
            return _tourpoints.Max(c => c.Id) + 1;
        }

        public void Delete(TourPoint tourpoint)
        {
            _tourpoints = _serializer.FromCSV(FilePath);
            TourPoint founded = _tourpoints.Find(c => c.Id == tourpoint.Id);
            _tourpoints.Remove(founded);
            _serializer.ToCSV(FilePath, _tourpoints);
        }

        public TourPoint Update(TourPoint tourpoint)
        {
            _tourpoints = _serializer.FromCSV(FilePath);
            TourPoint current = _tourpoints.Find(c => c.Id == tourpoint.Id);
            int index = _tourpoints.IndexOf(current);
            _tourpoints.Remove(current);
            _tourpoints.Insert(index, tourpoint);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _tourpoints);
            return tourpoint;
        }

<<<<<<< HEAD
        public List<TourPoint> GetAllByTourId(Tour tour)
        {
            _tourpoints = _serializer.FromCSV(FilePath);
            return _tourpoints.FindAll(c => c.IdTour == tour.Id);
=======
        public List<TourPoint> GetAllByTourId(int idTour)
        {
            _tourpoints = _serializer.FromCSV(FilePath);
            return _tourpoints.FindAll(c => c.IdTour == idTour);
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
        }

        public void ActivateFirstPoint(Tour tour)
        {
<<<<<<< HEAD
            foreach(TourPoint tourPoint in _tourpoints)
            {
                if(tourPoint.IdTour == tour.Id && tourPoint.Order == 1)
                {
                    int index = _tourpoints.IndexOf(tourPoint);
                    tourPoint.Active=true;
=======
            foreach (TourPoint tourPoint in _tourpoints)
            {
                if (tourPoint.IdTour == tour.Id && tourPoint.Order == 1)
                {
                    int index = _tourpoints.IndexOf(tourPoint);
                    tourPoint.Active = true;
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
                    _tourpoints.Remove(tourPoint);
                    _tourpoints.Insert(index, tourPoint);
                    _serializer.ToCSV(FilePath, _tourpoints);
                    return;
                }
            }
        }
<<<<<<< HEAD
=======

        public int FindMaxOrder(int idTour)
        {
            int max = 2;
            foreach (TourPoint tourPoint in _tourpoints)
            {
                if (tourPoint.IdTour == idTour && tourPoint.Order > max)
                {
                    max=tourPoint.Order;
                }
            }
            return max;
        }
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
    }
}
