using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using InitialProject.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
    public class RateOwnerViewModel:ViewModelBase
    {   public Action CloseAction{ get; set; }

		private readonly OwnerReviewRepository ownerReviewRepository;
		private readonly ImageRepository _imageRepository;
		public User LogedUser;
		public static AccommodationReservation SelectedReservation { get; set; }

		public RateOwnerViewModel(User user,AccommodationReservation reservation)
		{
			
			InitializeCommands();
			SelectedReservation= reservation;
			ownerReviewRepository=new OwnerReviewRepository();
			LogedUser= user;
			_imageRepository=new ImageRepository();

		}

		private bool CanExecute_Command(object parameter)
		{
			return true;
		}

		private void Execute_CancelCommand(object sender)
		{
			CloseAction();
		}

		private void InitializeCommands()
        {
			RateOwner = new RelayCommand(Execute_RateOwner, CanExecute_Command);
			CancelRate = new RelayCommand(Execute_CancelCommand, CanExecute_Command);
			Instruction= new RelayCommand(Execute_Instruction, CanExecute_Command);
		}

        private void Execute_Instruction(object obj)
        {
            throw new NotImplementedException();
        }

        private void Execute_RateOwner(object obj)
        {
			OwnerReview newReview = new OwnerReview(int.Parse(OwnerCorrectness),int.Parse(CleanlinessGrade), Comment, SelectedReservation.Id,SelectedReservation,LogedUser.Id);
			OwnerReview savedReview = ownerReviewRepository.Save(newReview);
			Guest1MainWindowViewModel.RateOwnerList.Add(savedReview);
			_imageRepository.StoreImageOwnerReview(savedReview, ImageUrl);

			CloseAction();
		}


		private RelayCommand rateOwner;
		public RelayCommand RateOwner
		{
			get { return rateOwner; }
			set
			{
				rateOwner = value;
			}
		}

		private RelayCommand instruction;
		public RelayCommand Instruction
		{
			get { return instruction; }
			set
			{
				instruction = value;
			}
		}

		private RelayCommand cancelCommand;
		public RelayCommand CancelRate
		{
			get { return cancelCommand; }
			set
			{
				cancelCommand = value;
			}
		}

		private string _cleanlinessGrade;
		public string CleanlinessGrade
		{
			get => _cleanlinessGrade;
			set
			{
				if (value != _cleanlinessGrade)
				{
					_cleanlinessGrade = value;
					OnPropertyChanged();
				}
			}
		}

		private string _corectness;
		public string OwnerCorrectness
		{
			get => _corectness;
			set
			{
				if (value != _corectness)
				{
					_corectness = value;
					OnPropertyChanged();
				}
			}
		}

		private string _comment;
		public string Comment
		{
			get => _comment;
			set
			{
				if (value != _comment)
				{
					_comment = value;
					OnPropertyChanged();
				}
			}
		}
		private string imageUrl;
		public string ImageUrl
		{
			get => imageUrl;
			set
			{
				if (value != imageUrl)
				{
					imageUrl = value;
					OnPropertyChanged();
				}
			}
		}
		
	}
}
