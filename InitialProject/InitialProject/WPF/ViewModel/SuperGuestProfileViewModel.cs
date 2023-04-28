using InitialProject.Commands;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class SuperGuestProfileViewModel:ViewModelBase
    {
        public Action CloseAction ;

       public User LogedInUser { get; set; }

        public SuperGuestProfileViewModel(User user)
        {
            LogedInUser = user;
          
            Bonus = Guest1ProfilViewModel.bonusPoints.ToString();
            ReservationNumber = Guest1ProfilViewModel.reservationsLastYear.Count().ToString();
            InitializeCommands();
        }


        private string bonus;
        public string Bonus
        {
            get { return bonus; }
            set
            {
                bonus = value;
                OnPropertyChanged(nameof(Bonus));
            }
        }

        private string reservationNum;
        public string ReservationNumber
        {
            get { return reservationNum; }
            set
            {
                reservationNum = value;
                OnPropertyChanged(nameof(ReservationNumber));
            }
        }
        private bool CanExecute_Command(object parameter)
        {
            return true;
        }


        private RelayCommand close;

        public RelayCommand Close
        {
            get { return close; }
            set
            {
                close = value;
            }
        }

        private void InitializeCommands()
        {
            Close = new RelayCommand(Execute_Close, CanExecute_Command);
    



        }

        private void Execute_Close(object obj)
        {
            CloseAction();
        }

    }
}
