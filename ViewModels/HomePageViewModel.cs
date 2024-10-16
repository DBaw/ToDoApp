using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;

namespace ToDoApp.ViewModels
{
    public partial class HomePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        public ViewModelBase _currentView;
        
        [ObservableProperty]
        public ViewModelBase _notesView;


        public HomePageViewModel(IMessenger messenger, UserDto user)
        {
            NotesView = new NotesPageViewModel(messenger, user);
            _currentView = new AddNotePageViewModel();
        }
    }
}
