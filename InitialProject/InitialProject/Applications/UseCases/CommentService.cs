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
        // private readonly UserService userService;

        public CommentService()
        {
            commentRepository = Inject.CreateInstance<ICommentRepository>();
            // userService = new UserService();
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

        public List<Comment> GetByUser(User user)
        {
            return commentRepository.GetByUser(user);
        }

        public Comment GetById(int id)
        {
            return commentRepository.GetById(id);
        }

    }
}
