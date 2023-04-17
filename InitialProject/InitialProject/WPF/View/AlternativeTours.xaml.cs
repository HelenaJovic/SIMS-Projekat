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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for AlternativeTours.xaml
    /// </summary>
    public partial class AlternativeTours : Window
    {
        public AlternativeTours(User user, Tour tour, TourReservation tourReservation, string againGuestNum, Tour alternativeTour)
        {
            InitializeComponent();
            AlternativeToursViewModel alternativeTourViewModel = new AlternativeToursViewModel(user, tour, tourReservation, againGuestNum, alternativeTour);
            DataContext = alternativeTourViewModel;
            if (alternativeTourViewModel.CloseAction == null)
                alternativeTourViewModel.CloseAction = new Action(this.Close);

        }


        /*
        private void Button_Click_ResrveAlternative(object sender, RoutedEventArgs e)
        {
            if (Tab.SelectedIndex == 0)
            {
                if (SelectedAlternativeTour != null)
                {
                    ReserveAlternativeTour();
                }
                else
                {
                    MessageBox.Show("Choose a tour which you can reserve");
                }
            }
            Close();
        }

        private void ReserveAlternativeTour()
        {
            if (SelectedAlternativeTour.FreeSetsNum - int.Parse(AgainGuestNum) >= 0 || AgainGuestNum.Equals(""))
            {
                SelectedAlternativeTour.FreeSetsNum -= int.Parse(AgainGuestNum);
                string TourName = _tourRepository.GetTourNameById(SelectedAlternativeTour.Id);
                TourReservation newAlternativeTour = new TourReservation(SelectedAlternativeTour.Id, TourName, LoggedInUser.Id, int.Parse(AgainGuestNum), SelectedAlternativeTour.FreeSetsNum, -1, LoggedInUser.Username);
                TourReservation savedAlternativeTour = _tourReservationRepository.Save(newAlternativeTour);
                Guest2MainWindowViewModel.ReservedTours.Add(savedAlternativeTour);
            }
        }

        private void Button_Click_FiltersAlternative(object sender, RoutedEventArgs e)
        {
            AlternativeTourFiltering filterAlternativeTour = new AlternativeTourFiltering();
            filterAlternativeTour.Show();
        }

        private void Button_Click_RestartAlternative(object sender, RoutedEventArgs e)
        {
            AlternativeToursMainList.Clear();
            foreach (Tour t in AlternativeToursCopyList)
            {
                AlternativeToursMainList.Add(t);
            }
        }

        private void Button_Click_ViewTourGallery(object sender, RoutedEventArgs e)
        {
            ViewTourGallery viewTourGallery = new ViewTourGallery(SelectedTour);
            viewTourGallery.Show();
        }
        */
    }
}
