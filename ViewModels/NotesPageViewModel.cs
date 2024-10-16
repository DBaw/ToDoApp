using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;
using ToDoApp.Utilities.Event;

namespace ToDoApp.ViewModels
{

    [ObservableRecipient]
    public partial class NotesPageViewModel : ViewModelBase
    {
        private UserDto _user;

        [ObservableProperty]
        public string _welcomeText = string.Empty;

        public NotesPageViewModel(IMessenger messenger, UserDto user)
        {
            Messenger = messenger;
            _user = user;

            WelcomeText = $"Welcome to NOTES, {user.Name}!";
        }

        [RelayCommand]
        public void AddNote()
        {

        }

        [RelayCommand]
        public void GoToSettings()
        {

        }

        [RelayCommand]
        public void Logout()
        {
            Messenger.Send(new LogoutMessage());
        }

    }
}
