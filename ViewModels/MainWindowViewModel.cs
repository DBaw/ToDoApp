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

        [ObservableProperty]
        private ViewModelBase _currentView;

        private ViewModelBase _previousView;

        [ObservableProperty]
        private string _messageText = "";

        [ObservableProperty]
        private bool _isError;

        [ObservableProperty]
        private bool _isMessageVisible = false;

        public MainWindowViewModel(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _currentView = new LoginPageViewModel(Messenger, _userRepository);
            _previousView = _currentView;

            // Activate message listening
            IsActive = true;
        }

        protected override void OnActivated()
        {
            Messenger.RegisterAll(this);
        }


        void IRecipient<LoginSuccessMessage>.Receive(LoginSuccessMessage message)
        {
            _previousView = CurrentView;
            CurrentView = new HomePageViewModel(Messenger, message.User);
        }
        void IRecipient<GoToCreateAcccountMessage>.Receive(GoToCreateAcccountMessage message)
        {
            _previousView = CurrentView;
            CurrentView = new CreateAccountViewModel(_userRepository,Messenger);
        }
        async void IRecipient<BottomBarMessage>.Receive(BottomBarMessage message)
        {
            MessageText = message.Message;
            IsError = message.IsError;
            IsMessageVisible = true;
            await Task.Delay(3000);
            IsMessageVisible = false;
        }
        public void Receive(AccountCreatedMessage message)
        {
            _previousView = new CreateAccountViewModel(_userRepository, Messenger);
            CurrentView = new LoginPageViewModel(Messenger, _userRepository);
        }
        void IRecipient<GoBackMessage>.Receive(GoBackMessage message)
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
