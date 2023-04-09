using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
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
        private readonly VoucherRepository _voucherRepository;
        private readonly VoucherService _voucherService;
        public Action CloseAction { get; set; }

        public ICommand UseVoucherCommand { get; set; }
        public ICommand CancelVoucherCommand { get; set; }

        public TourVouchersViewModel(User user)
        {
            _voucherRepository = new VoucherRepository();
            _voucherService = new VoucherService();
            VouchersMainList = new ObservableCollection<Voucher>(_voucherService.GetUpcomingVouchers(user));
            InitializeCommands();

        }

        private void InitializeCommands()
        {
            UseVoucherCommand = new RelayCommand(Execute_UseVoucherCommand, CanExecute_Command);
            CancelVoucherCommand =  new RelayCommand(Execute_CancelVoucherCommand, CanExecute_Command);
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
                _voucherRepository.Delete(SelectedVoucher);
                CloseAction();
            }
            else
            {
                MessageBox.Show("Choose a voucher which you want to use");
            }
        }
    }
}
