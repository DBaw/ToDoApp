using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;
using ToDoApp.Stores;
using ToDoApp.Utilities.Event.HomePageEvent;
using ToDoApp.Utilities.Repository;

namespace ToDoApp.ViewModels
{
    public partial class HomePageViewModel : ObservableRecipient, IRecipient<NoteAddedSuccessMessage>, IRecipient<NoteSelectedMessage>
    {
        private readonly NotesStore _notesStore;

        [ObservableProperty]
        public ObservableObject _currentView;
        
        [ObservableProperty]
        public ObservableObject _notesView;

        [ObservableProperty]
        private NoteDto? _selectedNote;


        public HomePageViewModel(INotesRepository notesRepository, UserDto user)
        {
            _notesStore = new NotesStore(notesRepository, user);
            SelectedNote = null;

            NotesView = new NotesPageViewModel(Messenger, _notesStore, user);
            CurrentView = new SelectNotePageViewModel();

            IsActive = true;
        }

        protected override void OnActivated()
        {
            Messenger.RegisterAll(this);
        }

        public void Receive(NoteAddedSuccessMessage message)
        {
            CurrentView = new SingleNotePageViewModel(Messenger, null);
        }

        public void Receive(NoteSelectedMessage message)
        {
            CurrentView = new SingleNotePageViewModel(Messenger, message.Note);
        }

        protected override void OnDeactivated()
        {
            Messenger.UnregisterAll(this);
        }
    }
}
