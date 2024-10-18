using System.Collections.Generic;
using System.Collections.ObjectModel;
using ToDoApp.Dto;

namespace ToDoApp.Utilities.Repository
{
    public interface INotesRepository
    {
        void AddNote(NoteDto note);
        void RemoveNote(int id);
        void EditNote(int id, string? title, string? content);
        NoteDto GetNoteById(int id);
        ObservableCollection<NoteDto> ListNotesByUser(int userId);
    }
}
