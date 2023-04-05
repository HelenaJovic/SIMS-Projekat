using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
    interface ITourPointRepository : IRepository<TourPoint>
    {
        List<TourPoint> GetAllByTourId(int idTour);
    }
}
