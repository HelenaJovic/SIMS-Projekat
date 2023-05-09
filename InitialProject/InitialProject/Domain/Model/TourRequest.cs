using InitialProject.Domain.RepositoryInterfaces;
using InitialProject.Injector;
using InitialProject.Serializer;
using InitialProject.Validations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace InitialProject.Domain.Model
{
    public class TourRequest : ValidationBase, ISerializable
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public int IdLocation { get; set; }
        public RequestType Status { get; set; }
        public List<Image> Images { get; set; }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private string _language;
        public string TourLanguage
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged(nameof(TourLanguage));
                }
            }
        }

        private int _guestNum;
        public int GuestNum
        {
            get => _guestNum;
            set
            {
                if (value != _guestNum)
                {
                    _guestNum = value;
                    OnPropertyChanged(nameof(GuestNum));
                }
            }
        }

        private DateOnly inputStartdate { get; set; }
        public DateOnly NewStartDate
        {
            get => inputStartdate;
            set
            {
                if (value != inputStartdate)
                {
                    inputStartdate = value;
                    OnPropertyChanged(nameof(NewStartDate));
                }
            }
        }

        private DateOnly inputEnddate { get; set; }
        public DateOnly NewEndDate
        {
            get => inputEnddate;
            set
            {
                if (value != inputEnddate)
                {
                    inputEnddate = value;
                    OnPropertyChanged(nameof(NewEndDate));
                }
            }
        }



        public TourRequest ()
        {
            Images = new List<Image>();
        }

        public TourRequest(Location location, string language, int guestNum, DateOnly startDate, DateOnly endDate, int idLocation, string description)
        {
            Location = location;
            TourLanguage = language;
            GuestNum = guestNum;
            NewStartDate =startDate;
            NewEndDate = endDate;
            IdLocation = idLocation;
            Description=description;
            Status = RequestType.OnHold;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Location.City,
                Location.Country,
                TourLanguage,
                GuestNum.ToString(),
                NewStartDate.ToString(),
                NewEndDate.ToString(),
                IdLocation.ToString(),
                Description,
                Status.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Location = new Location(values[1], values[2]);
            TourLanguage = values[3];
            GuestNum = int.Parse(values[4]);
            NewStartDate = DateOnly.Parse(values[5]);
            NewEndDate = DateOnly.Parse(values[6]);
            IdLocation = int.Parse(values[7]);
            Description = values[8];
            Status = (RequestType)Enum.Parse(typeof(RequestType), values[9]);
        }

        protected override void ValidateSelf()
        {
            if (string.IsNullOrWhiteSpace(this._language))
            {
                this.ValidationErrors["TourLanguage"] = "TourLanguage cannot be empty.";
            }
            if (this._guestNum == 0) 
            {
                this.ValidationErrors["GuestNum"] = "GuestNum cannot be empty.";
            }
            if (string.IsNullOrWhiteSpace(this._description))
            {
                this.ValidationErrors["Description"] = "Description cannot be empty.";
            }


            if (NewStartDate == default(DateOnly))
            {
                this.ValidationErrors["NewStartDate"] = "Start is required.";
            }

            if (NewEndDate == default(DateOnly))
            {
                this.ValidationErrors["NewEndDate"] = "End date cannot be empty.";

            }

            if (NewStartDate >= NewEndDate)
            {
                this.ValidationErrors["NewStartDate"] = "Start must be before end.";
                this.ValidationErrors["NewEndDate"] = "End must be after start.";
            }

        }
    }
}
