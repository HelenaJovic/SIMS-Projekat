using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.WPF.ViewModel;
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
    /// Interaction logic for AddDate.xaml
    /// </summary>
    public partial class AddDate : Window, INotifyPropertyChanged
    {
        public Tour SelectedTour;
        private readonly TourService _tourService;
        private TimeOnly startTime;
        
        public AddDate(Tour tour)
        {
            InitializeComponent();
            this.DataContext = new AddDateViewModel();

            this.Height = 400;
            this.Width = 600;

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


        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

            Tour newTour = new Tour(SelectedTour.Name, SelectedTour.Location, SelectedTour.Language, SelectedTour.MaxGuestNum, DateOnly.Parse(Date) , startTime, SelectedTour.Duration, SelectedTour.MaxGuestNum, false, SelectedTour.IdUser, SelectedTour.IdLocation);
            Tour savedTour = _tourService.Save(newTour);
            GuideMainWindow.Tours.Add(savedTour);
            Close();
        }
    }
}
