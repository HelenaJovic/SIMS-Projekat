using InitialProject.Serializer;
using InitialProject.Validations;
using InitialProject.View;
using InitialProject.WPF.View;
using InitialProject.WPF.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace InitialProject.Domain.Model
{
    public class Tour : ValidationBase, ISerializable
    {
        public int Id { get; set; }
        public Location Location { get; set; }
        public int IdLocation { get; set; }
        public List<Image> Images { get; set; }
        public TimeOnly StartTime { get; set; }
        public int FreeSetsNum { get; set; }
        public bool Active { get; set; }
        public bool Ended { get; set; }
        public int IdUser { get; set; }
        public bool UsedVoucher { get; set; }
        public int IdRequest { get; set; }

        private int _againGuestNum;
        public int AgainGuestNum
        {
            get => _againGuestNum;
            set
            {
                if (value != _againGuestNum)
                {
                    _againGuestNum = value;
                    OnPropertyChanged(nameof(AgainGuestNum));
                }
            }
        }


        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                if (value != _name)
                {
                    _name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private string _description;
        public string Descripiton
        {
            get => _description;
            set
            {
                if (value != _description)
                {
                    _description = value;
                    OnPropertyChanged("Description");
                }
            }
        }

        private string _language;
        public string Language
        {
            get => _language;
            set
            {
                if (value != _language)
                {
                    _language = value;
                    OnPropertyChanged("Language");
                }
            }
        }

        private string _points;
        public string Points
        {
            get => _points;
            set
            {
                if (value != _points)
                {
                    _points = value;
                    OnPropertyChanged("Points");
                }
            }
        }

        public int MaxGuestNum { get; set; }
        public DateOnly Date { get; set; }
        public int Duration { get; set; }

        public string ImageSource { get; set; }

        
        public Tour()
        {
            Images = new List<Image>();
        }


        public Tour(string name, Location location, string language, int maxGuestNum, DateOnly date, TimeOnly startTime, int duration, int freeSetsNum, bool active, int idUser, int idLocation, bool usedVoucher)

        {
            Name = name;
            Location = location;
            Language = language;
            MaxGuestNum = maxGuestNum;
            Date = date;
            StartTime = startTime;
            Duration = duration;
            FreeSetsNum = freeSetsNum;
            Active = active;
            Ended = false;
            IdUser = idUser;
            IdLocation = idLocation;
            Images = new List<Image>();
            UsedVoucher=usedVoucher;
            IdRequest = 0;
        }

        public string[] ToCSV()
        {
            string[] csvValues =
            {
                Id.ToString(),
                Name,
                Language,
                MaxGuestNum.ToString(),
                Date.ToString(),
                StartTime.ToString(),
                Duration.ToString(),
                FreeSetsNum.ToString(),
                Active.ToString(),
                Ended.ToString(),
                IdUser.ToString(),
                IdLocation.ToString(),
                UsedVoucher.ToString(),
                IdRequest.ToString(),
            };
            return csvValues;
        }

        public void FromCSV(string[] values)
        {
            Id = int.Parse(values[0]);
            Name = values[1];
            Language = values[2];
            MaxGuestNum = int.Parse(values[3]);
            Date = DateOnly.Parse(values[4]);
            StartTime = TimeOnly.Parse(values[5]);
            Duration = int.Parse(values[6]);
            FreeSetsNum = int.Parse(values[7]);
            Active = bool.Parse(values[8]);
            Ended = bool.Parse(values[9]);
            IdUser = int.Parse(values[10]);
            IdLocation = int.Parse(values[11]);
            UsedVoucher = bool.Parse(values[12]);
            IdRequest = int.Parse(values[13]);
        }

        protected override void ValidateSelf()
        {
            if(GuideFrameViewModel.Frame.Content is CreateTour || GuideFrameViewModel.Frame.Content is CreateTourByLocation)
            {
                if (string.IsNullOrWhiteSpace(this._name))
                {
                    this.ValidationErrors["Name"] = "Name is required.";
                }
                if (string.IsNullOrWhiteSpace(this._language))
                {
                    this.ValidationErrors["Language"] = "Language is required. ";
                }
                if (string.IsNullOrWhiteSpace(this._description))
                {
                    this.ValidationErrors["Descripiton"] = "Description is required. ";
                }
                if (string.IsNullOrWhiteSpace(this._points))
                {
                    this.ValidationErrors["Points"] = "Tour points are required. ";
                }
            }

            if(GuideFrameViewModel.Frame.Content is CreateTourByLanguage)
            {
                if (string.IsNullOrWhiteSpace(this._name))
                {
                    this.ValidationErrors["Name"] = "Name is required.";
                }
                if (string.IsNullOrWhiteSpace(this._description))
                {
                    this.ValidationErrors["Descripiton"] = "Description is required. ";
                }
                if (string.IsNullOrWhiteSpace(this._points))
                {
                    this.ValidationErrors["Points"] = "Tour points are required. ";
                }
            }
            

        }
    }
}