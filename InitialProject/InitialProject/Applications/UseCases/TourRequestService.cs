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

        public TourRequest Save(TourRequest tourRequest)
        {
            return _tourRequestRepository.Save(tourRequest);
        }

        public TourRequest Update(TourRequest tourRequest)
        {
            return _tourRequestRepository.Update(tourRequest);
        }

        public double GetTopAcceptedRequests()
        {
            double n = 0;
            double with = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                n++;
                if (tourRequest.Status.Equals(RequestType.Approved) || tourRequest.Status.Equals(RequestType.ApprovedChecked))
                {
                    with++;
                }
            }
            return CalculateRes(n, with);
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
                    if (tourRequest.Status.Equals(RequestType.Approved) || tourRequest.Status.Equals(RequestType.ApprovedChecked))
                    {
                        with++;
                    }
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
                    if (tourRequest.Status.Equals(RequestType.Rejected) || tourRequest.Status.Equals(RequestType.RejectedCreated))
                    {
                        with++;
                    }
                }
            }
            return CalculateRes(n, with);
        }

        public double GetTopRejectedRequests()
        {
            double n = 0;
            double with = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                n++;
                if (tourRequest.Status.Equals(RequestType.Rejected) || tourRequest.Status.Equals(RequestType.RejectedCreated))
                {
                    with++;
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
            double roundedNumber = Math.Round(res, 2);
            return roundedNumber;
        }

        public double GetTopGuestNumGeneral()
        {
            int sum = 0;
            int brojac = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.Status.Equals(RequestType.Approved) || tourRequest.Status.Equals(RequestType.ApprovedChecked))
                {
                    sum += tourRequest.GuestNum;
                    brojac++;
                }
            }

            if (brojac==0)
            {
                return 0;
            }
            return sum/brojac;
        }

        public double GetTopGuestByYear(int year)
        {
            int sum = 0;
            int brojac = 0;
            foreach (TourRequest tourRequest in _tourRequestRepository.GetAll())
            {
                if (tourRequest.NewStartDate.Year == year)
                {
                    if (tourRequest.Status.Equals(RequestType.Approved) || tourRequest.Status.Equals(RequestType.ApprovedChecked))
                    {
                        sum += tourRequest.GuestNum;
                        brojac++;
                    }
                }

            }
            if (brojac==0)
            {
                return 0;
            }
            return sum/brojac;
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

        public IEnumerable<string> GetAllLanguages()
        {
            List<string> languages = new List<string>();
            foreach (TourRequest t in _tourRequestRepository.GetAll())
            {
                if (!languages.Contains(t.TourLanguage))
                {
                    languages.Add(t.TourLanguage);
                }
            }
            return languages;
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

    }
}
