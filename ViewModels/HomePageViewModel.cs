using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;
using ToDoApp.Stores;
using ToDoApp.Utilities.Event.HomePageEvent;
using ToDoApp.Utilities.Repository;

namespace ToDoApp.ViewModels
{
    public partial class HomePageViewModel : ObservableRecipient, IRecipient<NoteAddedSuccessMessage>, IRecipient<NoteSelectedMessage>, IRecipient<CancelAddingNoteMessage>, IRecipient<GoToAddNoteMessage>
    {
        private readonly NotesStore _notesStore;
        private readonly UserDto _user;

        [ObservableProperty]
        public ObservableObject _currentView;
        
        [ObservableProperty]
        public ObservableObject _notesView;

        [ObservableProperty]
        private NoteDto? _selectedNote;


        public HomePageViewModel(INotesRepository notesRepository, UserDto user)
        {
            _user = user;
            _notesStore = new NotesStore(notesRepository, _user);
            SelectedNote = null;

            NotesView = new NotesPageViewModel(Messenger, _notesStore, _user);
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

        public void Receive(CancelAddingNoteMessage message)
        {
            CurrentView = new SelectNotePageViewModel();
        }

        public void Receive(GoToAddNoteMessage message)
        {
            CurrentView = new AddNotePageViewModel(Messenger,_notesStore, _user);
        }

        protected override void OnDeactivated()
        {
            Messenger.UnregisterAll(this);
        }
    }
}
