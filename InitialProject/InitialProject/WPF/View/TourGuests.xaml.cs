using InitialProject.Domain.Model;
using InitialProject.Forms;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourGuests.xaml
    /// </summary>
    public partial class TourGuests : Window
    {
        public static ObservableCollection<TourReservation> Users { get; set; }
        private TourReservationRepository _tourReservationRepository;
        private UserRepository _userRepository;
        
        public TourReservation SelectedUser { get; set; }
        public TourPoint CurrentPoint { get; set; }
        public TourGuests(TourPoint tourPoint)
        {
            InitializeComponent();
            DataContext = this;
            _tourReservationRepository = new TourReservationRepository();
            _userRepository = new UserRepository();
            CurrentPoint = tourPoint;
            Users = new ObservableCollection<TourReservation>(_tourReservationRepository.GetByTour(CurrentPoint.IdTour));
        }

        private void AddGuest_Click(object sender, RoutedEventArgs e)
        {
            User user = _userRepository.GetByUsername(SelectedUser.UserName);
            CurrentPoint.Guests.Add(user);
            string message = SelectedUser.UserName + " are you present at tourpoint " + CurrentPoint.Name;
            string title = "Confirmation window";
            MessageBoxButton buttons = MessageBoxButton.YesNo;
            MessageBoxResult result = MessageBox.Show(message, title, buttons);
            if (result == MessageBoxResult.Yes)
            {
                _tourReservationRepository.Delete(SelectedUser);
                Users.Remove(SelectedUser);
            }
             
        }

        private void DoneAddingGuest_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
