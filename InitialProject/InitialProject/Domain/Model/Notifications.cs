using InitialProject.Serializer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Domain.Model
{
	public class Notifications : ISerializable
	{
		public int Id { get; set; }

		public int UserId { get; set; }

		public string Title { get; set; } 
		public string Content { get; set; }

		public bool IsRead { get; set; }

		public Notifications( int userId,string title, string content, bool isRead)
		{
			
			UserId = userId;
			Title = title;
			Content = content;
			IsRead = isRead;

		}

		public Notifications()
		{

		}


		public void FromCSV(string[] values)
		{
			Id = int.Parse(values[0]);
			UserId = int.Parse(values[1]);
			Title = values[2];
			Content = values[3];
			IsRead = bool.Parse(values[4]);

		}

		public string[] ToCSV()
		{
			string[] csvValues =
			{
				Id.ToString(),
				UserId.ToString(),
				Title,
				Content,
				IsRead.ToString()

			};
			return csvValues;
		}

		
	}
}
