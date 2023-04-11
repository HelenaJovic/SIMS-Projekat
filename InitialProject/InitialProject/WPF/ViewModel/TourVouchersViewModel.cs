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
        public Action CloseAction { get; set; }
        public static User LoggedInUser { get; set; }

        public ICommand UseVoucherCommand { get; set; }
        public ICommand CancelVoucherCommand { get; set; }
        public ICommand ToursCommand { get; set; }
        public ICommand VouchersCommand { get; set; }
        public ICommand ActiveTourCommand { get; set; }
        public ICommand TourAttendenceCommand { get; set; }

        public TourVouchersViewModel(User user)
        {
            _voucherService = new VoucherService();
            LoggedInUser = user;
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
            
            ActiveTour activeTour = new ActiveTour(LoggedInUser);
            activeTour.Show();
            CloseAction();
        }

        private void Execute_VouchersCommand(object obj)
        {
            
            TourVouchers tourVouchers = new TourVouchers(LoggedInUser);
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
            if (SelectedVoucher != null)
            {
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
