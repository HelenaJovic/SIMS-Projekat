using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;

namespace InitialProject.WPF.ViewModel
{
    public class ViewTourGalleryViewModel : ViewModelBase
    {
        public static Tour SelectedTour { get; set; }

        //private readonly ImageRepository _imageRepository;

        public static List<String> ImageUrls = new List<String>();

        public ViewTourGalleryViewModel(Tour selectedTour)
        {
            SelectedTour = selectedTour;
            //_imageRepository = new ImageRepository();
            //ImageUrls = new List<String>(_imageRepository.GetUrlByTourId(SelectedTour.Id));

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
                /*   image.Source = bitmapImage;
                   image.Width = 130;
                   image.Height = 130;
                   image.Margin = new Thickness(20, 0, 10, 20);
                   WrapPanel wrapPanel = (WrapPanel)FindName("ImagesPanel");
                   wrapPanel.Children.Add(image);*/
            }
        }

    }
}
