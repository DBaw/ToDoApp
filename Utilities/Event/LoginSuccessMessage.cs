using ToDoApp.Dto;

namespace ToDoApp.Utilities.Event
{
    public class LoginSuccessMessage
    {
        public UserDto User { get; }

        public LoginSuccessMessage(UserDto user)
        {
            User = user;
        }
    }
}
