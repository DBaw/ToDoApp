using System.Collections.ObjectModel;
using System.Linq;
using ToDoApp.DB;
using ToDoApp.Dto;

namespace ToDoApp.Utilities.Repository
{
    public class DbNotesRepository(AppDbContext dbContext) : INotesRepository
    {
        private readonly AppDbContext _dbContext = dbContext;

        public void AddNote(NoteDto note)
        {
            _dbContext.Notes.Add(note);
            _dbContext.SaveChanges();
        }

        public void RemoveNote(int id)
        {
            var note = _dbContext.Notes.Find(id);
            if (note != null)
            {
                _dbContext.Notes.Remove(note);
                _dbContext.SaveChanges();
            }
        }

        public void EditNote(int id, string? title, string? content)
        {
            var note = _dbContext.Notes.Find(id);
            if (note != null)
            {
                note.Title = title;
                note.Content = content;
                _dbContext.SaveChanges();
            }
        }

        public NoteDto GetNoteById(int id) => _dbContext.Notes.Find(id);

        public ObservableCollection<NoteDto> ListNotesByUser(int userId) => new ObservableCollection<NoteDto>(_dbContext.Notes.Where(note => note.UserId == userId));
    }
}
