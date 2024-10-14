using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;

namespace ToDoApp.ViewModels
{
    public partial class HomePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        public ViewModelBase _singleNoteView;
        
        [ObservableProperty]
        public ViewModelBase _notesView;


        public HomePageViewModel(IMessenger messenger, UserDto user)
        {
            NotesView = new NotesPageViewModel(messenger, user);
            SingleNoteView = new SingleNotePageViewModel();
        }
    }
}
