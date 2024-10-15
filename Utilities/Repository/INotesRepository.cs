using System.Collections.Generic;
using ToDoApp.Dto;

namespace ToDoApp.Utilities.Repository
{
    public interface INotesRepository
    {
        void AddNote(NoteDto note);
        void RemoveNote(int id);
        void EditNote(int id, string? title, string? content);
        NoteDto GetNoteById(int id);
        List<NoteDto> ListNotesByUser(int userId);
    }
}
