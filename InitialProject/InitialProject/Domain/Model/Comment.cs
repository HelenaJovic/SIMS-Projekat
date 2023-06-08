using InitialProject.Serializer;
using InitialProject.Validations;
using System;

namespace InitialProject.Domain.Model
{
    public class Comment : ValidationBase, ISerializable
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public int IdForum { get; set; }

        public Forums forum { get; set; }
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
            if (this.Text.Equals(null))
            {
                this.ValidationErrors["Text"] = " Required Text.";
            }

            /*if (this._ruleGrade == 0)
            {
                this.ValidationErrors["RuleGrade"] = "Required grade.";
            }*/
        }
        public Comment() { }

        public Comment(string text, User user, int idForum,Forums forum)
        {
            Text = text;
            User = user;
            IdForum = idForum;
            this.forum = forum;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Text, User.Id.ToString(),IdForum.ToString(), Mark ?? string.Empty };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Text = values[1];
            User = new User() { Id = Convert.ToInt32(values[2]) };
            IdForum = Convert.ToInt32(values[3]);
            Mark = string.IsNullOrEmpty(values[4]) ? null : values[4];
        }

    }
}
