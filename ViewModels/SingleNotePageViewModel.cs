﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;
using ToDoApp.Utilities.Event;

namespace ToDoApp.ViewModels
{
    [ObservableRecipient]
    public partial class SingleNotePageViewModel : ObservableObject
    {
        [ObservableProperty]
        public NoteDto? _selectedNote = null;

        [ObservableProperty]
        public string _noteTitle = "";

        [ObservableProperty]
        public string _noteContent = "";

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(EditNoteCommand))]
        [NotifyCanExecuteChangedFor(nameof(SaveNoteCommand))]
        public bool _isReadOnlyMode;

        public SingleNotePageViewModel(IMessenger messenger, NoteDto note)
        {
            Messenger = messenger;
            IsReadOnlyMode = true;

            NoteTitle = note.Title ?? "Select Note";
            NoteContent = note.Content ?? string.Empty;
        }

        
        [RelayCommand(CanExecute=nameof(CanExecuteEditNote))]
        public void EditNote()
        {
            IsReadOnlyMode = !IsReadOnlyMode;
            
        }
        private bool CanExecuteEditNote => IsReadOnlyMode && SelectedNote != null;

        [RelayCommand(CanExecute = nameof(CanExecuteSaveNote))]
        public void SaveNote()
        {
            IsReadOnlyMode = !IsReadOnlyMode;
            Messenger.Send(new BottomBarMessage("Note saved"));
        }
        private bool CanExecuteSaveNote => IsReadOnlyMode == false;

    }
}
