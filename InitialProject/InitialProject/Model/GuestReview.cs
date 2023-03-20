using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Model
{
	public class GuestReview : ISerializable
	{
		public int Id { get; set; }

		public int IdOwner { get; set; }

		public int IdReservation { get; set; }

		public AccommodationReservation Reservation { get; set; }

		public int CleanlinessGrade { get; set; }

		public int RuleGrade { get; set; }

		public string Comment { get; set; }

		public GuestReview()
		{

		}

		public GuestReview(int idOwner, int idReservation, int cleanlinessGrade, int ruleGrade, string comment)
		{
			this.IdOwner=idOwner;
			this.IdReservation=idReservation;
			this.CleanlinessGrade=cleanlinessGrade;
			this.RuleGrade=ruleGrade;
			this.Comment=comment;

		}

		public void FromCSV(string[] values)
		{
			Id=int.Parse(values[0]);
			IdOwner=int.Parse(values[1]);
			IdReservation=int.Parse(values[2]);
			CleanlinessGrade=int.Parse(values[3]);
			RuleGrade=int.Parse(values[4]);
			Comment=values[5];


		}

		public string[] ToCSV()
		{
			string[] csvValues =
			{
				Id.ToString(),
				IdOwner.ToString(),
				IdReservation.ToString(),
				CleanlinessGrade.ToString(),
				RuleGrade.ToString(),
				Comment


			};
			return csvValues;
		}
	}
}
