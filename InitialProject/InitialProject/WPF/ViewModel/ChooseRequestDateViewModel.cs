using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace InitialProject.WPF.ViewModel
{
    public class ChooseRequestDateViewModel : ViewModelBase
    {

        private RelayCommand createRequest;
        public RelayCommand CreateRequestCommand
        {
            get => createRequest;
            set
            {
                if (value != createRequest)
                {
                    createRequest = value;
                    OnPropertyChanged();
                }

            }
        }
        
        public Tour Tour
        {
            get { return tour; }
            set
            {
                tour = value;
                OnPropertyChanged("Tour");
            }
        }

        private string _startTime;
        public string StartTime
        {
            get => _startTime;
            set
            {
                if (value != _startTime)
                {
                    _startTime = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _date;
        public string Date
        {
            get => _date;
            set
            {
                if (value != _date)
                {
                    _date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        private int _duration;
        public int Duration
        {
            get => _duration;
            set
            {
                if (value != _duration)
                {
                    _duration = value;
                    OnPropertyChanged("Duration");
                }
            }
        }




        public TourRequest SelectedRequest { get; set; }
        private readonly TourService _tourService;
        private readonly TourRequestService _tourRequestService;
        private readonly MessageBoxService _messageBoxService;
        public User LoggedInUser { get; set; }
        public Tour tour = new Tour();

        public DateTime StartInterval { get; set; }
        public DateTime EndInterval { get; set; }

        public delegate void EventHandler1();
        public event EventHandler1 EndCreatingRequestEvent;

        public ChooseRequestDateViewModel(User user, TourRequest tourRequest)
        {
            LoggedInUser= user;
            CreateRequestCommand = new RelayCommand(Execute_CreateRequest, CanExecute_Command);
            SelectedRequest= tourRequest;
            _tourService = new TourService();
            _tourRequestService = new TourRequestService();
            _messageBoxService = new MessageBoxService();

            StartInterval = new DateTime(SelectedRequest.NewStartDate.Year, SelectedRequest.NewStartDate.Month, SelectedRequest.NewStartDate.Day);
            EndInterval = new DateTime(SelectedRequest.NewEndDate.Year, SelectedRequest.NewEndDate.Month, SelectedRequest.NewEndDate.Day);
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_CreateRequest(object obj)
        {
            Tour.Location = SelectedRequest.Location;
            Tour.Language = SelectedRequest.TourLanguage;
            Tour.Descripiton = SelectedRequest.Description;

            Tour.Validate();


            if (Tour.IsValid)
            {
                if(!_tourService.IsUserFree(LoggedInUser, DateOnly.Parse(Date)))
                {
                    _messageBoxService.ShowMessage("You are not available at this date, try new date");
                }
                
                TimeOnly _startTime = ConvertTime(StartTime);
                Tour newTour = new Tour(Tour.Name, Tour.Location, Tour.Language, Tour.MaxGuestNum, DateOnly.Parse(Date), _startTime, Duration, Tour.MaxGuestNum, false, LoggedInUser.Id, Tour.Location.Id, false); ;

                Tour savedTour = _tourService.Save(newTour);
                GuideMainWindowViewModel.Tours.Add(newTour);

                SelectedRequest.Status = RequestType.Approved;
                _tourRequestService.Update(SelectedRequest);
                AcceptTourRequestViewModel.Requests.Remove(SelectedRequest);
                AcceptTourRequestViewModel.RequestsCopyList.Remove(SelectedRequest);

                /*CreatePoints(savedTour);
                CreateImages(savedTour);*/

                EndCreatingRequestEvent?.Invoke();
            }
            else
            {
                OnPropertyChanged(nameof(Tour));
            }

        }

        public TimeOnly ConvertTime(string times)
        {
            StartTimes time = (StartTimes)Enum.Parse(typeof(StartTimes), times);
            TimeOnly startTime;
            switch (time)
            {
                case StartTimes._8AM:
                    startTime = new TimeOnly(8, 0);
                    break;
                case StartTimes._10AM:
                    startTime = new TimeOnly(10, 0);
                    break;
                case StartTimes._12PM:
                    startTime = new TimeOnly(12, 0);
                    break;
                case StartTimes._2PM:
                    startTime = new TimeOnly(14, 0);
                    break;
                case StartTimes._4PM:
                    startTime = new TimeOnly(16, 0);
                    break;
                case StartTimes._6PM:
                    startTime = new TimeOnly(18, 0);

                    break;
            }
            return startTime;
        }
    }
}
