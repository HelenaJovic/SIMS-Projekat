using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
	public interface INotificationRepository : IRepository<Notifications>
	{
		List<Notifications> GetByUserId(int id);
	}
}
