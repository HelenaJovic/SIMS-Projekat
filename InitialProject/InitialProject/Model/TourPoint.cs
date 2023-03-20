using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class TourPoint : ISerializable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public int Order { get; set; }
        public List<User> Guests { get; set; }
        public int IdTour { get; set; }
        public TourPoint()
        {
            Guests = new List<User>();
        }
        public TourPoint(string name, bool active, int order, int idTour)
        {
            Name = name;
            Active = active;
            Order = order;
            IdTour = idTour;
        }
        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Active = bool.Parse(values[2]);
            Order = int.Parse(values[3]);
            IdTour = int.Parse(values[4]);
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Active.ToString(),
                Order.ToString(),
                IdTour.ToString(),
            };
            return csvValues;
        }
    }
}
