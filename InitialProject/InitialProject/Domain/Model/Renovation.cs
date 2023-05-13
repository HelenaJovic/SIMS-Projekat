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

		private DateOnly inputStartdate { get; set; }
		public DateOnly StartDate
		{
			get => inputStartdate;
			set
			{
				if (value != inputStartdate)
				{
					inputStartdate = value;
					OnPropertyChanged(nameof(StartDate));
				}
			}
		}

		private DateOnly inputEnddate { get; set; }
		public DateOnly EndDate
		{
			get => inputEnddate;
			set
			{
				if (value != inputEnddate)
				{
					inputEnddate = value;
					OnPropertyChanged(nameof(EndDate));
				}
			}
		}

		public int Duration { get; set; }

		public string Description { get; set; }

		public int AccommodationId  { get; set; }

		public Renovation( DateOnly startDate, DateOnly endDate, int duration, string description, int accommodationId)
		{
			
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
			StartDate = DateOnly.Parse(values[1]);
			EndDate = DateOnly.Parse(values[2]);
			Duration = int.Parse(values[3]);
			Description = values[4];
			AccommodationId = int.Parse(values[5]);


		}

		public string[] ToCSV()
		{
			string[] csvValues =
			{
				Id.ToString(),
				StartDate.ToShortDateString(),
				EndDate.ToShortDateString(),
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
