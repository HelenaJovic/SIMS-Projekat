using InitialProject.Commands;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class TourAttendenceViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        public ICommand RateTourCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public TourAttendenceViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            RateTourCommand = new RelayCommand(Execute_RateTourCommand, CanExecute_Command);
            CancelCommand =  new RelayCommand(Execute_CancelCommand, CanExecute_Command);
        }

        private void Execute_CancelCommand(object obj)
        {
            CloseAction();
        }

        private void Execute_RateTourCommand(object obj)
        {
            RateTour rateTour = new RateTour();
            rateTour.Show();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
