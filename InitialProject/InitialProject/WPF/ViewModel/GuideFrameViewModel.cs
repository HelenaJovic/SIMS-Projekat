using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.WPF.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InitialProject.WPF.ViewModel
{
    class GuideFrameViewModel : ViewModelBase
    {
        private RelayCommand menu;
        public RelayCommand MenuBarCommand
        {
            get => menu;
            set
            {
                if (value != menu)
                {
                    menu = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand user;
        public RelayCommand UserCommand
        {
            get => user;
            set
            {
                if (value != user)
                {
                    user = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand home;
        public RelayCommand HomeCommand
        {
            get => home;
            set
            {
                if (value != home)
                {
                    home = value;
                    OnPropertyChanged();
                }

            }
        }

        private RelayCommand demo;
        public RelayCommand DemoCommand
        {
            get => demo;
            set
            {
                if (value != demo)
                {
                    demo = value;
                    OnPropertyChanged();
                }

            }
        }

        public User LoggedInUser { get; set; }
        private Page frame { get; set; }
        public Page FrameContent
        {
              get { return frame; }
              set
              {
                  if (frame != value)
                  {
                    frame = value;
                    OnPropertyChanged();
                  }
              }
          }


        public GuideFrameViewModel(User user)
        {
            LoggedInUser= user;
            FrameContent =  new GuideMenuBar(LoggedInUser);
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            MenuBarCommand = new RelayCommand(Execute_MenuBar, CanExecute_Command);
            UserCommand = new RelayCommand(Execute_User, CanExecute_Command);
            HomeCommand = new RelayCommand(Execute_Home, CanExecute_Command);
            DemoCommand = new RelayCommand(Execute_Demo, CanExecute_Command);
        }

        private void Execute_Demo(object obj)
        {
            //
        }

        private void Execute_Home(object obj)
        {
            FrameContent = new GuideProfile(LoggedInUser);
            var guideProfileVm = new GuideProfileViewModel(LoggedInUser);
            
        }

        private void Execute_User(object obj)
        {
            FrameContent = new GuideProfile(LoggedInUser);
        }

        private void Execute_MenuBar(object obj)
        {
            FrameContent = new GuideMenuBar(LoggedInUser);
            var guideMenuBarVm = new GuideMenuBarViewModel(LoggedInUser);
            guideMenuBarVm.Tryng += Try;
        }

        private void Try()
        {
            FrameContent = new TryingPage();
        }

        private bool CanExecute_Command(object arg)
        {
            return true;
        }
    }
}
