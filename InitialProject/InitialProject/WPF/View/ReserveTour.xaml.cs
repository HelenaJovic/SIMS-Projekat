using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.WPF.View;
using InitialProject.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ReserveTour.xaml
    /// </summary>
    public partial class ReserveTour : Window

    { 
        public ReserveTour(Tour tour, TourReservation reserve, User user)
        {
            InitializeComponent();
            ReserveTourViewModel reserveTourViewModel = new ReserveTourViewModel(tour, reserve, user);
            DataContext = reserveTourViewModel;
            if (reserveTourViewModel.CloseAction == null)
                reserveTourViewModel.CloseAction = new Action(this.Close);

        }
        /*
        private void Button_Click_FindTour(object sender, RoutedEventArgs e)
        {
            if (SelectedReservation != null)
            {
                int max = 0;
                if (!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))))
                {
                    return;
                }
                SelectedReservation.FreeSetsNum += SelectedReservation.GuestNum;
                if (SelectedReservation.FreeSetsNum - max >= 0 || txtGuestNum.Text.Equals(""))
                {
                    UpdateSelectedReservation(max);
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
                int max = 0;
                if (!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))))
                {
                    return;
                }
                if (SelectedTour.FreeSetsNum - max >= 0 || txtGuestNum.Text.Equals(""))
                {
                    ReserveSelectedTour(max);
                }
                else
                {
                    MessageBox.Show("Find alternative tours because there isn't enaough room for that number of guest");
                    FindAlternativeTours findAlternative = new FindAlternativeTours(LoggedInUser, SelectedTour, SelectedReservation);
                    findAlternative.Show();
                }
            }
            Close();
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
        }*/

        /*
        private void Button_Click_CancelTour(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
        private void Button_Click_Vouchers(object sender, RoutedEventArgs e)
        {
            TourVouchers tourVouchers = new TourVouchers(LoggedInUser);
            tourVouchers.Show();
        }*/
        

    }
}
