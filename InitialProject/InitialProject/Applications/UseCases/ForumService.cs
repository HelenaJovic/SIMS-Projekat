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
		private readonly LocationService locationService;


		public ForumService()
		{
			forumRepository = Inject.CreateInstance<IForumRepository>();
			locationService=new LocationService();
			//userService = new UserService();


		}
		

		public Forums Save(Forums f, Comment comment)
		{
			int nextForumId = GetNextForumId();
			f.id = nextForumId;
			f.Comments.Add(comment);

			return forumRepository.SaveWithComment(f,comment);
		}
	




		public List<Forums> GetAll()
		{
			
			return forumRepository.GetAll();
		}
	
		public int GetNextForumId()
		{
			List<Forums> allForums = forumRepository.GetAll();
			if (allForums.Count > 0)
			{
				return allForums.Max(f => f.id) + 1;
			}
			else
			{
				return 1;
			}
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
