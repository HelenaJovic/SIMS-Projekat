using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InitialProject.WPF.View
{
    /// <summary>
    /// Interaction logic for CreateTourDEMO.xaml
    /// </summary>
    public partial class CreateTourDEMO : Page, INotifyPropertyChanged
    {
        public bool IsDemoRunning;
        public static ObservableCollection<String> Countries { get; set; }

        private ObservableCollection<String> _cities;
        public ObservableCollection<String> Cities
        {
            get { return _cities; }
            set
            {
                _cities = value;
                OnPropertyChanged(nameof(Cities));
            }
        }

        private String _selectedCity;
        public String SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value;

            }
        }

        private String _selectedCountry;
        public String SelectedCountry
        {
            get { return _selectedCountry; }
            set
            {
                if (_selectedCountry != value)
                {
                    _selectedCountry = value;
                    Cities = new ObservableCollection<String>(_locationService.GetCities(SelectedCountry));
                   
                    OnPropertyChanged(nameof(Cities));
                    OnPropertyChanged(nameof(SelectedCountry));
                }
            }
        }
        private int duration;
        public int Duration
        {
            get => duration;
            set
            {
                if (value != duration)
                {
                    duration = value;
                    OnPropertyChanged();
                }
            }
        }

        private int maxGuestNum;
        public int MaxGuestNum
        {
            get => maxGuestNum;
            set
            {
                if (value != maxGuestNum)
                {
                    maxGuestNum = value;
                    OnPropertyChanged();
                }
            }
        }


        private DateTime _startDate;
        public DateTime Date
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

        private string _name;
        public string TourName
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _description;
        public string Descripiton
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _language;
        public string TourLanguage
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _points;
        public string Points
        {
            get => _points;
            set
            {
                if (value != _points)
                {
                    _points = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand stop;
        public RelayCommand StopCommand
        {
            get => stop;
            set
            {
                if (value != stop)
                {
                    stop = value;
                    OnPropertyChanged();
                }

            }
        }



        private readonly LocationService _locationService;

        public delegate void EventHandler1();

        public event EventHandler1 EndDemoEvent;

        public CreateTourDEMO()
        {
            InitializeComponent();
            DataContext= this;
            _locationService = new LocationService();
            Countries = new ObservableCollection<String>(_locationService.GetAllCountries());
            Cities = new ObservableCollection<String>();
            Date = DateTime.Now;
            StopCommand = new RelayCommand(Execute_Stop, CanExecute_Command);
            StartDemo();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_Stop(object obj)
        {
            EndDemoEvent?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
        public void StopDemo()
        {
            IsDemoRunning = false;

        }

        public void StartDemo()
        {
            IsDemoRunning = true;
            EnterName();
        }

        private void ResetView()
        {
            TourName = string.Empty;
            ComboBoxCountry.SelectedIndex = -1;
            ComboBoxCity.SelectedIndex = -1;
            Descripiton = string.Empty;
            TourLanguage = string.Empty;
            MaxGuestNum = 0;
            Points = string.Empty;
            ComboBoxTime.SelectedIndex = -1;
            Duration = 0;

        }

        private async void EnterName()
        {
            await Task.Delay(1000);
            TourName = "Diznilend";
            await Task.Delay(2000);

            if (IsDemoRunning)
            {
               SelectCountry();
            }
        }

        private async void SelectCountry()
        {
            ComboBoxCountry.SelectedIndex = 0;
            await Task.Delay(2000);

            if (IsDemoRunning)
            {
                SelectCity();
            }
        }

        private async void SelectCity()
        {
            ComboBoxCity.SelectedIndex = 0;
            await Task.Delay(2000);

            if (IsDemoRunning)
            {
                EnterDescription();
            }
        }

        private async void EnterDescription()
        {
            Descripiton = "decja tura";
            await Task.Delay(2000);

            if (IsDemoRunning)
            {
                EnterLanguage();
            }
        }

        private async void EnterLanguage()
        {
            TourLanguage = "srpski";
            await Task.Delay(2000);

            if (IsDemoRunning)
            {
                EnterMaxGuestNum();
            }
        }

        private async void EnterMaxGuestNum()
        {
            MaxGuestNum = 35;
            await Task.Delay(2000);

            if (IsDemoRunning)
            {
                EnterPoints();
            }
        }

        private async void EnterPoints()
        {
            Points = "Ringispil,Plovidba Senom";
            await Task.Delay(2000);

            if (IsDemoRunning)
            {
                EnterDate();
            }
        }

        private async void EnterDate()
        {
            Date = new DateTime(2023, 6, 15);
            await Task.Delay(2000);

            if (IsDemoRunning)
            {
                EnterStartTime();
            }
           
        }

        private async void EnterStartTime()
        {
            ComboBoxTime.SelectedIndex= 0;
            await Task.Delay(2000);

            if (IsDemoRunning)
            {
                EnterDuration();
            }
        }

        private async void EnterDuration()
        {
            Duration = 1;
            await Task.Delay(2000);

            if (IsDemoRunning)
            {
                ResetView();
                await Task.Delay(1000);
                EnterName();
            }
        }


    }
}
