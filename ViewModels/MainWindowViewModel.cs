using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;
using ToDoApp.Utilities.Event;
using ToDoApp.Utilities.Repository;

namespace ToDoApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableRecipient, IRecipient<LoginSuccessMessage>, IRecipient<GoToCreateAcccountMessage>, IRecipient<BottomBarMessage>, IRecipient<GoBackMessage>, IRecipient<AccountCreatedMessage>, IRecipient<LogoutMessage>
    {
        private readonly IUserRepository _userRepository;
        private readonly INotesRepository _notesRepository;

        [ObservableProperty]
        private ObservableObject _currentView;

        private ObservableObject _previousView;

        [ObservableProperty]
        private string _messageText = "";

        [ObservableProperty]
        private bool _isError;

        [ObservableProperty]
        private bool _isMessageVisible = false;

        public MainWindowViewModel(IUserRepository userRepository, INotesRepository notesRepository)
        {
            _userRepository = userRepository;
            _notesRepository = notesRepository;

            CurrentView = new LoginPageViewModel(Messenger, _userRepository);
            _previousView = _currentView;

            IsActive = true;
        }

        protected override void OnActivated()
        {
            Messenger.RegisterAll(this);
        }


        public void Receive(LoginSuccessMessage message)
        {
            _previousView = CurrentView;
            CurrentView = new HomePageViewModel(_notesRepository, message.User);
        }
        public void Receive(GoToCreateAcccountMessage message)
        {
            _previousView = CurrentView;
            CurrentView = new CreateAccountViewModel(_userRepository,Messenger);
        }
        public async void Receive(BottomBarMessage message)
        {
            MessageText = message.Message;
            IsError = message.IsError;
            IsMessageVisible = true;
            await Task.Delay(message.DelayMs);
            IsMessageVisible = false;
        }
        public void Receive(AccountCreatedMessage message)
        {
            _previousView = new CreateAccountViewModel(_userRepository, Messenger);
            CurrentView = new LoginPageViewModel(Messenger, _userRepository);
        }
        public void Receive(GoBackMessage message)
        {
            CurrentView = _previousView;
        }
        public void Receive(LogoutMessage message)
        {
            CurrentView = new LoginPageViewModel(Messenger, _userRepository);
            _previousView = CurrentView;
        }

        protected override void OnDeactivated()
        {
            Messenger.UnregisterAll(this);
        }
    }
}
