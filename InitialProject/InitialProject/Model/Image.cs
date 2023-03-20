using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
    public class Image : ISerializable
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int IdAccommodation { get; set; }
<<<<<<< HEAD
        public int IdTour { get; set; }

        public Image(string url, int idAccommodation, int idTour)
        {
            this.Url = url;
            this.IdAccommodation = idAccommodation;
            this.IdTour = idTour;
        }
=======

		public int IdTour { get; set; }

		public Image(string url, int idAccommodation, int idTour)
		{
			this.Url = url;
			this.IdAccommodation = idAccommodation;
			this.IdTour=idTour;
		}

		public Image()
		{

		}

		public void FromCSV(string[] values)
		{
			Id=int.Parse(values[0]);
			Url=values[1];
			IdAccommodation=int.Parse(values[2]);
			IdTour=int.Parse(values[3]);
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346

        public Image()
        {

<<<<<<< HEAD
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Url = values[1];
            IdAccommodation = int.Parse(values[2]);
            IdTour = int.Parse(values[3]);

        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Url,
                IdAccommodation.ToString(),
                IdTour.ToString()
            };
            return csvValues;

        }
    }
=======
		public string[] ToCSV()
		{
			string[] csvValues =
			{
				Id.ToString(),
				Url,
				IdAccommodation.ToString(),
				IdTour.ToString()
			};
			return csvValues;
				
		}
	}
>>>>>>> 3b6201a38a1ddd5ee4c887f61b0a46940f62e346
}

