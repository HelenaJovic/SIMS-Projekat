using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.ComponentModel;

namespace InitialProject.WPF.ViewModel
{
    class AddDateViewModel : ViewModelBase
    {
      /*  public Tour SelectedTour;
        private readonly TourService _tourService;
        private TimeOnly startTime;

        public AddDateViewModel()
        {
            //SelectedTour = tour;
            _tourService = new TourService();

        }

        private string _startDate;
        public string Date
        {
            get => _startDate;
            set
            {
                if (value != _startDate)
                {
                    _startDate = value;
                    OnPropertyChanged();
                }
            }
        }
        private string _startTime;
        public string StartTime
        {
            get => _startTime;
            set
            {
                if (value != _startTime)
                {
                    _startTime = value;
                    OnPropertyChanged();
                }
            }
        }


        private void AddTour_Click(object sender, RoutedEventArgs e)
        {
            switch (ComboBoxTime.SelectedIndex)
            {
                case 0:
                    startTime = new TimeOnly(8, 0);
                    break;
                case 1:
                    startTime = new TimeOnly(10, 0);
                    break;
                case 2:
                    startTime = new TimeOnly(12, 0);
                    break;
                case 3:
                    startTime = new TimeOnly(14, 0);
                    break;
                case 4:
                    startTime = new TimeOnly(16, 0);
                    break;
                case 5:
                    startTime = new TimeOnly(18, 0);
                    break;
            }

            Tour newTour = new Tour(SelectedTour.Name, SelectedTour.Location, SelectedTour.Language, SelectedTour.MaxGuestNum, DateOnly.Parse(Date), startTime, SelectedTour.Duration, SelectedTour.MaxGuestNum, false, SelectedTour.IdUser, SelectedTour.IdLocation);
            Tour savedTour = _tourService.Save(newTour);
            GuideMainWindow.Tours.Add(savedTour);
           
        }*/

    }
}
