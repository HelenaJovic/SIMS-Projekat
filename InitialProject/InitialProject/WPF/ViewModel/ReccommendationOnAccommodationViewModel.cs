using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class ReccommendationOnAccommodationViewModel : ViewModelBase
    {
        private readonly IMessageBoxService messageBoxService;
        public Action CloseAction { get; set; }

        public OwnerReview SelectedRate { get; set; }
        public ReccommendationOnAccommodationViewModel()
        {
            InitializeCommands();

        }

        private RelayCommand reccommend;
        public RelayCommand Reccommend
        {
            get { return reccommend; }
            set
            {
                reccommend = value;
            }
        }

        private RelayCommand close;
        public RelayCommand Close
        {
            get { return close; }
            set
            {
                close = value;
            }
        }


        private void InitializeCommands()
        {
            
        }



        private string comment;

        public string Comment
        {
            get => comment;
            set
            {
                if (value != comment)
                {
                    comment = value;
                    OnPropertyChanged();
                }
            }
        }

        private string level;

        public string Level
        {
            get => level;
            set
            {
                if (value != level)
                {
                    level = value;
                    OnPropertyChanged();
                }
            }
        }


    }
}
