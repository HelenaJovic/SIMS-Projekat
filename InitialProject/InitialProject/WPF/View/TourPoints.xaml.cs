using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
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
    /// Interaction logic for TourPoints.xaml
    /// </summary>
    public partial class TourPoints : Window
    {
        public TourPointsViewModel pointsView;
        public TourPoints(Tour tour)
        {
            this.Width = 430;
            this.Height = 750;
            InitializeComponent();
            pointsView = new TourPointsViewModel(tour);
            DataContext = pointsView;
            if (pointsView.CloseAction == null)
            {
                pointsView.CloseAction = new Action(this.Close);
            }

        }

        private void CheckBoxChanged(object sender, RoutedEventArgs e)
        {
            pointsView.Changed();

        }
    }
}
