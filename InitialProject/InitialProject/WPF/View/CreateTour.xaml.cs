using InitialProject.Applications.UseCases;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using InitialProject.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Image = InitialProject.Domain.Model.Image;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for CreateTour.xaml
    /// </summary>
    public partial class CreateTour : Window
    {
        public CreateTour(User user)
        {
            InitializeComponent();
            this.Width = 430;
            this.Height = 750;
            CreateTourViewModel createView = new CreateTourViewModel(user);
            DataContext = createView;
            if(createView.CloseAction == null)
            {
                createView.CloseAction = new Action(this.Close);
            }
        }

    }
}