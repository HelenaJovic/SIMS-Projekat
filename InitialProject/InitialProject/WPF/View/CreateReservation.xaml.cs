using InitialProject.Domain.Model;
using InitialProject.Repository;
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
using System.Windows.Shapes;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for CreateReservation.xaml
    /// </summary>
    public partial class CreateReservation : Window
    {
        public Accommodation SelectedAccommodation;
        public AccommodationReservation accommodationReservation;
        public List<DateOnly> StartDates;
        public List<DateOnly> EndDates;
        public List<DateOnly> BetweenDates;
        private DateOnly startDate1;
        private DateOnly endDate1;
        private int minReservation = 0;



        private readonly AccommodationReservationRepository _accommodationReservationRepository;
        private readonly AccommodationRepository _accommodationRepository;
        public User LoggedInUser { get; set; }
        public CreateReservation(Accommodation selectedAccommodation, User user, AccommodationReservation selectedReservation)
        {
            InitializeComponent();
            DataContext = this;
            StartDates = new List<DateOnly>();
            EndDates = new List<DateOnly>();
            BetweenDates = new List<DateOnly>();
            _accommodationReservationRepository = new AccommodationReservationRepository();
            accommodationReservation = selectedReservation;
            SelectedAccommodation = selectedAccommodation;
            LoggedInUser = user;
            _accommodationReservationRepository = new AccommodationReservationRepository();
            _accommodationRepository = new AccommodationRepository();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string _startDate { get; set; }
        public string _endDate { get; set; }

        private string _minDaysReservation { get; set; }

        public string StartDate
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
        public string EndDate
        {
            get => _endDate;
            set
            {
                if (value != _endDate)
                {
                    _endDate = value;
                    OnPropertyChanged();
                }
            }
        }

        public string DaysNum
        {
            get => _minDaysReservation;
            set
            {
                if (value != _minDaysReservation)
                {
                    _minDaysReservation = value;
                    OnPropertyChanged();
                }
            }
        }


        private void Reserve_Click(object sender, RoutedEventArgs e)
        {
            StartDates.Clear();
            EndDates.Clear();
            BetweenDates.Clear();
            
            AccommodationReservation newReservation = new(LoggedInUser, LoggedInUser.Id, SelectedAccommodation, SelectedAccommodation.Id, DateOnly.Parse(StartDate), DateOnly.Parse(EndDate), int.Parse(DaysNum));
            AccommodationReservation savedReservation = _accommodationReservationRepository.Save(newReservation);
            Guest1MainWindow.AccommodationsReservationList.Add(savedReservation);
            Close();


        }


        private void CancelCreate_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void CheckAvailableDate_Click(object sender, RoutedEventArgs e)
        {


            if (!(int.TryParse(TxtDaysNum.Text, out minReservation) || (TxtDaysNum.Text.Equals(""))))
            {

                return;
            }

            if ((minReservation - SelectedAccommodation.MinReservationDays) >= 0)
            {

            }
            else
            {
                MessageBox.Show("Prekrsili ste broj minimalnih dana za rezervaciju.");
                return;

            }



            StartDates.Clear();
            EndDates.Clear();

            startDate1 = DateOnly.FromDateTime((DateTime)startDate.SelectedDate);
            endDate1 = DateOnly.FromDateTime((DateTime)endDate.SelectedDate);
            StartDates = _accommodationReservationRepository.GetAllStartDates(SelectedAccommodation.Id);
            EndDates = _accommodationReservationRepository.GetAllEndDates(SelectedAccommodation.Id);
            List<DateOnly> freeDates = GetFreeDates(startDate1, endDate1, StartDates, EndDates, minReservation);

            BetweenDates.Clear();
            for (DateOnly date = startDate1; date <= endDate1; date = date.AddDays(1))
            {
                BetweenDates.Add(date);
            }

            GetDateByCondition(freeDates);
        }

        private void GetDateByCondition(List<DateOnly> freeDates)
        {
            if (freeDates.Count == 0 && StartDates.Count != 0 && EndDates.Count != 0)
            {
                IsEnabled = true;
                MessageBox.Show("Nema slobodnih datuma u vasem opsegu. Alternative su: ");
                AlternativeDates(endDate1, StartDates, EndDates, minReservation);
                return;
            }

            else if (freeDates.Count == 0 && StartDates.Count == 0)
            {
                TxtNumGuest.IsEnabled = true;
                MessageBox.Show("Mozete izabrati koji god zelite datum u ovom opsegu");
            }
            else
            {

                TxtNumGuest.IsEnabled = true;
                string message = "";
                foreach (DateOnly item in freeDates)
                {
                    message += item + "\n";
                }

                MessageBox.Show(message, "Free Dates");

                freeDates.Clear();
            }
        }

        public void AlternativeDates(DateOnly endDate, List<DateOnly> startDates, List<DateOnly> endDates, int numDays)
        {
            List<DateOnly> alternativeFreeDates = new List<DateOnly>();
            alternativeFreeDates = GetFreeDates(endDate, endDate.AddDays(30), startDates, endDates, numDays);
            string message = "";
            int i = 0;
            foreach (DateOnly item in alternativeFreeDates)
            {
                if(i < numDays)
                {
                    message += item + "\n";
                    i++;
                }
                else
                {
                    MessageBox.Show(message, "Alternative days");
                    return;
                }
            }


        }
    
        public List<DateOnly> GetFreeDates(DateOnly startDate, DateOnly endDate, List<DateOnly> startDates, List<DateOnly> endDates, int numDays)
        {
            List<DateOnly> freeDates = new List<DateOnly>();
            if (startDates.Count!=0)
            {
                
                DateOnly earliestStartDate = startDate < startDates.Min() ? startDate : startDates.Min();
                DateOnly latestEndDate = endDate > endDates.Max() ? endDate : endDates.Max();

                for (DateOnly currentDate = earliestStartDate; currentDate <= latestEndDate; currentDate = currentDate.AddDays(1))
                {
                    bool isBooked = false;

                    isBooked = GetSequenceFreeDates(startDates, endDates, numDays, currentDate, isBooked);

                    if (!isBooked)
                    {
                        freeDates.Add(currentDate);
                    }
                }
            }
            return freeDates;



        }

        private static bool GetSequenceFreeDates(List<DateOnly> startDates, List<DateOnly> endDates, int numDays, DateOnly currentDate, bool isBooked)
        {
            for (int i = 0; i < startDates.Count; i++)
            {
                DateOnly bookedStartDate = startDates[i];
                DateOnly bookedEndDate = endDates[i];

                if (currentDate >= bookedStartDate && currentDate <= bookedEndDate)
                {
                    isBooked = true;
                    break;
                }

                if (currentDate.AddDays(numDays) >= bookedStartDate && currentDate.AddDays(numDays) <= bookedEndDate)
                {
                    isBooked = true;
                    break;
                }
            }

            return isBooked;
        }

        private void CheckGuestNumber_Click(object sender, RoutedEventArgs e)
        {
            int maxGuest = 0;
            if (!(int.TryParse(TxtNumGuest.Text, out maxGuest) || (TxtNumGuest.Text.Equals(""))))
            {
                return;
            }

            if ((maxGuest - SelectedAccommodation.MaxGuestNum) <= 0 && minReservation == BetweenDates.Count())
            {
                BlockedButton.IsEnabled = true;
            }
            else
            {
                BlockedButton.IsEnabled = false;
                MessageBox.Show("Prekrsili ste maksimalan broj gostiju za rezervaciju ili pokusavate da rezervisete razlicit broj dana od navedenog!");
            }

            
        }
        private void TextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Subtract || e.Key == Key.OemMinus)
            {
                e.Handled = true; // Prevent hyphen from being entered
            }
            else if (e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                {
                    textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next)); // Move focus to next TextBox
                }
            }
        }


        private void DatePicker_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var datePicker = sender as DatePicker;
                if (datePicker != null)
                {
                    datePicker.IsDropDownOpen = true; // Open the DatePicker popup
                    e.Handled = true; // Mark the event as handled
                }
            }
        }


    }
}

