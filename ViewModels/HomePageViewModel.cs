using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;
using ToDoApp.Stores;
using ToDoApp.Utilities.Event.HomePageEvent;
using ToDoApp.Utilities.Repository;

namespace ToDoApp.ViewModels
{
    public partial class HomePageViewModel : ObservableRecipient, IRecipient<GoToNotePageMessage>, IRecipient<CancelAddingNoteMessage>, IRecipient<GoToAddNoteMessage>, IRecipient<GoToEditNoteMessage>
    {
        private readonly NotesStore _notesStore;
        private readonly UserDto _user;
        private NoteDto? _selectedNote;

        [ObservableProperty]
        public ObservableObject _currentView;
        
        [ObservableProperty]
        public ObservableObject _notesView;



        public HomePageViewModel(INotesRepository notesRepository, UserDto user)
        {
            _user = user;
            _selectedNote = null;
            _notesStore = new NotesStore(notesRepository, _user);

            NotesView = new NotesPageViewModel(Messenger, _notesStore, _user);
            CurrentView = new SelectNotePageViewModel();

            IsActive = true;
        }

        protected override void OnActivated()
        {
            Messenger.RegisterAll(this);
        }

        public void Receive(GoToNotePageMessage message)
        {
            _selectedNote = message.Note;
            ChangeToSelectedNote();
        }

        public void Receive(CancelAddingNoteMessage message)
        {
            ChangeToSelectedNote();
        }

        public void Receive(GoToAddNoteMessage message)
        {
            CurrentView = new AddNotePageViewModel(Messenger,_notesStore, _user);
        }

        public void Receive(GoToEditNoteMessage message)
        {
            CurrentView = new NoteEditPageViewModel(Messenger, _notesStore, _selectedNote);
        }

        protected override void OnDeactivated()
        {
            Messenger.UnregisterAll(this);
        }


        private void ChangeToSelectedNote()
        {
            if (_selectedNote != null)
            {
                CurrentView = new NotePageViewModel(Messenger, _notesStore, _selectedNote);
            }
            else
            {
                CurrentView = new SelectNotePageViewModel();
            }
        }
    }
}
