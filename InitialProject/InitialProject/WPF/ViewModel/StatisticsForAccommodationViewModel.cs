using InitialProject.Applications.DTO;
using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace InitialProject.WPF.ViewModel
{
	public class StatisticsForAccommodationViewModel : ViewModelBase
	{
		public static Accommodation SelectedAccommodation { get; set; }

		public static User LoggedInUser { get; set; }


		public static ObservableCollection<int> AvailableYears { get; set; }


		private readonly AccommodationReservationService accommodationReservationService;

		public UserControl CurrentUserControl { get; set; }


		private int _selectedYear;
		public int SelectedYear
		{
			get => _selectedYear;
			set
			{
				_selectedYear = value;
				OnPropertyChanged(nameof(SelectedYear));
				var monthlyStatisticsViewModel = new MonthlyStatisticsUCViewModel(LoggedInUser,SelectedAccommodation,SelectedYear);
				CurrentUserControl.Content=new MonthlyStatisticsUC(LoggedInUser,monthlyStatisticsViewModel);
				
			}
		}



		public StatisticsForAccommodationViewModel(Accommodation selectedAccommodation,User owner )
		{
			
			accommodationReservationService = new AccommodationReservationService();
			var yearlyStatisticsViewModel = new YearlyStatisticsUCViewModel(owner,selectedAccommodation);
			CurrentUserControl = new YearlyStatisticsUC(owner, yearlyStatisticsViewModel);
			InitializeProperties(selectedAccommodation, owner);
		}



		private void InitializeProperties(Accommodation selectedAccommodation, User owner)
		{ 
			AvailableYears = new ObservableCollection<int>(accommodationReservationService.GetYearsForAccommodation(selectedAccommodation.Id));
			SelectedAccommodation = selectedAccommodation;
			LoggedInUser = owner;
		}
	}
}
