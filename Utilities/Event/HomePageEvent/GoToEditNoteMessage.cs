using ToDoApp.Dto;

namespace ToDoApp.Utilities.Event.HomePageEvent
{
    public class GoToEditNoteMessage
    {
        public NoteDto Note;

        public GoToEditNoteMessage(NoteDto note)
        {
            Note = note;
        }
    }
}
