using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class TourStatisticsGuest2ViewModel : ViewModelBase
    {
        public static double TopGuestNum { get; set; }
        public static double TopYearGuestNum { get; set; }
        public List<TourRequest> TourRequests { get; set; }
        public static ObservableCollection<int> Years { get; set; }
        public static User LoggedInUser { get; set; }
        private readonly TourRequestService _tourRequestService;
        public TourStatisticsGuest2ViewModel(User user)
        { 
            LoggedInUser = user;
            _tourRequestService = new TourRequestService();
            Years = new ObservableCollection<int>(GetAllYears());
            TourRequests = new List<TourRequest>(_tourRequestService.GetAll());
            
            TopGuestNum = GetTopGuestNumGeneral();
            TopYearGuestNum = TopGuestNum;
        }

        private double GetTopGuestByYear(int year)
        {
            int sum = 0;
            int brojac = 0;
            foreach (TourRequest tourRequest in TourRequests)
            {
                if (tourRequest.StartDate.Year == year)
                {
                    if (tourRequest.Status.Equals(RequestType.Approved))
                    {
                        sum += tourRequest.MaxGuestNum;
                        brojac++;
                    }
                }
                
            }
            return sum/brojac;
        }

        private double GetTopGuestNumGeneral()
        {
            int sum = 0;
            int brojac = 0;
            foreach (TourRequest tourRequest in TourRequests)
            {
                if(tourRequest.Status.Equals(RequestType.Approved))
                {
                    sum += tourRequest.MaxGuestNum;
                    brojac++;
                }
            }
            return sum/brojac;
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
                    TopYearGuestNum = GetTopGuestByYear(int.Parse(SelectedYear));
                    OnPropertyChanged(nameof(TopYearGuestNum));
                    OnPropertyChanged(nameof(SelectedYear));
                }
            }
        }

        private List<int> GetAllYears()
        {
            List<int> years = new List<int>();
            foreach (TourRequest t in _tourRequestService.GetAll())
            {
                if (!years.Contains(t.StartDate.Year))
                {
                    years.Add(t.StartDate.Year);
                }
            }
            return years;
        }
    }
}
