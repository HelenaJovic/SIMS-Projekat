using InitialProject.Domain.Model;
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
		private readonly AccommodationRepository _accommodationRepository;
		List<Accommodation> _accommodations;

		public AccommodationService()
		{
			_accommodationRepository=new AccommodationRepository();
			_accommodations = _accommodationRepository.GetAll();
		}
		public List<Accommodation> GetByUser(User user)

		{
			List<Accommodation> accommodations = new List<Accommodation>();
			accommodations=_accommodationRepository.GetByUser(user);
			return accommodations;
		}

		public Accommodation GetById(int id)
		{
			foreach(Accommodation accommodation in _accommodations)
			{
				if(accommodation.Id == id)
					return accommodation;
			}
			return null;
		
		}

		public Accommodation Save(Accommodation accommodation)
		{
			Accommodation savedAccommodation = _accommodationRepository.Save(accommodation);
			return savedAccommodation;
		}

	}
}
