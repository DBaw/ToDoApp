using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;
using ToDoApp.Stores;
using ToDoApp.Utilities.Event;
using ToDoApp.Utilities.Event.HomePageEvent;

namespace ToDoApp.ViewModels
{
    [ObservableRecipient]
    public partial class NotePageViewModel : ObservableObject
    {
        private readonly NoteDto _noteDto;
        private readonly NotesStore _notesStore;

        [ObservableProperty]
        public string _noteTitle;
        [ObservableProperty]
        public string _noteContent;

        public NotePageViewModel(IMessenger messenger,NotesStore notesStore, NoteDto note)
        {
            Messenger = messenger;
            _noteDto = note;
            _notesStore = notesStore;

            _noteTitle = _noteDto.Title ?? "";
            _noteContent = _noteDto.Content ?? ""   ;
        }

        [RelayCommand]
        public void Edit()
        {
            Messenger.Send(new GoToEditNoteMessage(_noteDto));
        }

        [RelayCommand]
        public void Delete()
        {
            _notesStore.RemoveNote(_noteDto.Id);
            Messenger.Send(new BottomBarMessage("Note deleted", true));
            Messenger.Send(new GoToNotePageMessage(null));
        }
    }
}
