using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;
using ToDoApp.Stores;
using ToDoApp.Utilities.Event;
using ToDoApp.Utilities.Event.HomePageEvent;
using ToDoApp.Utilities.Repository;

namespace ToDoApp.ViewModels
{
    [ObservableRecipient]
    public partial class NoteEditPageViewModel : ObservableObject
    {
        private readonly NotesStore _notesStore;
        private readonly NoteDto _noteDto;

        [ObservableProperty]
        public NoteDto? _selectedNote;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        public string _noteTitle;
        private string originalTile;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SaveCommand))]
        public string _noteContent;
        private string originalContent;

        public NoteEditPageViewModel(IMessenger messenger, NotesStore notesStore, NoteDto note)
        {
            _notesStore = notesStore;
            _noteDto = note;
            Messenger = messenger;

            NoteTitle = note.Title ?? "";
            NoteContent = note.Content ?? "";

            originalTile = NoteTitle;
            originalContent = NoteContent;
        }

        
        [RelayCommand]
        public void Cancel()
        {
            Messenger.Send(new BottomBarMessage("Edit canceled"));
            Messenger.Send(new GoToNotePageMessage(_noteDto));
        }

        [RelayCommand(CanExecute = nameof(CanExecuteSaveCommand))]
        public void Save()
        {
            _notesStore.EditNote(_noteDto.Id, NoteTitle, NoteContent);
            NoteDto editedNote = _notesStore.GetNote(_noteDto.Id);
            Messenger.Send(new BottomBarMessage("Note edited"));
            Messenger.Send(new GoToNotePageMessage(editedNote));
        }
        private bool CanExecuteSaveCommand => NoteTitle != originalTile || NoteContent != originalContent;

    }
}
