using InitialProject.Applications.UseCases;
using InitialProject.Commands;
using InitialProject.Domain.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.WPF.ViewModel
{
	public class OwnerForumViewModel : ViewModelBase
	{
		public static User LoggedInUser { get; set; }

		public static ObservableCollection<Forums> AllForums { get; set; }

		public static ObservableCollection<Forums> AvailableForums { get; set; }

		private readonly ForumService forumService;

		private readonly CommentService commentService;

		private ObservableCollection<Comment> _comments;

		public ObservableCollection<Comment> Comments
		{
			get { return _comments; }
			set
			{
				_comments = value;
				OnPropertyChanged(nameof(Comments));
			}
		}

		private Forums _selectedForum;

		public Forums SelectedForum
		{
			get { return _selectedForum; }

			set
			{
				_selectedForum = value;
				Comments = new ObservableCollection<Comment>(commentService.GetByForum(SelectedForum.Id));
				OnPropertyChanged(nameof(Comments));
				
			
				OnPropertyChanged(nameof(SelectedForum));
				if (AvailableForums.Any(forum => forum.Id == value.Id))
				{
					_canComment = true;
					CanComment=true;
				}

				else
				{
					_canComment = false;
					CanComment=false;
				}
				OnPropertyChanged(nameof(CanComment));

			}
		}

		private String _yourComment;
		public String YourComment
		{
			get { return _yourComment; }
			set
			{
				_yourComment = value;

			}
		}

		private bool _canComment;

		public bool CanComment
		{
			get { return _canComment; }
			set
			{
				if (_canComment == value)
				OnPropertyChanged(nameof(CanComment));
			}
		}

		private RelayCommand confirmCreate;
		public RelayCommand ConfirmCreate
		{
			get { return confirmCreate; }
			set
			{
				confirmCreate = value;
			}
		}

		public OwnerForumViewModel(User user)
		{
			forumService = new ForumService();
			commentService = new CommentService();
			AllForums = new ObservableCollection<Forums>(forumService.GetAll());
			ConfirmCreate = new RelayCommand(Execute_ConfirmCreate, CanExecute_Command);
			AvailableForums= new ObservableCollection<Forums>(forumService.GetAvailableForums(user));
			LoggedInUser = user;
		}

		private void Execute_ConfirmCreate(object sender)
		{
			Comment newComment = new Comment(YourComment, LoggedInUser, LoggedInUser.Id, SelectedForum.Id);
			Comment savedComment = commentService.Save(newComment);

			Comments.Add(savedComment);
		}

		private bool CanExecute_Command(object parameter)
		{

			return true;
			
		}
	}
}
