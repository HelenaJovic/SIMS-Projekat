using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class TheMostVisitedTourViewModel : ViewModelBase
    {
        public Tour TopTour { get; set; }
        public Tour TopYearTour { get; set; }
        public List<TourAttendance> ToursAttendances { get; set; }
        public List<Tour> Tours { get; set; }
        public static ObservableCollection<int> Years { get; set; }
        public User LoggedInUser { get; set; }

        private readonly ITourAttendanceRepository _tourAttendanceRepository;
        private readonly TourService _tourService;

        public TheMostVisitedTourViewModel(User user)
        {
            _tourAttendanceRepository = Inject.CreateInstance<ITourAttendanceRepository>();
            _tourService = new TourService();
            LoggedInUser= user;
            InitializeProperties();
             
        }


        private String _selectedYear;
        public String SelectedYear
        {
            get { return _selectedYear; }
            set
            {
                if (_selectedYear != value)
                {
                    _selectedYear = value;
                    TopYearTour = GetTopYearTour(int.Parse(SelectedYear));
                    OnPropertyChanged(nameof(TopYearTour));
                    OnPropertyChanged(nameof(SelectedYear));
                }
            }
        }

        void InitializeProperties()
        {
            ToursAttendances = new List<TourAttendance>(_tourAttendanceRepository.GetAllByGuide(LoggedInUser));
            Tours = new List<Tour>(_tourService.GetAllByUser(LoggedInUser));
            TopTour = GetTopTour();
            Years = new ObservableCollection<int>(GetAllYears(LoggedInUser));
            TopYearTour = TopTour;
        }

        public Tour GetTopTour()
        {
            int max = 0;
            int idTour = 0;
            int j = 0;

            foreach(Tour t in Tours)
            {
                j = FindAttendanceNum(j, t);
                if (j>max) 
                {
                    max = j;
                    idTour = t.Id;
                }
            }

            return _tourService.GetById(idTour);
        }

        private Tour GetTopYearTour(int year)
        {
            int max = 0;
            int idTour = 0;
            int j = 0;

            foreach (Tour t in Tours)
            {
                if(t.Date.Year== year)
                {
                    j = FindAttendanceNum(j,t);
                    if (j > max)
                    {
                        max = j;
                        idTour = t.Id;
                    }
                }
            }

            return _tourService.GetById(idTour);
        }

        private int FindAttendanceNum(int j, Tour t)
        {
            foreach (TourAttendance ta in ToursAttendances)
            {
                if (t.Id == ta.IdTour)
                {
                    j++;
                }
            }

            return j;
        }

        private List<int> GetAllYears(User user)
        {
            List<int> years = new List<int>();
            foreach (Tour t in _tourService.GetAll())
            {
                if (!years.Contains(t.Date.Year))
                {
                    years.Add(t.Date.Year);
                }
            }
            return years;
        }

    }
}
