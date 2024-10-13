using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;
using ToDoApp.Utilities.Event;
using ToDoApp.Utilities.Repository;

namespace ToDoApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableRecipient, IRecipient<LoginSuccessMessage>, IRecipient<GoToCreateAcccountMessage>, IRecipient<CreateAccountMessage>, IRecipient<GoBackMessage>
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

        public MainWindowViewModel(IMessenger messenger, IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _currentView = new LoginPageViewModel(Messenger);
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
            CurrentView = new HomePageViewModel();
        }
        void IRecipient<GoToCreateAcccountMessage>.Receive(GoToCreateAcccountMessage message)
        {
            _previousView = CurrentView;
            CurrentView = new CreateAccountViewModel(_userRepository,Messenger);
        }
        async void IRecipient<CreateAccountMessage>.Receive(CreateAccountMessage message)
        {
            if (!message.IsError)
            {
                _previousView = CurrentView;
                CurrentView = new LoginPageViewModel(Messenger);
            }
            MessageText = message.Message;
            IsError = message.IsError;
            IsMessageVisible = true;
            await Task.Delay(3000);
            IsMessageVisible = false;
        }
        void IRecipient<GoBackMessage>.Receive(GoBackMessage message)
        {
            CurrentView = _previousView;
        }

        protected override void OnDeactivated()
        {
            Messenger.UnregisterAll(this);
        }
    }
}
