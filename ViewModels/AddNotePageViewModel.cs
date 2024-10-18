using Avalonia.Controls;
using Avalonia.Media;
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
    public partial class AddNotePageViewModel : ObservableObject
    {
        private readonly NotesStore _notesStore;
        private readonly UserDto _userDto;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(ConfirmCommand))]
        private string? _noteTitle;

        [ObservableProperty]
        private string? _noteContent;

        [ObservableProperty]
        private IBrush _noteTextColor = Brushes.White;

        [ObservableProperty]
        private IBrush _selectedColor = Brushes.Gray;

        public AddNotePageViewModel(IMessenger messenger, NotesStore notesStore, UserDto user)
        {
            Messenger = messenger;
            _notesStore = notesStore;
            _userDto = user;
        }

        [RelayCommand(CanExecute =nameof(CanConfirm))]
        private void Confirm()
        {
            NoteDto note = new(_userDto.Id, NoteTitle, NoteContent, SelectedColor.ToString(), NoteTextColor.ToString());
            _notesStore.AddNote(note);
            Messenger.Send(new BottomBarMessage("Note added"));
            Messenger.Send(new NoteAddedSuccessMessage());
        }
        private bool CanConfirm => !string.IsNullOrEmpty(NoteTitle);

        [RelayCommand]
        private void Cancel()
        {
            Messenger.Send(new CancelAddingNoteMessage());
        }

        [RelayCommand]
        private void ChangeColor(object sender) 
        {
            RadioButton? rd = sender as RadioButton;
            string color = rd?.Background?.ToString() ?? "Gray";
            switch (color) 
            {
                case ("Gray"):
                    SelectedColor = Brushes.Gray;
                    NoteTextColor = Brushes.White;
                    return;
                case ("Yellow"):
                    SelectedColor = Brushes.Yellow;
                    NoteTextColor = Brushes.Black;
                    return;
                case ("LightGreen"):
                    SelectedColor = Brushes.LightGreen;
                    NoteTextColor = Brushes.Black;
                    return;
                case ("LightBlue"):
                    SelectedColor = Brushes.LightBlue;
                    NoteTextColor = Brushes.Black;
                    return;
                case ("Purple"):
                    SelectedColor = Brushes.Purple;
                    NoteTextColor = Brushes.White;
                    return;
                case ("Red"):
                    SelectedColor = Brushes.Red;
                    NoteTextColor = Brushes.White;
                    return;
            }
        }
    }
}
