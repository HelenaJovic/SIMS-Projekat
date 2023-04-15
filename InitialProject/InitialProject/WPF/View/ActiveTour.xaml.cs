using InitialProject.Domain.Model;
using InitialProject.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace InitialProject.WPF.View
{
    /// <summary>
    /// Interaction logic for ActiveTour.xaml
    /// </summary>
    public partial class ActiveTour : Window
    {
        private User loggedUser;
        private int brojac;

        public ActiveTour(User user, int brojac)
        {
            InitializeComponent();
            ActiveTourViewModel activeTourViewModel = new ActiveTourViewModel(user, brojac);
            DataContext = activeTourViewModel;
            if (activeTourViewModel.CloseAction == null)
                activeTourViewModel.CloseAction = new Action(this.Close);
        }

        public ActiveTour()
        {
        }
    }
}
