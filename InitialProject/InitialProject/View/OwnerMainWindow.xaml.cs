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
    /// Interaction logic for OwnerMainWindow.xaml
    /// </summary>
    public partial class OwnerMainWindow : Window
    {
        public static ObservableCollection<Accommodation> Accommodations { get; set; }

        public static Accommodation SelectedAccommodation { get; set; }

        public static User LoggedInUser { get; set; }

        private readonly AccommodationRepository _repository;
        public OwnerMainWindow(User user)
        {
            InitializeComponent();
            DataContext = this;
            LoggedInUser = user;
            _repository = new AccommodationRepository();
            Accommodations = new ObservableCollection<Accommodation>(_repository.GetByUser(user));
            Console.WriteLine("gas");

        }

		private void AddAccommodation_Click(object sender, RoutedEventArgs e)
		{
            CreateAccommodation createAccommodation = new CreateAccommodation(LoggedInUser);
            createAccommodation.Show();

		}

		private void RateGuests_Click(object sender, RoutedEventArgs e)
		{

		}
	}
}
