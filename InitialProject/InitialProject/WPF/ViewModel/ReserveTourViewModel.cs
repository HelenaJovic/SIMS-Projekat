﻿using InitialProject.Commands;
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
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class ReserveTourViewModel : ViewModelBase
    {
        public Tour SelectedTour { get; set; }
        public TourReservation SelectedReservation { get; set; }

        public Tour AlternativeTour { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        private readonly TourReservationRepository _tourReservationRepository;
        private readonly TourRepository _tourRepository;

        public ICommand FindTourCommand { get; set; }
        public ICommand CancelTourCommand { get; set; }
        public ICommand UseVoucherCommand { get; set; }
        public User LoggedInUser { get; set; }
        public Action CloseAction { get; set; }  //kako da uradim close windowa nekog

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

        public event PropertyChangedEventHandler? PropertyChanged;

        public ReserveTourViewModel(Tour selectedTour, TourReservation selectedReservation, User loggedInUser)
        {
            SelectedTour=selectedTour;
            SelectedReservation=selectedReservation;
            LoggedInUser=loggedInUser;
            InitializeCommands();
            _tourReservationRepository = new TourReservationRepository();
            _tourRepository = new TourRepository();
        }

        private void InitializeCommands()
        {
            FindTourCommand = new RelayCommand(Execute_FindTourCommand, CanExecute_Command);
            CancelTourCommand =  new RelayCommand(Execute_CancelTourCommand, CanExecute_Command);
            UseVoucherCommand = new RelayCommand(Execute_UseVoucherCommand, CanExecute_Command);
        }

        private void Execute_UseVoucherCommand(object obj)
        {
            TourVouchers tourVouchers = new TourVouchers(LoggedInUser);
            tourVouchers.Show();
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
                if (SelectedReservation.FreeSetsNum - (int.Parse(GuestNum)) >= 0 || GuestNum.Equals(""))
                {
                    UpdateSelectedReservation(int.Parse(GuestNum));
                }
                else
                {

                    MessageBox.Show("Find alternative tours because there isn't enaough room for that number of guest");
                    FindAlternativeTours findAlternative = new FindAlternativeTours(LoggedInUser, SelectedTour, SelectedReservation);
                    findAlternative.Show();
                }
            }
            else
            {
                if (GuestNum.Equals(""))
                {
                    return;
                }
                if (SelectedTour.FreeSetsNum - (int.Parse(GuestNum)) >= 0 || GuestNum.Equals(""))
                {
                    ReserveSelectedTour(int.Parse(GuestNum));
                }
                else
                {
                    MessageBox.Show("Find alternative tours because there isn't enaough room for that number of guest");
                    FindAlternativeTours findAlternative = new FindAlternativeTours(LoggedInUser, SelectedTour, SelectedReservation);
                    findAlternative.Show();
                }
            }
            CloseAction();
        }

        private void ReserveSelectedTour(int max)
        {
            SelectedTour.FreeSetsNum -= max;
            string TourName = _tourRepository.GetTourNameById(SelectedTour.Id);
            TourReservation newReservedTour = new TourReservation(SelectedTour.Id, TourName, LoggedInUser.Id, int.Parse(GuestNum), SelectedTour.FreeSetsNum, -1, LoggedInUser.Username);

            TourReservation savedReservedTour = _tourReservationRepository.Save(newReservedTour);
            Guest2MainWindowViewModel.ReservedTours.Add(savedReservedTour);
        }

        private void UpdateSelectedReservation(int max)
        {
            SelectedReservation.GuestNum = max;
            SelectedReservation.FreeSetsNum -= max;
            _tourReservationRepository.Update(SelectedReservation);
            Guest2MainWindowViewModel.ReservedTours.Clear();

            foreach (TourReservation tour in _tourReservationRepository.GetAll())
            {
                Guest2MainWindowViewModel.ReservedTours.Add(tour);
            }
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}