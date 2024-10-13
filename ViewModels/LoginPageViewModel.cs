using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;
using ToDoApp.Utilities.Event;

namespace ToDoApp.ViewModels
{
    [ObservableRecipient]
    public partial class LoginPageViewModel : ViewModelBase
    {
        public UserDto User = new("Domonik", "Hasełko");


        [ObservableProperty]
        private string _userLogin = "";

        [ObservableProperty]
        private string _userPassword = "";

        [RelayCommand]
        public void Login()
        {
            Messenger.Send(new LoginSuccessMessage(User.Id, User.Name));
        }

        [RelayCommand]
        public void CreateAccount()
        {
            Messenger.Send(new GoToCreateAcccountMessage());
        }
    
    }

}
