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
    public class SuperGuestService
    {
		private readonly ISuperGuestRepository superGuestRepository;

		private readonly UserService userService;
		


		public SuperGuestService()
		{
			superGuestRepository = Inject.CreateInstance<ISuperGuestRepository>();

			userService= new UserService();

		}

		public SuperGuest Save(SuperGuest superGuest)
		{
			return superGuestRepository.Save(superGuest);
		}

		

		public List<SuperGuest> GetAll()
		{
			return superGuestRepository.GetAll();
		}


		public void Delete(SuperGuest superGuest)
			{

				superGuestRepository.Delete(superGuest);
			}

	

		public SuperGuest Update(SuperGuest superGuest)
		{
			return superGuestRepository.Update(superGuest);
		}

		

		public List<SuperGuest> GetByUser(User user)

		{
			return superGuestRepository.GetByUser(user);
		}

		public SuperGuest GetById(int id)
		{

			return superGuestRepository.GetById(id);
		}

		public SuperGuest GetSuperGuest(int id)
        {
			foreach(SuperGuest superGuest in superGuestRepository.GetAll())
            {
				
					if (superGuest.GuestId == id)
						return superGuest;
				
            }

			return null;
        }
	}
}
