using InitialProject.Serializer;
using InitialProject.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
	public class Renovation : ValidationBase, ISerializable
	{
		public int Id { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public int Duration { get; set; }

		public string Description { get; set; }

		public int AccommodationId  { get; set; }

		public Renovation(int id, DateTime startDate, DateTime endDate, int duration, string description, int accommodationId)
		{
			Id = id;
			StartDate = startDate;
			EndDate = endDate;
			Duration = duration;
			Description = description;
			AccommodationId = accommodationId;

		}

		public Renovation()
		{

		}

		public void FromCSV(string[] values)
		{
			Id = int.Parse(values[0]);
			StartDate = DateTime.Parse(values[1]);
			EndDate = DateTime.Parse(values[2]);
			Duration = int.Parse(values[3]);
			Description = values[4];
			AccommodationId = int.Parse(values[4]);


		}

		public string[] ToCSV()
		{
			string[] csvValues =
			{
				Id.ToString(),
				StartDate.ToString(),
				EndDate.ToString(),
				Duration.ToString(),
				Description,
				AccommodationId.ToString()


			};
			return csvValues;
		}

		protected override void ValidateSelf()
		{
			throw new NotImplementedException();
		}
	}
}
