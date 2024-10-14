namespace ToDoApp.Utilities.Event
{
    internal class BottomBarMessage
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
