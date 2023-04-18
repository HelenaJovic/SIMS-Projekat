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
	
	public class LocationService
	{

		private readonly ILocationRepository _locationRepository;
		public LocationService()
		{
			_locationRepository = Inject.CreateInstance<ILocationRepository>();
		}

		public Location GetById(int id)
		{
			return _locationRepository.GetById(id);
		}
	}
}
