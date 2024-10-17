using ToDoApp.Dto;

namespace ToDoApp.Utilities.Event.HomePageEvent
{
    public class NoteSelectedMessage
    {
        public NoteDto? Note { get; }

        public NoteSelectedMessage(NoteDto note)
        {
            Note = note;
        }
    }

}
