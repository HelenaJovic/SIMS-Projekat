using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace InitialProject.WPF.ViewModel
{
    public class ViewTourGalleryGuestViewModel : ViewModelBase
    {
        public Tour SelectedTour { get; set; }
        public Action CloseAction { get; set; }
        public static User User { get; set; }
        public ICommand BackCommand { get; set; }
        public ViewTourGalleryGuestViewModel(User user, Tour selectedTour) 
        {
            User = user;
            SelectedTour = selectedTour;
            BackCommand = new RelayCommand(Execute_BackCommand, CanExecute_Command);
            
        }
        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_BackCommand(object obj)
        {
            CloseAction();
        }
    }
}
