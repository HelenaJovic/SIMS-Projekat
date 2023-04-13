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
	internal class AccommodationService
	{
		private readonly IAccommodationRepository _accommodationRepository;

		private readonly ILocationRepository _locationRepository;

		

		public AccommodationService()
		{
			_locationRepository = Inject.CreateInstance<ILocationRepository>();
			_accommodationRepository = Inject.CreateInstance<IAccommodationRepository>();
			
		}
		public List<Accommodation> GetByUser(User user)

		{
			List<Accommodation> accommodations = new List<Accommodation>();
			accommodations=_accommodationRepository.GetByUser(user);
			BindData(accommodations);
			return accommodations;
		}

		public Accommodation GetById(int id)
		{
			Accommodation accommodation = _accommodationRepository.GetById(id);
			BindParticularData(accommodation);
			return accommodation;
		
		}

		public Accommodation Save(Accommodation accommodation)
		{
			Accommodation savedAccommodation = _accommodationRepository.Save(accommodation);
			BindParticularData(savedAccommodation);
			return savedAccommodation;
		}

		private void BindData(List<Accommodation> accommodations)
		{
			foreach (Accommodation accommodation in accommodations)
			{
				accommodation.Location = _locationRepository.GetById(accommodation.IdLocation);
			}

		}

		private void BindParticularData(Accommodation accommodation)
		{
			accommodation.Location= _locationRepository.GetById(accommodation.IdLocation);
		}

		
	}
}
