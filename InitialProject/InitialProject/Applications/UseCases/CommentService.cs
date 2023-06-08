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


        private void BindData(List<Comment> comments)
		{
            foreach (Comment comment in comments)
			{
                comment.User = userService.GetById(comment.UserId);
			}
		}

        private void BindParticularData(Comment comment)
		{
            comment.User = userService.GetById(comment.UserId);
		}
        public List<Comment> GetAll()
        {
            List<Comment> comments = commentRepository.GetAll();
            if(comments.Count > 0)
			{
                BindData(comments);
			}

            return comments;
        }

        public void Delete(Comment comment)
        {
            commentRepository.Delete(comment);
        }

        public Comment Update(Comment comment)
        {
            return commentRepository.Update(comment);
        }

        public List<Comment> GetByUser(User user)
        {
            List<Comment> comments = commentRepository.GetByUser(user);
            if (comments.Count > 0)
            {
                BindData(comments);
            }

            return comments;
        }

        public Comment GetById(int id)
        {
            Comment comment = commentRepository.GetById(id);
            if(comment != null)
			{
                BindParticularData(comment);
			}

            return comment;
        }

        public List<Comment> GetByForum(int forumId)
		{

            List<Comment> comments = commentRepository.GetByForum(forumId);
            if (comments.Count > 0)
            {
                BindData(comments);
            }

            return comments;

        }


        

    }
}
