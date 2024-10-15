using CommunityToolkit.Mvvm.ComponentModel;
using ToDoApp.Dto;

namespace ToDoApp.ViewModels
{
    public partial class SingleNotePageViewModel : ViewModelBase
    {
        [ObservableProperty]
        public NoteDto _selectedNote;

        [ObservableProperty]
        public string _noteTitle;

        [ObservableProperty]
        public string _noteContent;

        [ObservableProperty]
        public bool _isReadOnlyMode;

        public SingleNotePageViewModel()
        {
            if (SelectedNote == null) 
            {
                NoteTitle = "Select Note"; 
            }

            IsReadOnlyMode = true;

        }

    }
}
