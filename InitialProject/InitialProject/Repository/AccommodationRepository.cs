using InitialProject.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
	public class AccommodationRepository
	{
        public const string FilePath = "../../../Resources/Data/accommodations.csv";

        private readonly Serializer<Accommodation> _serializer;

        private List<Accommodation> _accommodations;

        public AccommodationRepository()
        {
            _serializer = new Serializer<Accommodation>();
            _accommodations = _serializer.FromCSV(FilePath);
        }

        public List<Accommodation> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Accommodation Save(Accommodation accommodation)
        {
            accommodation.Id = NextId();
            _accommodations = _serializer.FromCSV(FilePath);
            _accommodations.Add(accommodation);
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }

        public int NextId()
        {
            _accommodations = _serializer.FromCSV(FilePath);
            if (_accommodations.Count < 1)
            {
                return 1;
            }
            return _accommodations.Max(c => c.Id) + 1;
        }

        public void Delete(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation founded = _accommodations.Find(a => a.Id == accommodation.Id);
            _accommodations.Remove(founded);
            _serializer.ToCSV(FilePath, _accommodations);
        }

        public Accommodation Update(Accommodation accommodation)
        {
            _accommodations = _serializer.FromCSV(FilePath);
            Accommodation current = _accommodations.Find(a => a.Id == accommodation.Id);
            int index = _accommodations.IndexOf(current);
            _accommodations.Remove(current);
            _accommodations.Insert(index, accommodation);       // keep ascending order of ids in file 
            _serializer.ToCSV(FilePath, _accommodations);
            return accommodation;
        }

        public List<Accommodation> GetByUser(User user)

		{
            _accommodations= _serializer.FromCSV(FilePath);
            return _accommodations.FindAll(a => a.IdUser == user.Id);
		}

       

    }
}

