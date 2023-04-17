using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InitialProject.WPF.ViewModel
{
    public class Guest2AccountViewModel:ViewModelBase
    {
        public User LoggedInUser { get; set; }

        public string ImageSource { get; set; }

        public string UserImageSource { get; set; }


        private readonly UserService userService;
        public ICommand ContinueCommand { get; set; }
        public ICommand LogOutCommand { get; set; }
        public Action CloseAction { get; set; }

        public Guest2AccountViewModel(User user) 
        {
            LoggedInUser = user;


            userService = new UserService();
            ContinueCommand = new RelayCommand(Execute_ContinueCommand, CanExecute_Command);
            LogOutCommand =  new RelayCommand(Execute_LogOutCommand, CanExecute_Command);

            SetImagesSource(user);
        }

        private void Execute_LogOutCommand(object obj)
        {
            CloseAction();
        }

        private void Execute_ContinueCommand(object obj)
        {
            Guest2MainWindow guest2MainWindow = new Guest2MainWindow(LoggedInUser);
            guest2MainWindow.Show();
            CloseAction();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        public void SetImagesSource(User user)
        {
            UserImageSource = userService.GetImageUrlByUserId(user.Id);
        }

    }
}
