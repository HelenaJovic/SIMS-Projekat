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
        public ActiveTour()
        {
            InitializeComponent();
            ActiveTourViewModel activeTourViewModel = new ActiveTourViewModel();
            DataContext = activeTourViewModel;
            if (activeTourViewModel.CloseAction == null)
                activeTourViewModel.CloseAction = new Action(this.Close);
        }

    }
}
