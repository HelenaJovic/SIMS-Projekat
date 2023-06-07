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
        public int id { get; set; }

        public Location location { get; set; }

        public int idUser { get; set; }

        public List<Comment> Comments { get; set; }
        public bool IsClosed { get; set; }

        public Forums(Location location, int idUser,  List<Comment> Comments,bool isClosed)
        {
            this.location = location;
            this.idUser = idUser;
            this.Comments = Comments;
            this.IsClosed = isClosed;
        }

        public Forums()
        {
        }

        public string[] ToCSV()
        {
            string comments = string.Join("; ", Comments.Select(c => c.Text)); 

            string[] csvValues = { id.ToString(), idUser.ToString(), location.Id.ToString(), IsClosed.ToString(), comments};
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            int id = int.Parse(values[0]);
            int idUser = int.Parse(values[1]);
            int locationId = int.Parse(values[2]);
            bool isClosed = bool.Parse(values[3]);
            string[] commentTexts = values[4].Split(';');
        }
    }
}
