namespace ToDoApp.Utilities.Event
{
    internal class PasswordDoesntMatchMessage
    {
        public string Message;

        public PasswordDoesntMatchMessage(string message = "Passwords don't match")
        {
            Message = message;
        }
    }
}
