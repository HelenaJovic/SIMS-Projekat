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

		private readonly AccommodationReservationService accommodationReservationService;

		private readonly AccommodationService accommodationService;
		public LocationService()
		{
			_locationRepository = Inject.CreateInstance<ILocationRepository>();
			accommodationReservationService = new AccommodationReservationService();
		    accommodationService = new AccommodationService();
			
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



		public Dictionary<Location, int> CalculateReservationCountByLocation(List<Accommodation> accommodations, List<AccommodationReservation> reservations)
		{
			Dictionary<Location, int> reservationCountByLocation = new Dictionary<Location, int>();

			foreach (var accommodation in accommodations)
			{
				reservationCountByLocation[accommodation.Location] = 0;
			}

			foreach (var reservation in reservations)
			{
				Accommodation accommodation = accommodations.FirstOrDefault(a => a.Id == reservation.IdAccommodation);

				if (accommodation != null)
				{
					reservationCountByLocation[accommodation.Location]++;
				}
			}

			return reservationCountByLocation;
		}

		public Location FindBusiestLocation(Dictionary<Location, int> reservationCountByLocation)
		{
			var busiestLocation = reservationCountByLocation.OrderByDescending(x => x.Value).FirstOrDefault();
			return busiestLocation.Key;
		}


		public List<Location> FindWorstLocations(Dictionary<Location, int> reservationCountByLocation)
		{
			int minReservationCount = reservationCountByLocation.Values.Min();
			List<Location> worstLocations = reservationCountByLocation
				.Where(x => x.Value == minReservationCount)
				.Select(x => x.Key)
				.ToList();
			return worstLocations;
		}

		public List<Location> GetUniqueLocations(List<Accommodation> accommodations)
		{
			List<Location> uniqueLocations = new List<Location>();

			foreach (var accommodation in accommodations)
			{
				if (!uniqueLocations.Contains(accommodation.Location))
				{
					uniqueLocations.Add(accommodation.Location);
				}
			}

			return uniqueLocations;
		}

	}
}
