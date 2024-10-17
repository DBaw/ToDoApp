using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Linq;
using ToDoApp.Dto;
using ToDoApp.Utilities.Event;
using ToDoApp.Utilities.Repository;

namespace ToDoApp.ViewModels
{
    [ObservableRecipient]
    public partial class LoginPageViewModel : ObservableObject
    {
        public UserDto User = new("Domonik", "Hasełko");

        private readonly IUserRepository _userRepository;

        [ObservableProperty]
        private string _userLogin = "";

        [ObservableProperty]
        private string _userPassword = "";

        public LoginPageViewModel(IMessenger messenger, IUserRepository userRepository)
        {
            Messenger = messenger;
            _userRepository = userRepository;
        }


        [RelayCommand]
        public void Login()
        {
            // Get the list of users and try to find the user by login.
            UserDto? loggedUser = _userRepository.ListUsersAsync().Result
                                 .FirstOrDefault(u => u.Name == UserLogin);

            // If the user doesn't exist or the password is wrong, send a unified error message.
            if (loggedUser == null || loggedUser.Password != UserPassword)
            {
                Messenger.Send(new BottomBarMessage("User doesn't exist or wrong password", true));
                return;
            }

            // If both login and password are correct, send the success message.
            Messenger.Send(new LoginSuccessMessage(loggedUser));
        }

        [RelayCommand]
        public void CreateAccount()
        {
            Messenger.Send(new GoToCreateAcccountMessage());
        }
    
    }

}
