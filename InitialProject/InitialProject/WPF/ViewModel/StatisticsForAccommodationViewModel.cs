using InitialProject.Applications.DTO;
using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
	public class StatisticsForAccommodationViewModel : ViewModelBase
	{
		public static Accommodation SelectedAccommodation { get; set; }

		public static User LoggedInUser { get; set; }

		public static ObservableCollection<YearlyStatisticsDTO> Statistics { get; set; }

		private readonly AccommodationReservationService accommodationReservationService;
		public StatisticsForAccommodationViewModel(Accommodation selectedAccommodation,User owner )
		{
			SelectedAccommodation = selectedAccommodation;
			LoggedInUser = owner;
			accommodationReservationService = new AccommodationReservationService();
			Statistics = new ObservableCollection<YearlyStatisticsDTO>(accommodationReservationService.GetYearlyStatistics(selectedAccommodation.Id));
		}
	}
}
