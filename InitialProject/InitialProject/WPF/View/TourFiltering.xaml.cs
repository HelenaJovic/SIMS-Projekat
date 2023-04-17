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
    /// Interaction logic for TourFiltering.xaml
    /// </summary>
    public partial class TourFiltering : Window
    {
        public TourFiltering()
        {
            InitializeComponent();
            TourFilteringViewModel tourFiltering = new TourFilteringViewModel();
            DataContext = tourFiltering;
            if (tourFiltering.CloseAction == null)
                tourFiltering.CloseAction = new Action(this.Close);
        }

        /*
        private void Button_Click_Filter(object sender, RoutedEventArgs e)
        {
            Guest2MainWindowViewModel.ToursMainList.Clear();
            Location location = _locationRepository.FindLocation(Country, City);

            int max = 0;
            if (!(int.TryParse(txtGuestNum.Text, out max) || (txtGuestNum.Text.Equals(""))))
            {
                return;
            }
            foreach (Tour tour in Guest2MainWindowViewModel.ToursCopyList)
            {
                FilterTour(location, max, tour);
            }
            Close();
        }

        private void FilterTour(Location location, int max, Tour tour)
        { 
            if (tour.Language.ToLower().Contains(txtLanguage.Text.ToLower()) && (tour.Location.Country == Country || Country ==null) && (tour.Location.City == City || City == null) && tour.Duration.ToString().ToLower().Contains(txtDuration.Text.ToLower()) &&
                                (tour.MaxGuestNum - max >= 0 || txtGuestNum.Text.Equals("")))
            {
                Guest2MainWindowViewModel.ToursMainList.Add(tour);
            }
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {

            Country = ComboBoxCountry.SelectedItem.ToString();
            Cities = new ObservableCollection<String>(_locationRepository.GetCities(Country));

            ComboBoxCity.IsEnabled = true;
            ComboBoxCity.ItemsSource = Cities;
            ComboBoxCity.SelectedIndex = 0;
            
        }

        private void ComboboxCity_DropDownClosed(object sender, EventArgs e)
        {
            City = ComboBoxCity.SelectedItem.ToString();
        }
        */
    }
}
