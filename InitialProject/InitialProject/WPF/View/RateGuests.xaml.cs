using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for rateGuests.xaml
    /// </summary>
    public partial class RateGuests : Window
	{
		public static AccommodationReservation SelectedReservation { get; set; }

		public static User LogginUser { get; set; }


		private readonly AccommodationReservationRepository _reservationRepository;


		private readonly GuestReviewRepository _guestReviewRepository;


		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		public RateGuests(User user, AccommodationReservation reservation)
		{
			InitializeComponent();
			DataContext = this;
			SelectedReservation = reservation;
			LogginUser = user;
			_reservationRepository = new AccommodationReservationRepository();
			_guestReviewRepository = new GuestReviewRepository();
			CboGrade1.SelectedIndex = 0;
			CboGrade2.SelectedIndex = 0;
		}

		private string _cleanlinessGrade;
		public string CleanlinessGrade
		{
			get => _cleanlinessGrade;
			set
			{
				if (value != _cleanlinessGrade)
				{
					_cleanlinessGrade = value;
					OnPropertyChanged();
				}
			}
		}

		private string _ruleGrade;
		public string RuleGrade
		{
			get => _ruleGrade;
			set
			{
				if (value != _ruleGrade)
				{
					_ruleGrade = value;
					OnPropertyChanged();
				}
			}
		}

		private string _comment;
		public string Comment1
		{
			get => _comment;
			set
			{
				if (value != _comment)
				{
					_comment = value;
					OnPropertyChanged();
				}
			}
		}



		private void Cancel_Click(object sender, RoutedEventArgs e)
		{
			Close();
		}

		private void Confirm_Click(object sender, RoutedEventArgs e)
		{

			GuestReview newReview = new GuestReview(LogginUser.Id, SelectedReservation.Id, int.Parse(CleanlinessGrade), int.Parse(RuleGrade), Comment1);
			GuestReview savedReview = _guestReviewRepository.Save(newReview);
			OwnerMainWindow.FilteredReservations.Remove(SelectedReservation);
			Close();

		}
	}
}
