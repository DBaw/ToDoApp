using System.Collections.Generic;
using ToDoApp.Dto;
using ToDoApp.Utilities.Repository;

namespace ToDoApp.Stores
{
    public class NotesStore
    {
        private INotesRepository _notesRepository;
        private UserDto _userDto;

        public List<NoteDto> UserNotes;

        public NotesStore(INotesRepository notesRepository, UserDto user)
        {
            _notesRepository = notesRepository;
            _userDto = user;

            UserNotes = _notesRepository.ListNotesByUser(_userDto.Id);
        }

        public void AddNote(NoteDto note)
        {
            _notesRepository.AddNote(note);
            ReloadUserNotes();
        }

        public void RemoveNote(int id)
        {
            _notesRepository.RemoveNote(id);
            ReloadUserNotes();
        }

        public NoteDto GetNote(int id)
        {
            NoteDto note = _notesRepository.GetNoteById(id);
            return note;
        }

        public void EditNote(int id, string title, string content)
        {
            _notesRepository.EditNote(id, title, content);
            ReloadUserNotes();
        }

        private void ReloadUserNotes()
        {
            UserNotes.Clear();
            List<NoteDto> allNotes = _notesRepository.ListNotesByUser(_userDto.Id);
            foreach (NoteDto note in allNotes)
            {
                UserNotes.Add(note);
            }

        }
    }
}
