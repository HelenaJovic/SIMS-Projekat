using InitialProject.Serializer;
using System;

namespace InitialProject.Domain.Model
{
    public class Comment : ISerializable
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public User User { get; set; }

        public int UserId { get; set; }
        public int ForumId { get; set; }
        public Comment() { }

        public Comment(string text, User user, int userId, int forumId)
        {
            Text = text;
            User = user;
            UserId = userId;
            ForumId = forumId;
        }

        public string[] ToCSV()
        {
            string[] csvValues =

             {
               Id.ToString(),
               UserId.ToString(),
               Text.ToString(),
               ForumId.ToString()
               

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            Text = values[2];
            ForumId = int.Parse(values[3]);

        }
    }
}
