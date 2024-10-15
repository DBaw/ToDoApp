using System.Collections.Generic;
using System.Xml;
using System;
using ToDoApp.Dto;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace ToDoApp.Utilities.Repository
{
    public class JsonNotesRepository : INotesRepository
    {
        private string _filePath;
        private List<NoteDto> _notes;

        public JsonNotesRepository(string filePath)
        {
            _filePath = filePath;
            _notes = LoadNotes();
        }

        private List<NoteDto> LoadNotes()
        {
            if (!File.Exists(_filePath))
            {
                return new List<NoteDto>();
            }

            var jsonData = File.ReadAllText(_filePath);
            return JsonConvert.DeserializeObject<List<NoteDto>>(jsonData) ?? new List<NoteDto>();
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

        public List<NoteDto> ListNotesByUser(int userId)
        {
            return _notes.Where(n => n.UserId == userId).ToList();
        }
    }
}
