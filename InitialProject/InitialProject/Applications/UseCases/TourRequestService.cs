using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using InitialProject.Serializer;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.Applications.UseCases
{
    public class TourRequestService
    {
        private readonly ITourRequestsRepository _tourRequestRepository;

        public TourRequestService()
        {
            _tourRequestRepository = Inject.CreateInstance<ITourRequestsRepository>();
        }

        public List<TourRequest> GetAll()
        {
            return _tourRequestRepository.GetAll();
        }

        public List<TourRequest> GetAllUnaccepted()
        {
            List<TourRequest> requests = new List<TourRequest>();
            foreach (TourRequest request in _tourRequestRepository.GetAll())
            {
                if(request.Status == RequestType.OnHold)
                {
                    requests.Add(request);
                }
            }
            return requests;
        }

        public TourRequest Save(TourRequest tourRequest)
        {
            return _tourRequestRepository.Save(tourRequest);
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            return _tourRequestRepository.Update(tourRequest);
        }

        public List<int> GetAllYears()
        {
            List<int> years = new List<int>();
            foreach (TourRequest t in _tourRequestRepository.GetAll())
            {
                if (!years.Contains(t.StartDate.Year))
                {
                    years.Add(t.StartDate.Year);
                }
            }
            return years;
        }


        public List<string> GetLanguages()
        {
            List<string> languages = new List<string>();
            foreach (TourRequest t in _tourRequestRepository.GetAll())
            {
                if (!languages.Contains(t.TourLanguage.ToLower()))
                {
                    languages.Add(t.TourLanguage.ToLower());
                }
            }
            return languages;
        }

        public Dictionary<int, int> GetTourRequestsPerYear(string location, string language)
        {
            Dictionary<int, int> statistics = new Dictionary<int, int>();

            int startYear = 2019; 
            int endYear = 2023; 

            for (int year = startYear; year <= endYear; year++)
            {
                statistics.Add(year, 0);
            }

            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                int year = tourRequest.StartDate.Year;
                if (AreConditionsTrue(tourRequest, location, language))
                {
                    if (statistics.ContainsKey(year))
                    {
                        statistics[year]++;
                    }
                    else
                    {
                        statistics.Add(year, 1);
                    }
                }
            }
            return statistics;
        }

        public Dictionary<int, int> GetTourRequestsPerMonth(int year, string location, string language)
        {
            Dictionary<int, int> statistics = new Dictionary<int, int>();

            int startMonth = 1;
            int endMonth = 12;

            for (int month = startMonth; month <= endMonth; month++)
            {
                statistics.Add(month, 0);
            }

            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                int month = tourRequest.StartDate.Month;
                if(AreConditionsTrue(tourRequest,location,language) && tourRequest.StartDate.Year == year)
                if (statistics.ContainsKey(month))
                {
                    statistics[month]++;
                }
                else
                {
                    statistics.Add(month, 1);
                }
            }

            return statistics;
        }


        private bool AreConditionsTrue(TourRequest tourRequest, string location, string language)
        {
            string requestLocation = tourRequest.Location.Country.ToString() + " " + tourRequest.Location.City.ToString();
            if(location != null)
            {
                if (!location.Equals(requestLocation))
                {
                    return false;
                }
            }
            if(language!= null)
            {
                if (!language.Equals(tourRequest.TourLanguage.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }

    }
}
