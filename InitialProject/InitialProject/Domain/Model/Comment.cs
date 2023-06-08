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

        public bool IsOwnerComment { get; set; }

        public bool CanReport { get; set; }

        public int ReportsNumber { get; set; }

        public bool AlreadyReported { get; set; }
        public Comment() { }

        public Comment(string text, User user, int userId, int forumId, bool isOwnerComment, bool canReport, int reportsNumber, bool alreadyReported)
        {
            Text = text;
            User = user;
            UserId = userId;
            ForumId = forumId;
            IsOwnerComment = isOwnerComment;
            CanReport = canReport;
            ReportsNumber = reportsNumber;
            AlreadyReported = alreadyReported;
        }

        public string[] ToCSV()
        {
            string[] csvValues =

             {
               Id.ToString(),
               UserId.ToString(),
               Text.ToString(),
               ForumId.ToString(),
               IsOwnerComment.ToString(),
               CanReport.ToString(),
               ReportsNumber.ToString(),
               AlreadyReported.ToString()
               

            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            UserId = int.Parse(values[1]);
            Text = values[2];
            ForumId = int.Parse(values[3]);
            IsOwnerComment = bool.Parse(values[4]);
            CanReport = bool.Parse(values[5]);
            ReportsNumber = int.Parse(values[6]);
            AlreadyReported = bool.Parse(values[7]);

        }
    }
}
