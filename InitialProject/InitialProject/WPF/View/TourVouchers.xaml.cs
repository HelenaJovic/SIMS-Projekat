using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace InitialProject.WPF.View
{
    /// <summary>
    /// Interaction logic for TourVouchers.xaml
    /// </summary>
    public partial class TourVouchers : Window
    {
        public static ObservableCollection<Voucher> VouchersMainList { get; set; }
        public Voucher SelectedVoucher { get; set; }
        private readonly VoucherRepository _voucherRepository;
        private readonly VoucherService _voucherService;
        public TourVouchers(User user)
        {
            InitializeComponent();
            DataContext=this;
            _voucherRepository = new VoucherRepository();
            _voucherService = new VoucherService();
            VouchersMainList = new ObservableCollection<Voucher>(_voucherService.GetUpcomingVouchers(user));
        }

        private void Button_Click_UseVoucher(object sender, RoutedEventArgs e)
        {
            if (SelectedVoucher != null)
            {
                _voucherRepository.Delete(SelectedVoucher);
                Close();
            }
            else
            {
                MessageBox.Show("Choose a voucher which you want to use");
            }
        }

        private void Button_Click_CancelVoucher(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
