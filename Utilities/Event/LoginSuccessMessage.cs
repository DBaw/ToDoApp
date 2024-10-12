namespace ToDoApp.Utilities.Event
{
    internal class LoginSuccessMessage
    {
        public int UserId { get; }
        public string Username { get; }

        public LoginSuccessMessage(int userId, string username)
        {
            UserId = userId;
            Username = username;
        }
    }
}
