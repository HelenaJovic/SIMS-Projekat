using InitialProject.Domain.Model;
using InitialProject.Repository;
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
    /// Interaction logic for AlternativeTourFiltering.xaml
    /// </summary>
    public partial class AlternativeTourFiltering : Window
    {
        public AlternativeTourFiltering()
        {
            InitializeComponent();
            AlternativeTourFilteringViewModel alternativeTourFilteringViewModel = new AlternativeTourFilteringViewModel();
            DataContext = alternativeTourFilteringViewModel;
            if (alternativeTourFilteringViewModel.CloseAction == null)
                alternativeTourFilteringViewModel.CloseAction = new Action(this.Close);


        }

        /*
        private void Button_Click_Filter(object sender, RoutedEventArgs e)
        {
            AlternativeTours.AlternativeToursMainList.Clear();
            Location location = _locationRepository.FindLocation(Country, City);

            int max = 0;
            if (!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))))
            {
                return;
            }
            foreach (Tour tour in AlternativeTours.AlternativeToursCopyList)
            {
                FilterAlternativeTour(location, max, tour);

            }
            Close();
        }

        private void FilterAlternativeTour(Location location, int max, Tour tour)
        {
            if (tour.Language.ToLower().Contains(txtLanguage.Text.ToLower()) && (tour.Location.Country == Country || Country == null) && (tour.Location.City == City || City == null) && tour.Duration.ToString().ToLower().Contains(txtDuration.Text.ToLower()) &&
                                (tour.MaxGuestNum - max >= 0 || txtGuestNum.Text.Equals("")))
            {
                AlternativeTours.AlternativeToursMainList.Add(tour);
            }
        }
*/

    }
}
