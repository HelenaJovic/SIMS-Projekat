using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Repository;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    class TourPointService
    {
        private readonly TourPointRepository _tourPointRepository;
        private List<TourPoint> _tourpoints;
        public TourPointService() {
            _tourPointRepository = new TourPointRepository();
            _tourpoints = new List<TourPoint>(_tourPointRepository.GetAll());
        }

        public void ActivateFirstPoint(Tour tour)
        {
            foreach (TourPoint tourPoint in _tourpoints)
            {
                if (tourPoint.IdTour == tour.Id && tourPoint.Order == 1)
                {
                    int index = _tourpoints.IndexOf(tourPoint);
                    tourPoint.Active = true;
                    _tourPointRepository.Update(tourPoint);
                    /*_tourpoints.Remove(tourPoint);
                    _tourpoints.Insert(index, tourPoint);
                    _serializer.ToCSV(FilePath, _tourpoints);*/
                    return;
                }
            }
        }

        public int FindMaxOrder(int idTour)
        {
            int max = 2;
            foreach (TourPoint tourPoint in _tourpoints)
            {
                if (tourPoint.IdTour == idTour && tourPoint.Order > max)
                {
                    max = tourPoint.Order;
                }
            }
            return max;
        }

        public TourPoint Save(TourPoint tourPoint)
        {
            TourPoint savedTourPoint = _tourPointRepository.Save(tourPoint);
            return savedTourPoint;
        }

        public string GetTourPointNameByTourPointId(int idTourPoint)
        {
            foreach(TourPoint tP in _tourPointRepository.GetAll())
            {
                if(idTourPoint==tP.Id)
                {
                    return tP.Name;
                }
            }
            return null;
        }

        public List<TourPoint> GetAllByTourId(int idTour)
        {
            List<TourPoint> points = new List<TourPoint>();
            points= _tourPointRepository.GetAllByTourId(idTour);
            return points;
        }

    }
}
