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
    public class ForumService
    {
		private readonly IForumRepository forumRepository;
		//private readonly UserService userService;



		public ForumService()
		{
			forumRepository = Inject.CreateInstance<IForumRepository>();
			//userService = new UserService();


		}

		public Forums Save(Forums f)
		{
			return forumRepository.Save(f);
		}



		public List<Forums> GetAll()
		{
			return forumRepository.GetAll();
		}


		public void Delete(Forums f)
		{

			forumRepository.Delete(f);
		}



		public Forums Update(Forums f)
		{
			return forumRepository.Update(f);
		}



		public List<Forums> GetByUser(User user)

		{
			return forumRepository.GetByUser(user);
		}

		public Forums GetById(int id)
		{

			return forumRepository.GetById(id);
		}

	}
}
