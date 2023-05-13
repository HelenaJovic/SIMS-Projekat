using InitialProject.Domain.Model;
using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Repository;
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

		public List<String> GetCities(String Country)
		{
			return _locationRepository.GetCities(Country);
		}

		public List<String> GetAllCountries()
		{
			return _locationRepository.GetAllCountries();
		}

		public Location FindLocation(String Country, String City)
		{
			return _locationRepository.FindLocation(Country, City);
		}


        public List<string> GetLocations()
        {
            List<string> locations = new List<string>();
            foreach (Location l in _locationRepository.GetAll())
            {
				locations.Add(l.Country.ToString() + " " + l.City.ToString());
            }
            return locations;
        }
    }
}
