using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Applications.UseCases
{
    public class CommentService
    {
        private readonly ICommentRepository commentRepository;
         private readonly UserService userService;

        public CommentService()
        {
            commentRepository = Inject.CreateInstance<ICommentRepository>();
             userService = new UserService();
        }

        public Comment Save(Comment comment)
        {
            return commentRepository.Save(comment);
        }

        public List<Comment> GetAll()
        {
            return commentRepository.GetAll();
        }

        public void Delete(Comment comment)
        {
            commentRepository.Delete(comment);
        }

        public Comment Update(Comment comment)
        {
            return commentRepository.Update(comment);
        }
        public int GetGuestCommentsCountByForum(int forumId)
        {
            List<Comment> comments = GetCommentsByForum(forumId);
            int guestCommentsCount = comments.Count(c => GetActualUserRole(c.User.Id) == Roles.GUEST1);
            return guestCommentsCount;
        }

        public int GetOwnerCommentsCountByForum(int forumId)
        {
            List<Comment> comments = GetCommentsByForum(forumId);
            int ownerCommentsCount = comments.Count(c => GetActualUserRole(c.User.Id) == Roles.OWNER);
            return ownerCommentsCount;
        }

        public Roles GetActualUserRole(int userId)
        {
            User user = userService.GetById(userId);
            if (user != null)
            {
                return user.Role;
            }
            return Roles.GUEST1;
        }




        public List<Comment> GetByUser(User user)
        {
            return commentRepository.GetByUser(user);
        }

        public Comment GetById(int id)
        {
            return commentRepository.GetById(id);
        }

        public List<Comment> GetCommentsByForum(int id)
        {
            List<Comment> comments = commentRepository.GetAll();
            List<Comment> matchingComments = new List<Comment>();

            foreach (Comment comment in comments)
            {
                if (comment.IdForum == id)
                {
                    matchingComments.Add(comment);
                }
            }

            return matchingComments;
        }

    }
}
