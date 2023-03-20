using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{

	public class Accommodation : ISerializable

	{

		public int Id { get; set; }

		public string Name { get; set; }

		public Location Location { get; set; }

		public AccommodationType Type { get; set; }

		public int MaxGuestNum { get; set; }

		public int MinReservationDays { get; set; }

		public int DaysBeforeCancel { get; set; }

		public List<Image> Images	{get; set;}

		public int IdUser { get; set; }


		public Accommodation(string name,Location location, AccommodationType type, int maxGuestNum, int minResevationDays, int daysBeforeCancel, int idUser)



		{
			this.Name = name;
			this.Location = location;
			this.Type = type;
			this.MaxGuestNum = maxGuestNum;
			this.MinReservationDays=minResevationDays;
			this.DaysBeforeCancel = daysBeforeCancel;
			//this.IdImage = idImage;
			this.IdUser= idUser;


		}

		public Accommodation()
		{

		}

		public void FromCSV(string[] values)
		{
			Id = int.Parse(values[0]);
			Name = values[1];
			Location = new Location(values[2], values[3]);
			Type = (AccommodationType)Enum.Parse(typeof(AccommodationType), values[4]);
			MaxGuestNum = int.Parse(values[5]);
			MinReservationDays = int.Parse(values[6]);
			DaysBeforeCancel=int.Parse(values[7]);
			IdUser = int.Parse(values[9]);
  }

		public string[] ToCSV()
		{
			string[] csvValues =
			{
				Id.ToString(),
				Name,
				Location.City,
				Location.Country,
				Type.ToString(),
				MaxGuestNum.ToString(),
				MinReservationDays.ToString(),
				DaysBeforeCancel.ToString(),
				//IdImage.ToString(),
				IdUser.ToString(),


			};
			return csvValues;
		}

	}
}
