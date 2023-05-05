using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.WPF.View;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class ReserveTourViewModel : ViewModelBase
    {
        public Tour SelectedTour { get; set; }
        public TourReservation SelectedReservation { get; set; }
        public TourReservation TourReservationTmp { get; set; }
        public UserControl CurrentUserControl { get; set; }
        public Tour AlternativeTour { get; set; }
        public User LoggedInUser { get; set; }
        public Action CloseAction { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        private readonly TourService _tourService;
        private readonly TourReservationService _tourReservationService;
        private readonly IMessageBoxService _messageBoxService;
        public ICommand FindTourCommand { get; set; }
        public ICommand CancelTourCommand { get; set; }
        public ICommand UseVoucherCommand { get; set; }
        
        private string _guestNum;

        public string GuestNum
        {
            get => _guestNum;
            set
            {
                if (value != _guestNum)
                {
                    _guestNum = value;
                    OnPropertyChanged();
                }
            }
        }


        public ReserveTourViewModel(Tour selectedTour, TourReservation selectedReservation, User loggedInUser)
        {
            SelectedTour=selectedTour;
            SelectedReservation=selectedReservation;
            LoggedInUser=loggedInUser;
            InitializeCommands();
            _tourService = new TourService();
            _tourReservationService = new TourReservationService();
            _messageBoxService = new MessageBoxService();
        }

        private void InitializeCommands()
        {
            FindTourCommand = new RelayCommand(Execute_FindTourCommand, CanExecute_Command);
            CancelTourCommand =  new RelayCommand(Execute_CancelTourCommand, CanExecute_Command);
            UseVoucherCommand = new RelayCommand(Execute_UseVoucherCommand, CanExecute_Command);
        }

        private void Execute_UseVoucherCommand(object obj)
        {
            if (SelectedReservation != null)
            {
                AlreadyUsedVoucherCheck();
            }
            else
            {
                ChooseVoucher chooseVoucher = new ChooseVoucher(LoggedInUser, SelectedTour, SelectedReservation);
                chooseVoucher.Show();
            }
            
        }

        private void AlreadyUsedVoucherCheck()
        {
            _messageBoxService.ShowMessage("You can't use voucher because you already used it for this reservation!");
        }

        private void Execute_CancelTourCommand(object obj)
        {
            CloseAction();
        }

        private void Execute_FindTourCommand(object obj)
        {
            if (SelectedReservation != null)
            {
                if (GuestNum.Equals(""))
                {
                    return;
                }
                SelectedReservation.FreeSetsNum += SelectedReservation.GuestNum;

                ChangeSelectedReservation();
            }
            else
            {
                if (GuestNum.Equals(""))
                {
                    return;
                }
                ReserveTour();
            }
            CloseAction();
        }

        private void ReserveTour()
        {
            if (SelectedTour.FreeSetsNum - (int.Parse(GuestNum)) >= 0 || GuestNum.Equals(""))
            {
                ReserveSelectedTour(int.Parse(GuestNum));
                CloseAction();
            }
            else
            {
                ReserveAlternativeTour();
            }
        }

        private void ReserveAlternativeTour()
        {
            _messageBoxService.ShowMessage("Find alternative tours because there isn't enaough room for that number of guest");
            if (SelectedReservation != null)
            {
                TourReservationTmp = _tourReservationService.GetTourById(SelectedReservation.Id);
                RemoveFromReservedTours();
            }
            AlternativeTours alternativeTours = new AlternativeTours(LoggedInUser, SelectedTour, TourReservationTmp, GuestNum, AlternativeTour);
            alternativeTours.Show();
            CloseAction();
        }

        private void RemoveFromReservedTours()
        {
            SelectedReservation.FreeSetsNum += SelectedReservation.GuestNum;
            _tourReservationService.Delete(SelectedReservation);
            TourReservationsViewModel.ReservedTours.Remove(SelectedReservation);
        }

        private void ChangeSelectedReservation()
        {
            if (SelectedReservation.FreeSetsNum - (int.Parse(GuestNum)) >= 0 || GuestNum.Equals(""))
            {
                UpdateSelectedReservation(int.Parse(GuestNum));
            }
            else
            {
                ReserveAlternativeTour();
            }
        }

        private void ReserveSelectedTour(int max)
        {     
            SelectedTour.FreeSetsNum -= max;
            string TourName = _tourService.GetTourNameById(SelectedTour.Id);
            TourReservation newReservedTour = new TourReservation(SelectedTour.Id, TourName, LoggedInUser.Id, int.Parse(GuestNum), SelectedTour.FreeSetsNum, -1, LoggedInUser.Username);
            TourReservation savedReservedTour = _tourReservationService.Save(newReservedTour);
            TourReservationsViewModel.ReservedTours.Add(savedReservedTour);
            if (SelectedTour.UsedVoucher==true)
            {
                newReservedTour.UsedVoucher=true;
                _tourReservationService.Update(newReservedTour);
            }
        }

        private void UpdateSelectedReservation(int max)
        {
            SelectedReservation.GuestNum = max;
            SelectedReservation.FreeSetsNum -= max;
            _tourReservationService.Update(SelectedReservation);
            TourReservationsViewModel.ReservedTours.Clear();

            foreach (TourReservation tour in _tourReservationService.GetAllByUser(LoggedInUser))
            {
                TourReservationsViewModel.ReservedTours.Add(tour);
            }
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
