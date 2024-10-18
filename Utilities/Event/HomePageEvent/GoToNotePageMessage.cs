using ToDoApp.Dto;

namespace ToDoApp.Utilities.Event.HomePageEvent
{
    public class GoToNotePageMessage
    {
        public NoteDto? Note { get; }

        public GoToNotePageMessage(NoteDto note)
        {
            Note = note;
        }
    }

}
