using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
    public class Forums: ISerializable
    {
        public int Id { get; set; }

        public Location Location { get; set; }

        public int LocationId { get; set; }

        public int IdUser { get; set; }

        public List<Comment> Comments { get; set; }
        public bool IsClosed { get; set; }

        public Forums(Location location,int locationId, int idUser,  List<Comment> Comments,bool isClosed)
        {
            this.Location = location;
            this.LocationId = locationId;
            this.IdUser = idUser;
            this.Comments = Comments;
            this.IsClosed = isClosed;
        }

        public Forums()
        {
        }

        public string[] ToCSV()
        {
            string[] csvValues =

            {
               Id.ToString(),
               IdUser.ToString(),
               LocationId.ToString(),
               IsClosed.ToString()

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
           Id = int.Parse(values[0]);
           IdUser = int.Parse(values[1]);   
           LocationId = int.Parse(values[2]);
           IsClosed = bool.Parse(values[3]);
            
        }
    }
}
