
using InitialProject.Domain.Model;
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
    /// Interaction logic for Guest1MainWindow.xaml
    /// </summary>
    public partial class Guest1MainWindow : Window
    {
        public static ObservableCollection<Accommodation> Accommodations { get; set; }
        public static ObservableCollection<Accommodation> AccommodationsMainList { get; set; }
        public static ObservableCollection<AccommodationReservation> AccommodationsReservationList { get; set; }

        public static ObservableCollection<Accommodation> AccommodationsCopyList { get; set; }

        public Accommodation SelectedAccommodation{ get; set; }
        public AccommodationReservation SelectedReservation { get; set; }
        public User LoggedInUser { get; set; }
        private readonly AccommodationRepository _accommodationRepository;
        private readonly AccommodationReservationRepository _reservationRepository;
        private readonly LocationRepository _locationRepository;

        



        public Guest1MainWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _accommodationRepository = new AccommodationRepository();
            _locationRepository = new LocationRepository();
            _reservationRepository = new AccommodationReservationRepository();
            AccommodationsMainList = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            AccommodationsCopyList = new ObservableCollection<Accommodation>(_accommodationRepository.GetAll());
            AccommodationsReservationList=new ObservableCollection<AccommodationReservation>(_reservationRepository.GetByUser(user));
            

            BindData();

        }

        private void BindData()
        {
            foreach (Accommodation accommodation in AccommodationsMainList)
            {
                accommodation.Location = _locationRepository.GetById(accommodation.IdLocation);
            }
            foreach(AccommodationReservation accRes in AccommodationsReservationList)
            {
                accRes.Accommodation = _accommodationRepository.GetById(accRes.IdAccommodation);
            }

        }


        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            if (Tab.SelectedIndex == 0)
            {
                if (SelectedAccommodation != null)
                {
                    CreateReservation createReservation = new CreateReservation(SelectedAccommodation, LoggedInUser, SelectedReservation);
                    createReservation.Show();
                }
                else return;
            }
        }

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            FilteringAccommodation filteringAccommodation1 = new FilteringAccommodation();
            filteringAccommodation1.Show();
        }

        private void Restart_Click(object sender, RoutedEventArgs e)
        {
            AccommodationsMainList.Clear();
            foreach (Accommodation a in AccommodationsCopyList)
            {
                a.Location = _locationRepository.GetById(a.IdLocation);
                AccommodationsMainList.Add(a);
            }
        }

		private void ViewGallery_Click(object sender, RoutedEventArgs e)
		{
            ViewAccommodationGallery viewAccommodationGallery = new ViewAccommodationGallery(SelectedAccommodation);
            viewAccommodationGallery.Show();
		}
	}
}
