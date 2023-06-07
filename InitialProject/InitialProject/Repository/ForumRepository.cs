using InitialProject.Domain.Model;
using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Repository
{
    public class ForumRepository
    {
        private const string FilePath = "../../../Resources/Data/forums.csv";

        private readonly Serializer<Forums> _serializer;

        private List<Forums> _forums;

        public ForumRepository()
        {
            _serializer = new Serializer<Forums>();
            _forums = _serializer.FromCSV(FilePath);
        }

        public List<Forums> GetAll()
        {
            return _serializer.FromCSV(FilePath);
        }

        public Forums Save(Forums forum)
        {
            forum.id = NextId();
            _forums = _serializer.FromCSV(FilePath);
            _forums.Add(forum);
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        }

        public int NextId()
        {
            _forums = _serializer.FromCSV(FilePath);
            if (_forums.Count < 1)
            {
                return 1;
            }
            return _forums.Max(f => f.id) + 1;
        }

        public List<Forums> GetByUser(User user)
        {
            _forums = _serializer.FromCSV(FilePath);
            return _forums.FindAll(f => f.idUser == user.Id);
        }

        public void Delete(Forums forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            Forums founded = _forums.Find(c => c.id == forum.id);
            _forums.Remove(founded);
            _serializer.ToCSV(FilePath, _forums);
        }

        public Forums Update(Forums forum)
        {
            _forums = _serializer.FromCSV(FilePath);
            Forums current = _forums.Find(c => c.id == forum.id);
            int index = _forums.IndexOf(current);
            _forums.Remove(current);
            _forums.Insert(index, forum);       
            _serializer.ToCSV(FilePath, _forums);
            return forum;
        }
    }
}
