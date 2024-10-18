using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using ToDoApp.Dto;
using ToDoApp.Utilities.Event.HomePageEvent;

namespace ToDoApp.ViewModels
{
    [ObservableRecipient]
    public partial class NotePageViewModel : ObservableObject
    {
        private readonly NoteDto _noteDto;

        [ObservableProperty]
        public string _noteTitle;
        [ObservableProperty]
        public string _noteContent;

        public NotePageViewModel(IMessenger messenger, NoteDto note)
        {
            Messenger = messenger;
            _noteDto = note;

            _noteTitle = _noteDto.Title ?? "";
            _noteContent = _noteDto.Content ?? ""   ;
        }

        [RelayCommand]
        public void Edit()
        {
            Messenger.Send(new GoToEditNoteMessage(_noteDto));
        }
    }
}
