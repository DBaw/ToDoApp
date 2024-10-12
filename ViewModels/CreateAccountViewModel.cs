using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Input;
using ToDoApp.Utilities.Event;

namespace ToDoApp.ViewModels
{
    [ObservableRecipient]
    internal partial class CreateAccountViewModel : ViewModelBase
    {
        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        public string _userLogin;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        public string _userPassword;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(CreateCommand))]
        public string _userRepeatedPassword;

        [RelayCommand (CanExecute = nameof(CanCreate))]
        public void Create() 
        {
            if (UserPassword != UserRepeatedPassword) 
            {

                Messenger.Send(new PasswordDoesntMatchMessage());
            }
            else
            {
                Messenger.Send(new CreateAccountSuccessMessage());
            }

        }

        private bool CanCreate() => !string.IsNullOrEmpty(UserLogin) && !string.IsNullOrEmpty(UserPassword) && !string.IsNullOrEmpty(UserRepeatedPassword);

        [RelayCommand]
        public void Exit()
        {
            Messenger.Send(new GoBackMessage());
        }

    }
}
