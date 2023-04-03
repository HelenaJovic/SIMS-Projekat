using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.Metrics;
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
    /// Interaction logic for Guest2MainWindow.xaml
    /// </summary>
    public partial class Guest2MainWindow : Window
    {
        public static ObservableCollection<Tour> Tours { get; set; }
        public static ObservableCollection<Tour> ToursMainList { get; set; }
        public static ObservableCollection<Tour> ToursCopyList { get; set; }
        public static ObservableCollection<Location> Locations { get; set; }
        public Tour SelectedTour { get; set; }
        public User LoggedInUser { get; set; }

        private readonly TourRepository _tourRepository;
        public List<Tour> tours { get; set; }
        public Guest2MainWindow(User user)
        {
            InitializeComponent();
            DataContext= this;
            LoggedInUser= user;
            _tourRepository= new TourRepository();
            Tours = new ObservableCollection<Tour>(_tourRepository.GetByUser(user));
            ToursMainList = new ObservableCollection<Tour>();
            ToursCopyList = new ObservableCollection<Tour>();
            Locations = new ObservableCollection<Location>();
            string[] lines = File.ReadAllLines("../../../Resources/Data/tours.csv");
            foreach (string line in lines)
            {
                Tour t = new Tour();
                string[] splitted = line.Split("|");
                t.FromCSV(splitted);
                ToursMainList.Add(t);
                ToursCopyList.Add(t);
            }
        }

       
      

        private void Button_Click_Filters(object sender, RoutedEventArgs e)
        {
            AddFilters addFilters = new();
            addFilters.Show();
        }
    }
}
