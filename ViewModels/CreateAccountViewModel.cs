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
            Messenger = messenger;
        }

        [RelayCommand (CanExecute = nameof(CanCreate))]
        public void Create() 
        {
            bool userAlreadyExists = _userRepository.UserExistsAsync(UserLogin).Result;
            if (userAlreadyExists) 
            {
                BottomBarMessage message = new("User already exists", true);
                Messenger.Send(message);
            }
            else if (UserPassword != UserRepeatedPassword)
            {
                BottomBarMessage message = new("Passwords don't match", true);
                Messenger.Send(message);
            }
            else
            {
                UserDto user = new(UserLogin, UserPassword);
                _userRepository.AddUserAsync(user);
                BottomBarMessage barMessage = new BottomBarMessage("Account created succesfully", false);
                LoginSuccessMessage loginMessage = new(user);
                Messenger.Send(barMessage);
                Messenger.Send(loginMessage);
            }

        }

        private bool CanCreate() => !string.IsNullOrEmpty(UserLogin) && !string.IsNullOrEmpty(UserPassword) && !string.IsNullOrEmpty(UserRepeatedPassword);

        [RelayCommand]
        public void Exit()
        {
            GoBackMessage message = new();
            Messenger.Send(message);
        }

    }
}
