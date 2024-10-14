using ToDoApp.Dto;

namespace ToDoApp.Utilities.Event
{
    internal class LoginSuccessMessage
    {
        public UserDto User { get; }

        public LoginSuccessMessage(UserDto user)
        {
            User = user;
        }
    }
}
