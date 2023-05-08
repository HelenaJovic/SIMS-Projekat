﻿using InitialProject.Serializer;
using InitialProject.Validation;
using System;
using System.Collections.Generic;
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

        private DateOnly startDate { get; set; }
        public DateOnly StartDate
        {
            get => startDate;
            set
            {
                if (value != startDate)
                {
                    startDate = value;
                    OnPropertyChanged(nameof(StartDate));
                }
            }
        }

        private DateOnly endDate { get; set; }
        public DateOnly EndDate
        {
            get => endDate;
            set
            {
                if (value != endDate)
                {
                    endDate = value;
                    OnPropertyChanged(nameof(EndDate));
                }
            }
        }


        public TourRequest()
        {
            Images = new List<Image>();
        }

        public TourRequest(Location location, string language, int guestNum, DateOnly startDate, DateOnly endDate, int idLocation, string description)
        {
            Location = location;
            TourLanguage = language;
            GuestNum = guestNum;
            StartDate = startDate;
            EndDate = endDate;
            IdLocation = idLocation;
            Description = description;
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
                StartDate.ToString(),
                EndDate.ToString(),
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
            StartDate = DateOnly.Parse(values[5]);
            EndDate = DateOnly.Parse(values[6]);
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
            if (startDate == default(DateOnly))
            {
                this.ValidationErrors["StartDate"] = "StartDate is requied.";
            }
            if (endDate == default(DateOnly))
            {
                this.ValidationErrors["EndDate"] = "EndDate is requied.";
            }

            if (StartDate >= EndDate)
            {
                this.ValidationErrors["StartDate"] = "Start must be before end.";
                this.ValidationErrors["EndDate"] = "End must be after start.";
            }
        }
    }
}
