﻿using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.WPF.ViewModel;
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
        
        public TourVouchers(User user)
        {
            InitializeComponent();
            TourVouchersViewModel tourVouchersViewModel = new TourVouchersViewModel(user);
            DataContext=tourVouchersViewModel;
            if (tourVouchersViewModel.CloseAction == null)
                tourVouchersViewModel.CloseAction = new Action(this.Close);

        }
        /*
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
        */
    }
}
