﻿using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.View;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class TourRequestsViewModel : ViewModelBase
    {
        public static ObservableCollection<TourRequest> TourRequests { get; set; }
        public static ObservableCollection<TourRequest> TourRequestsMainList { get; set; }
        public static ObservableCollection<TourRequest> TourRequestsCopyList { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public TourRequest SelectedTourRequest { get; set; }
        public RequestType Status { get; set; }
        public User LoggedInUser { get; set; }

        public delegate void EventHandler1();

        public delegate void EventHandler2();

        public delegate void EventHandler3();

        public event EventHandler1 CreateTourRequest;

        public event EventHandler2 ViewMoreEvent;

        public event EventHandler3 StatisticsEvent;
        public ICommand CreateTourRequestCommand { get; set; }
        public ICommand ViewTourGalleryCommand { get; set; }

        private readonly IMessageBoxService _messageBoxService;
        private readonly  TourRequestService _tourRequestService;
        public TourRequestsViewModel(User user, TourRequest selectedTourRequest)
        {
            _messageBoxService = new MessageBoxService();
            _tourRequestService = new TourRequestService();
            SelectedTourRequest = selectedTourRequest;
            LoggedInUser =user;
            InitializeProperties(user);
            InitializeCommands();
            foreach (TourRequest tourRequest in TourRequestsMainList)
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Now);
                DateOnly futureDate = today.AddDays(2);

                if (tourRequest.StartDate.CompareTo(futureDate) < 0 && tourRequest.Status!=RequestType.Approved)
                {
                    tourRequest.Status = RequestType.Rejected;
                    _tourRequestService.Update(tourRequest);
                    
                }
                else if (tourRequest.StartDate.CompareTo(futureDate) == 0 && tourRequest.Status!=RequestType.Approved)
                {
                    tourRequest.Status = RequestType.Rejected;
                    _tourRequestService.Update(tourRequest);
                }
                else if(tourRequest.Status == RequestType.Approved)
                {
                    _messageBoxService.ShowMessage("Guide approved "+tourRequest.Id+". request now you can see the choosen start date");
                    MoreDetailsRequest moreDetailsRequest = new MoreDetailsRequest(LoggedInUser, tourRequest);
                    moreDetailsRequest.Show();

                }
            }
            
        }

        private void InitializeCommands()
        {
            CreateTourRequestCommand= new RelayCommand(Execute_CreateTourRequestCommand, CanExecute_Command);
            ViewTourGalleryCommand =  new RelayCommand(Execute_ViewTourGalleryCommand, CanExecute_Command);


        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }


        private void Execute_ViewTourGalleryCommand(object obj)
        {
            if(SelectedTourRequest != null)
            {

                MoreDetailsRequest moreDetailsRequest = new MoreDetailsRequest(LoggedInUser, SelectedTourRequest);
                moreDetailsRequest.Show();
            }
            else
            {
                _messageBoxService.ShowMessage("Choose a tour request which you want to see");
            }
           
        }

        private void Execute_CreateTourRequestCommand(object obj)
        {
            CreateTourRequest?.Invoke();
        }

        private void InitializeProperties(User user)
        {
            LoggedInUser = user;
            TourRequests = new ObservableCollection<TourRequest>(_tourRequestService.GetAll());
            TourRequestsMainList = new ObservableCollection<TourRequest>(_tourRequestService.GetAll());
            TourRequestsCopyList = new ObservableCollection<TourRequest>(_tourRequestService.GetAll());
            Locations = new ObservableCollection<Location>();
        }
    }
}
