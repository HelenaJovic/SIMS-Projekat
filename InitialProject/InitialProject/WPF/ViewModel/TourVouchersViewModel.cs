using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.View;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class TourVouchersViewModel : ViewModelBase
    {
        public static ObservableCollection<Voucher> VouchersMainList { get; set; }
        public Voucher SelectedVoucher { get; set; }
        private readonly VoucherService _voucherService;
        private readonly TourReservationService _tourReservationService;
        private readonly TourService _tourService;
        public Action CloseAction { get; set; }
        public static User LoggedInUser { get; set; }
        public static TourReservation TourReservation { get; set; }
        public ICommand UseVoucherCommand { get; set; }
        public ICommand CancelVoucherCommand { get; set; }
        public ICommand ToursCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ActiveTourCommand { get; set; }
        public ICommand TourAttendenceCommand { get; set; }
        public ICommand CheckNotificationsCommand { get; set; }
        private readonly TourAttendanceService _tourAttendanceService;
        private readonly UserService _userService;

        public TourVouchersViewModel(User user, TourReservation tourReservation)
        {
            _voucherService = new VoucherService();
            _tourReservationService = new TourReservationService();
            _tourAttendanceService= new TourAttendanceService();
            _tourService = new TourService();
            _userService = new UserService();
            LoggedInUser = user;
            TourReservation = tourReservation;
            VouchersMainList = new ObservableCollection<Voucher>(_voucherService.GetUpcomingVouchers(user));
            InitializeCommands();

        }

        private void InitializeCommands()
        {
            UseVoucherCommand = new RelayCommand(Execute_UseVoucherCommand, CanExecute_Command);
            CancelVoucherCommand =  new RelayCommand(Execute_CancelVoucherCommand, CanExecute_Command);
            ToursCommand = new RelayCommand(Execute_ToursCommand, CanExecute_Command);
            VouchersCommand = new RelayCommand(Execute_VouchersCommand, CanExecute_Command);
            ActiveTourCommand =  new RelayCommand(Execute_ActiveTourCommand, CanExecute_Command);
            TourAttendenceCommand = new RelayCommand(Execute_TourAttendenceCommand, CanExecute_Command);
            CheckNotificationsCommand = new RelayCommand(Execute_CheckNotificationsCommand, CanExecute_Command);
        }

        private void Execute_CheckNotificationsCommand(object obj)
        {
            int brojac = 0;
            User user = _userService.GetByUsername(LoggedInUser.Username);
            Tour activ = new Tour();
            GetCurrentActiveTour(ref brojac, ref activ);

            string message = LoggedInUser.Username + " are you present at current active tour " + activ.Name + "?";
            string title = "Confirmation window";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);
            MessageBoxResult(brojac, activ, result);
        }

        private void MessageBoxResult(int brojac, Tour activ, MessageBoxResult result)
        {
            if (result == System.Windows.MessageBoxResult.Yes)
            {
                ActiveTour activeTour = new ActiveTour(LoggedInUser, brojac);
                activeTour.Show();
                CloseAction();
            }
            else
            {
                DeleteFromAttendedTours(activ);

            }
        }

        private void DeleteFromAttendedTours(Tour activ)
        {
            foreach (TourAttendance tA in _tourAttendanceService.GetAllAttendedToursByUser(LoggedInUser))
            {
                if (activ.Id ==  tA.IdTour)
                {
                    _tourAttendanceService.Delete(tA);
                }
            }
        }

        private void GetCurrentActiveTour(ref int brojac, ref Tour activ)
        {
            foreach (TourAttendance tourAttendence in _tourAttendanceService.GetAllAttendedToursByUser(LoggedInUser))
            {
                Tour tour = _tourService.GetById(tourAttendence.IdTour);
                if (tour.Active==true)
                {
                    brojac =1;
                    activ=tour;
                }
            }
        }
        private void Execute_ToursCommand(object obj)
        {

            Guest2MainWindow guest2MainWindow = new Guest2MainWindow(LoggedInUser);
            guest2MainWindow.Show();
            CloseAction();
        }

        private void Execute_TourAttendenceCommand(object obj)
        {

            TourAttendence tourAttendance = new TourAttendence(LoggedInUser);
            tourAttendance.Show();
            CloseAction();
        }

        private void Execute_ActiveTourCommand(object obj)
        {
           
            ActiveTour activeTour = new ActiveTour(LoggedInUser, 0);
            activeTour.Show();
            CloseAction();
            
        }

        private void Execute_VouchersCommand(object obj)
        {

            TourVouchers tourVouchers = new TourVouchers(LoggedInUser, TourReservation);
            tourVouchers.Show();
            CloseAction();
        }

        private void Execute_CancelVoucherCommand(object obj)
        {
            CloseAction();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_UseVoucherCommand(object obj)
        {
            if (TourReservation == null)
            {
                ReserveTourForVoucher();
            }
            else
            {
                CheckVoucher();
            }

        }

        private void ReserveTourForVoucher()
        {
            MessageBox.Show("Choose a reserved tour where you want to use some voucher");
            Guest2MainWindow guest2MainWindow = new Guest2MainWindow(LoggedInUser);
            guest2MainWindow.Show();
            CloseAction();
        }

        private void CheckVoucher()
        {
            if (SelectedVoucher != null)
            {
                TourReservation.UsedVoucher=true;
                _tourReservationService.Update(TourReservation);
                _voucherService.Delete(SelectedVoucher);
                CloseAction();
            }
            else
            {
                MessageBox.Show("Choose a voucher which you want to use");
            }
        }
    }
}
