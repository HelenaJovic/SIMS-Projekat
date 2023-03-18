using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
	internal class ImageRepository
	{
        private const string FilePath = "../../../Resources/Data/images.csv";

        private readonly Serializer<Image> _serializer;

        private List<Image> _images;

        public ImageRepository()
        {
            _serializer = new Serializer<Image>();
            _images = _serializer.FromCSV(FilePath);
        }

        public List<Image> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Image Save(Image image)
        {
            image.Id = NextId();
            _images = _serializer.FromCSV(FilePath);
            _images.Add(image);
            _serializer.ToCSV(FilePath, _images);
            return image;
        }

        public int NextId()
        {
            _images = _serializer.FromCSV(FilePath);
            if (_images.Count < 1)
            {
                return 1;
            }
            return _images.Max(c => c.Id) + 1;
        }

        public void Delete(Image image)
        {
            _images = _serializer.FromCSV(FilePath);
            Image founded = _images.Find(a => a.Id == image.Id);
            _images.Remove(founded);
            _serializer.ToCSV(FilePath, _images);
        }

        public Image Update(Image image)
        {
            _images = _serializer.FromCSV(FilePath);
            Image current = _images.Find(a => a.Id == image.Id);
            int index = _images.IndexOf(current);
            _images.Remove(current);
            _images.Insert(index, image);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _images);
            return image;
        }

        public List<String> GetUrlByAccommodationId(int id)
        {
            List<String> urlList = new List<String>();
            
            foreach(Image image in _images)
			{
				if (image.IdAccommodation == id)
				{
                    urlList.Add(image.Url);
				}
			}
            return urlList;
		}
    }
}
