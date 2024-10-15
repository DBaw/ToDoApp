namespace ToDoApp.Utilities.Event
{
    public class BottomBarMessage
    {
        public string Message;
        public bool IsError;
        public int DelayMs;

        public BottomBarMessage(string message, bool isError, int delay)
        {
            Message = message;
            IsError = isError;
            DelayMs = delay;
        }

        public BottomBarMessage(string message, int delay)
        {
            Message = message;
            DelayMs = delay;
            IsError = false;
        }

        public BottomBarMessage(string message, bool isError = false)
        {
            Message = message;
            IsError = isError;
            DelayMs = 3000;
        }
    }
}
