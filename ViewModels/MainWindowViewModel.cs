using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Threading.Tasks;
using ToDoApp.Utilities.Event;

namespace ToDoApp.ViewModels
{
    public partial class MainWindowViewModel : ObservableRecipient, IRecipient<LoginSuccessMessage>, IRecipient<GoToCreateAcccountMessage>, IRecipient<CreateAccountSuccessMessage>, IRecipient<GoBackMessage>, IRecipient<PasswordDoesntMatchMessage>
    {

        [ObservableProperty]
        private ViewModelBase _currentView = new LoginPageViewModel();

        private ViewModelBase _previousView;

        [ObservableProperty]
        private string _messageText;

        [ObservableProperty]
        private bool _isError;

        [ObservableProperty]
        private bool _isMessageVisible = false;

        public MainWindowViewModel()
        {
            // Activate message listening
            IsActive = true;
        }

        // Listen for LoginSuccessMessage and change the view
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
            CurrentView = new CreateAccountViewModel();
        }
        async void IRecipient<CreateAccountSuccessMessage>.Receive(CreateAccountSuccessMessage message)
        {
            _previousView = CurrentView;
            CurrentView = new LoginPageViewModel();
            MessageText = message.Message;
            IsError = false;
            IsMessageVisible = true;
            await Task.Delay(3000);
            IsMessageVisible = false;
        }
        async void IRecipient<PasswordDoesntMatchMessage>.Receive(PasswordDoesntMatchMessage message)
        {
            MessageText = message.Message;
            IsError = true;
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
