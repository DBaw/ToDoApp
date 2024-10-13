using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Dto
{
    public class UserDto
    {
        [Key]
        public int Id { get; }
        public string Name { get; }
        public string Password { get; }

        public UserDto(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
