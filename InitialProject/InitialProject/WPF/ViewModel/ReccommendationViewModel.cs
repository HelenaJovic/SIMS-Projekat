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
    public class ReccommendationViewModel : ViewModelBase
    {
        private readonly IMessageBoxService messageBoxService;

        private readonly RecommendationService recommendationService;
        public Action CloseAction { get; set; }

        public User LogedInUser { get; set; }

        public OwnerReview SelectedRate { get; set; }
        public ReccommendationViewModel(User user, IMessageBoxService _messageBoxService, OwnerReview ownerReview)
        {
            InitializeCommands();
            LogedInUser=user;
            messageBoxService = _messageBoxService;
            recommendationService= new RecommendationService();
            SelectedRate = ownerReview;

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
            Reccommend= new RelayCommand(Execute_Recommend, CanExecute_Command);
            Close = new RelayCommand(Execute_Close, CanExecute_Command);


        }

        private void Execute_Recommend(object obj)
        {
            RecommendationOnAccommodation newRecommend = new (SelectedRate,Comment,SelectedRate.Id, (LevelType)Enum.Parse(typeof(LevelType), Level),LogedInUser.Id);
            RecommendationOnAccommodation savedRecommend = recommendationService.Save(newRecommend);
            Guest1MainWindowViewModel.RecommendationList.Add(savedRecommend);
            CloseAction();
        }

        private void Execute_Close(object obj)
        {
            CloseAction();
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
        private bool CanExecute_Command(object parameter)
        {
            return true;
        }

    }
}
