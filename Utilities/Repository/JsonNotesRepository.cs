using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using ToDoApp.Dto;

namespace ToDoApp.Utilities.Repository
{
    public class JsonNotesRepository : INotesRepository
    {
        private string _filePath;
        private ObservableCollection<NoteDto> _notes;

        public JsonNotesRepository(string filePath)
        {
            _filePath = filePath;
            _notes = LoadNotes();
        }

        private ObservableCollection<NoteDto> LoadNotes()
        {
            if (!File.Exists(_filePath))
            {
                return new ObservableCollection<NoteDto>();
            }

            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<ObservableCollection<NoteDto>>(jsonData) ?? new ObservableCollection<NoteDto>();
        }

        private void SaveNotes()
        {
            var jsonData = JsonConvert.SerializeObject(_notes);
            File.WriteAllText(_filePath, jsonData);
        }

        public void AddNote(NoteDto note)
        {
            _notes.Add(note);
            SaveNotes();
        }

        public void RemoveNote(int id)
        {
            var noteToRemove = _notes.FirstOrDefault(n => n.Id == id);
            if (noteToRemove != null)
            {
                _notes.Remove(noteToRemove);
                SaveNotes();
            }
            else
            {
                throw new ArgumentException($"Note with Id {id} not found.");
            }
        }

        public void EditNote(int id, string? title, string? content)
        {
            var noteToEdit = _notes.FirstOrDefault(n => n.Id == id);
            if (noteToEdit != null)
            {
                noteToEdit.Title = title ?? noteToEdit.Title;
                noteToEdit.Content = content ?? noteToEdit.Content;

                SaveNotes();
            }
            else
            {
                throw new ArgumentException($"Note with Id {id} not found.");
            }
        }

        public NoteDto GetNoteById(int id)
        {
            return _notes.FirstOrDefault(n => n.Id == id) ?? throw new ArgumentException($"Note with Id {id} not found.");
        }

        public ObservableCollection<NoteDto> ListNotesByUser(int userId)
        {
            return new ObservableCollection<NoteDto>(_notes.Where(n => n.UserId == userId));
        }
    }
}
