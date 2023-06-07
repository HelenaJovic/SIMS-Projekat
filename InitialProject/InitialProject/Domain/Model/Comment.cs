using InitialProject.Serializer;
using System;

namespace InitialProject.Domain.Model
{
    public class Comment : ISerializable
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }
        public int IdForum { get; set; }
        public Comment() { }

        public Comment(string text, User user, int idForum)
        {
            Text = text;
            User = user;
            IdForum = idForum;
        }

        public string[] ToCSV()
        {
            string[] csvValues = { Id.ToString(), Text, User.Id.ToString(),IdForum.ToString() };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = Convert.ToInt32(values[0]);
            Text = values[1];
            User = new User() { Id = Convert.ToInt32(values[2]) };
            IdForum = Convert.ToInt32(values[3]);
        }
    }
}
