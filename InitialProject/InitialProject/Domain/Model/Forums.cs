using InitialProject.Serializer;
using InitialProject.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
    public class Forums: ValidationBase,ISerializable
    {
        public int id { get; set; }

        public Location location { get; set; }

        public int idUser { get; set; }

        public List<Comment> Comments { get; set; }
        public bool IsClosed { get; set; }

        public User User { get; set; }
        private string mark;
        public string Mark
        {
            get { return mark; }
            set
            {
                if (mark != value)
                {
                    mark = value;
                    OnPropertyChanged(nameof(Mark));
                }
            }
        }
        protected override void ValidateSelf()
        {
            if (this.location.Equals(null))
            {
                this.ValidationErrors["location"] = " Required Text.";
            }

            /*if (this._ruleGrade == 0)
            {
                this.ValidationErrors["RuleGrade"] = "Required grade.";
            }*/
        }
        public Forums(Location location, int idUser,  List<Comment> Comments,bool isClosed,User user)
        {
            this.location = location;
            this.idUser = idUser;
            this.Comments = Comments;
            this.IsClosed = isClosed;
            this.User = user;
        }

        public Forums()
        {
            Comments = new List<Comment>();

        }

        public string[] ToCSV()
        {
           // string comments = Comments != null ? string.Join("; ", Comments.Select(c => c.Text)) : string.Empty;

            string[] csvValues = { id.ToString(), idUser.ToString(), location.Id.ToString(), IsClosed.ToString(), Mark ?? string.Empty };
            return csvValues;
        }



        public void FromCSV(string[] values)
        {
            id = int.Parse(values[0]);
            idUser = int.Parse(values[1]);
            int locationId = int.Parse(values[2]);
            IsClosed = bool.Parse(values[3]);
            Mark = string.IsNullOrEmpty(values[4]) ? null : values[4];

            // Initialize the location object with the parsed locationId
            location = new Location { Id = locationId };

            // Create Comment objects from the parsed comment texts
            // Comments = commentTexts.Select(text => new Comment(text,user,id)).ToList();
        }

    }
}
