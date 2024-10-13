using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using ToDoApp.Utilities.Event;
using ToDoApp.Utilities.Repository;
using ToDoApp.Dto;

namespace ToDoApp.ViewModels
{
    [ObservableRecipient]
    public partial class CreateAccountViewModel : ViewModelBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMessenger _messenger;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        public string _userLogin = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        public string _userPassword = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        public string _userRepeatedPassword = "";

        public CreateAccountViewModel(IUserRepository userRepository, IMessenger messenger)
        {
            _userRepository = userRepository;
            _messenger = messenger;
        }

        [RelayCommand (CanExecute = nameof(CanCreate))]
        public void Create() 
        {
            bool userAlreadyExists = _userRepository.UserExistsAsync(UserLogin).Result;
            if (userAlreadyExists) 
            {
                CreateAccountMessage message = new("User already exists", true);
                _messenger.Send(message);
            }
            else if (UserPassword != UserRepeatedPassword)
            {
                CreateAccountMessage message = new("Passwords don't match", true);
                _messenger.Send(message);
            }
            else
            {
                UserDto user = new(UserLogin, UserPassword);
                _userRepository.AddUserAsync(user);
                CreateAccountMessage message = new("Account created succesfully");
                _messenger.Send(message);
            }

        }

        private bool CanCreate() => !string.IsNullOrEmpty(UserLogin) && !string.IsNullOrEmpty(UserPassword) && !string.IsNullOrEmpty(UserRepeatedPassword);

        [RelayCommand]
        public void Exit()
        {
            GoBackMessage message = new();
            _messenger.Send(message);
        }

    }
}
