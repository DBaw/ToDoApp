namespace ToDoApp.Utilities.Event
{
    internal class CreateAccountMessage
    {
        public string Message;
        public bool IsError;

        public CreateAccountMessage(string message, bool isError = false)
        {
            Message = message;
            IsError = isError;
        }
    }
}
