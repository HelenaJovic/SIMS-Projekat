using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class RateTourViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        public ICommand SubmitCommand { get; set; }
        public ICommand GoSelectTourCommand { get; set; }
        public RateTourViewModel()
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            SubmitCommand = new RelayCommand(Execute_SubmitCommand, CanExecute_Command);
            GoSelectTourCommand =  new RelayCommand(Execute_GoSelectTourCommand, CanExecute_Command);
        }

        private void Execute_GoSelectTourCommand(object obj)
        {
            TourAttendence tourAttendance = new TourAttendence();
            tourAttendance.Show();
        }

        private void Execute_SubmitCommand(object obj)
        {
            CloseAction();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
