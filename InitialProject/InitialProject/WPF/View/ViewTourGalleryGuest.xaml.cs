using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
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
        public static Tour SelectedTour { get; set; }


        private readonly ImageRepository _imageRepository;

        public static List<String> ImageUrls = new List<String>();
        public ViewTourGallery(Tour selectedTour)
        {
            InitializeComponent();
            DataContext = this;
            SelectedTour = selectedTour;
            _imageRepository = new ImageRepository();
            ImageUrls = new List<String>(_imageRepository.GetUrlByTourId(SelectedTour.Id));


            DisplayPictures();

        }

        private void DisplayPictures()
        {

            foreach (String url in ImageUrls)
            {

                Image image = new Image();
                BitmapImage bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.UriSource = new Uri(url, UriKind.Relative);
                bitmapImage.EndInit();
                image.Source = bitmapImage;
                image.Width = 150;
                image.Height = 180;
                image.Margin = new Thickness(20, 0, 10, 20);
                WrapPanel wrapPanel = (WrapPanel)FindName("ImagesPanel");
                wrapPanel.Children.Add(image);

            }

        }
    }
}