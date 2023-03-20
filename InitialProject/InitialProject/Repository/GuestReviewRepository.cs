using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
	internal class GuestReviewRepository
	{
        public const string FilePath = "../../../Resources/Data/guestreviews.csv";

        private readonly Serializer<GuestReview> _serializer;

        private List<GuestReview> _guestReviews;

        public GuestReviewRepository()
        {
            _serializer = new Serializer<GuestReview>();
            _guestReviews = _serializer.FromCSV(FilePath);
        }

        public List<GuestReview> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public GuestReview Save(GuestReview guestReview)
        {
            guestReview.Id = NextId();
            _guestReviews = _serializer.FromCSV(FilePath);
            _guestReviews.Add(guestReview);
            _serializer.ToCSV(FilePath, _guestReviews);
            return guestReview;
        }

        public int NextId()
        {
            _guestReviews = _serializer.FromCSV(FilePath);
            if (_guestReviews.Count < 1)
            {
                return 1;
            }
            return _guestReviews.Max(c => c.Id) + 1;
        }

        public void Delete(GuestReview guestReview)
        {
            _guestReviews = _serializer.FromCSV(FilePath);
            GuestReview founded = _guestReviews.Find(a => a.Id == guestReview.Id);
            _guestReviews.Remove(founded);
            _serializer.ToCSV(FilePath, _guestReviews);
        }

        public GuestReview Update(GuestReview guestReview)
        {
            _guestReviews = _serializer.FromCSV(FilePath);
            GuestReview current = _guestReviews.Find(a => a.Id == guestReview.Id);
            int index = _guestReviews.IndexOf(current);
            _guestReviews.Remove(current);
            _guestReviews.Insert(index, guestReview);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _guestReviews);
            return guestReview;
        }

    }
}
