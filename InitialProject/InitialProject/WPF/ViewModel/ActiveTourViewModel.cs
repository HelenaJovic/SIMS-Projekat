using InitialProject.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class ActiveTourViewModel : ViewModelBase
    {
        public Action CloseAction { get; set; }
        public ICommand ConfirmAttendenceCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public ActiveTourViewModel() 
        {
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            ConfirmAttendenceCommand = new RelayCommand(Execute_ConfirmAttendenceCommand, CanExecute_Command);
            CancelCommand =  new RelayCommand(Execute_CancelCommand, CanExecute_Command);
        }

        private void Execute_CancelCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_ConfirmAttendenceCommand(object obj)
        {
            throw new NotImplementedException();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
