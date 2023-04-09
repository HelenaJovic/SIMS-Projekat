using InitialProject.Domain.Model;
using InitialProject.Repository;
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
using Image = System.Windows.Controls.Image;

namespace InitialProject.View
{
    /// <summary>
    /// Interaction logic for ViewTourGallery.xaml
    /// </summary>
    public partial class ViewTourGallery : Window
    {
        public ViewTourGallery(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = new ViewTourGalleryViewModel(selectedTour);
        }
    }
}