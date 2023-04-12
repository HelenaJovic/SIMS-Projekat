using InitialProject.Repository;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class TourAttendanceService
    {
        private readonly TourAttendanceRepository _tourAttendenceRepository;
        public TourAttendanceService() 
        {
            _tourAttendenceRepository = new TourAttendanceRepository();
        }
        public List<TourAttendance> GetAll()
        {
            return _tourAttendenceRepository.GetAll();
        }
    }
}
