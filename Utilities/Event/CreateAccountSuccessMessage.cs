namespace ToDoApp.Utilities.Event
{
    internal class CreateAccountSuccessMessage
    {
        public string Message;

        public CreateAccountSuccessMessage(string message = "Account created succesfully")
        {
            Message = message;
        }
    }
}
