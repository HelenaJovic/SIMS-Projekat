using InitialProject.Model;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for TourPoints.xaml
    /// </summary>
    public partial class TourPoints : Window
    {
        public static ObservableCollection<TourPoint> Points { get; set; }
        //public User LoggedInUser { get; set; }
        public Tour SelectedTour { get; set; }
        private readonly TourPointRepository _tourPointRepository;

        public TourPoints(Tour tour)
        {
            InitializeComponent();
            DataContext= this;
            SelectedTour= tour;
            _tourPointRepository = new TourPointRepository();
            Points = new ObservableCollection<TourPoint>(_tourPointRepository.GetAllByTourId(SelectedTour));
        }

    }
}
