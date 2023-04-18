using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class GuideProfileViewModel : ViewModelBase
    {
         public User LoggedInUser { get; set; }

        private RelayCommand demission;
        public  RelayCommand DemissionCommand
        {
            get => demission;
            set
            {
                if (value != demission)
                {
                    demission = value;
                    OnPropertyChanged();
                }
            }
         }

        private RelayCommand ratings;
        public RelayCommand YourRatingsCommand
        {
            get => ratings;
            set
            {
                if (value != ratings)
                {
                    ratings = value;
                    OnPropertyChanged();
                }
            }
        }

        private RelayCommand logOut;
        public RelayCommand LogOutCommand
        {
            get => logOut;
            set
            {
                if (value != logOut)
                {
                    logOut = value;
                    OnPropertyChanged();
                }
            }
        }

        public GuideProfileViewModel(User user)
         {
            LoggedInUser = user;
            DemissionCommand = new RelayCommand(Execute_Demission, CanExecute_Command);
            YourRatingsCommand = new RelayCommand(Execute_YourRatings, CanExecute_Command);
            LogOutCommand = new RelayCommand(Execute_LogOut, CanExecute_Command);
         }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }

        private void Execute_LogOut(object obj)
        {
            //
        }

        private void Execute_YourRatings(object obj)
        {
            GuideRatings guideRatings = new GuideRatings(LoggedInUser);
            guideRatings.Show();
        }

        private void Execute_Demission(object obj)
        {
            //
        }
    }
}
