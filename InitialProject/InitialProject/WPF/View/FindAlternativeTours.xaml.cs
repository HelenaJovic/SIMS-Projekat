using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for FindAlternativeTours.xaml
    /// </summary>
    public partial class FindAlternativeTours : Window
    {
        public FindAlternativeTours(User user, Tour tour, TourReservation reservation)
        {
            InitializeComponent();
            FindAlternativeToursViewModel findAlternativeToursView = new FindAlternativeToursViewModel(user, tour, reservation);
            DataContext = findAlternativeToursView;
            if (findAlternativeToursView.CloseAction == null)
                findAlternativeToursView.CloseAction = new Action(this.Close);
        }
    }
}
