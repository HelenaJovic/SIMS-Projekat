using InitialProject.Applications.UseCases;
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
    /// Interaction logic for AddDate.xaml
    /// </summary>
    public partial class AddDate : Window
    {
        public AddDate(Tour tour)
        {
            this.Width = 430;
            this.Height = 750;
            InitializeComponent();
            AddDateViewModel addView = new AddDateViewModel(tour);
            DataContext = addView;
            if (addView.CloseAction == null)
            {
                addView.CloseAction = new Action(this.Close);
            }
        }
    }
}
