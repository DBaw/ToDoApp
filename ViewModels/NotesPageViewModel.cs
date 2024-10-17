using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using System.Collections.Generic;
using ToDoApp.Dto;
using ToDoApp.Stores;
using ToDoApp.Utilities.Event;
using ToDoApp.Utilities.Event.HomePageEvent;

namespace ToDoApp.ViewModels
{

    [ObservableRecipient]
    public partial class NotesPageViewModel : ObservableObject
    {
        private readonly NotesStore _notesStore;
        private UserDto _user;

        [ObservableProperty]
        public string _welcomeText = string.Empty;

        [ObservableProperty]
        public List<NoteDto> _notes;

        private NoteDto? _selectedNote;
        public NoteDto? SelectedNote
        {
            get => _selectedNote;
            set
            {
                SetProperty(ref _selectedNote, value);
                OnSelectedNoteChanged();
            }
        }


        public NotesPageViewModel(IMessenger messenger, NotesStore notesStore, UserDto user)
        {
            Messenger = messenger;
            _notesStore = notesStore;
            _user = user;

            WelcomeText = $"Welcome to NOTES, {user.Name}!";
            Notes = _notesStore.UserNotes;
        }

        private void OnSelectedNoteChanged()
        {
            Messenger.Send(new NoteSelectedMessage(_selectedNote));
        }

        [RelayCommand]
        public void GoToAddNote()
        {

        }

        [RelayCommand]
        public void GoToSettings()
        {

        }

        [RelayCommand]
        public void Logout()
        {
            Messenger.Send(new LogoutMessage());
        }

    }
}
