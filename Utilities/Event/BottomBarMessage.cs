namespace ToDoApp.Utilities.Event
{
    public class BottomBarMessage
    {
        public string Message;
        public bool IsError;

        public BottomBarMessage(string message, bool isError = false)
        {
            Message = message;
            IsError = isError;
        }
    }
}
