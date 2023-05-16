using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
using InitialProject.Serializer;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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
                if (!years.Contains(t.NewStartDate.Year))
                {
                    years.Add(t.NewStartDate.Year);
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
                int year = tourRequest.NewStartDate.Year;
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
                int month = tourRequest.NewStartDate.Month;
                if(AreConditionsTrue(tourRequest,location,language) && tourRequest.NewStartDate.Year == year)
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
        public double GetTopAcceptedRequests()
        {
            double n = 0;
            double with = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                n++;
                with=ApprovedTourRequests(with, tourRequest);
            }
            return CalculateRes(n, with);
        }

        private static double ApprovedTourRequests(double with, TourRequest tourRequest)
        {
            if (tourRequest.Status.Equals(RequestType.Approved) || tourRequest.Status.Equals(RequestType.ApprovedChecked))
            {
                with++;
            }

            return with;
        }

        public double GetTopYearAcceptedRequests(int year)
        {
            double n = 0;
            double with = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.NewStartDate.Year == year)
                {
                    n++;
                    with=ApprovedTourRequests(with, tourRequest);
                }
            }
            return CalculateRes(n, with);
        }

        public double GetTopYearRejectedRequests(int year)
        {
            double n = 0;
            double with = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.NewStartDate.Year == year)
                {
                    n++;
                    with=RejectedTourRequests(with, tourRequest);
                }
            }
            return CalculateRes(n, with);
        }

        private static double RejectedTourRequests(double with, TourRequest tourRequest)
        {
            if (tourRequest.Status.Equals(RequestType.Rejected) || tourRequest.Status.Equals(RequestType.RejectedCreated))
            {
                with++;
            }

            return with;
        }

        public double GetTopRejectedRequests()
        {
            double n = 0;
            double with = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                n++;
                with=RejectedTourRequests(with, tourRequest);
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
            double roundedNumber = Math.Round(res, 2);
            return roundedNumber;
        }

        public double GetTopGuestNumGeneral()
        {
            int sum = 0;
            int brojac = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                ApprovedTourRequestGuestNum(ref sum, ref brojac, tourRequest);
            }

            if (brojac==0)
            {
                return 0;
            }
            return sum/brojac;
        }

        private static void ApprovedTourRequestGuestNum(ref int sum, ref int brojac, TourRequest tourRequest)
        {
            if (tourRequest.Status.Equals(RequestType.Approved) || tourRequest.Status.Equals(RequestType.ApprovedChecked))
            {
                sum += tourRequest.GuestNum;
                brojac++;
            }
        }

        public double GetTopGuestByYear(int year)
        {
            int sum = 0;
            int brojac = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.NewStartDate.Year == year)
                {
                    ApprovedTourRequestGuestNum(ref sum, ref brojac, tourRequest);
                }

            }
            if (brojac==0)
            {
                return 0;
            }
            return sum/brojac;
        }

       

        public int GetLanguageGuestNum(string language)
        {
            int brojac = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if(tourRequest.Status.Equals(RequestType.Approved) || tourRequest.Status.Equals(RequestType.ApprovedChecked))
                {
                    if (tourRequest.TourLanguage == language)
                    {
                        brojac++;
                    }
                }
                

            }
            return brojac;
        }

        public int GetLocationGuestNum(string country, string city)
        {
            int brojac = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.Status.Equals(RequestType.Approved) || tourRequest.Status.Equals(RequestType.ApprovedChecked))
                {
                    if (tourRequest.Location.Country==country && tourRequest.Location.City==city)
                    {
                        brojac++;
                    }
                }

            }
            return brojac;
        }

        public List<TourRequest> GetAcceptedRequests()
        {
            List<TourRequest> requestList = new List<TourRequest>();
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if(tourRequest.Status.Equals(RequestType.Approved) || tourRequest.Status.Equals(RequestType.ApprovedChecked))
                {
                    requestList.Add(tourRequest);
                }
            }

            return requestList;
        }

        public TourRequest GetById(int id)
        {
            return _tourRequestRepository.GetById(id);
        }

    }
}
