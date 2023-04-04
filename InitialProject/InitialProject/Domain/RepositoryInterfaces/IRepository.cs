using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.RepositoryInterfaces
{
	internal interface IRepository<T>
	{
		T Save(T entity);

		T Update(T entity);

		void Delete(T entity);

		int NextId();

		List<T> GetAll();

		T GetById(int id);
	}
}
