using InitialProject.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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
    /// Interaction logic for TourPoints.xaml
    /// </summary>
    public partial class TourPoints : Window
    {
        public static ObservableCollection<TourPoint> Points { get; set; }
        
        //public User LoggedInUser { get; set; }
        public Tour SelectedTour { get; set; }
        private readonly TourPointRepository _tourPointRepository;
        private readonly TourRepository _tourRepository;
        public int MaxOrder { get; set; }
        public int Order = 0;

        private bool _active;

        public bool Active
        {
            get => _active;
            set
            {
                if(value != _active)
                {
                    _active = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TourPoints(Tour tour)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = tour;
            _tourPointRepository = new TourPointRepository();
            _tourRepository = new TourRepository();
            Points = new ObservableCollection<TourPoint>(_tourPointRepository.GetAllByTourId(SelectedTour.Id));
            MaxOrder = _tourPointRepository.FindMaxOrder(tour.Id);
        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            Order++;
            if (Order == MaxOrder)
            {
                MessageBox.Show("Tour is done");
                _tourRepository.EndTour(SelectedTour);
                Close();
            }
            else
            {
                foreach (TourPoint point in Points)
                {
                    if (point.Active && !point.GuestAdded)
                    {
                        point.GuestAdded = true;
                        TourGuests tourGuests = new TourGuests(point);
                        tourGuests.Show();
                    }
                }
            }
           
        }

        private void SuddenEnd_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Tour is done");
            _tourRepository.EndTour(SelectedTour);
            Close();
        }
    }
}
