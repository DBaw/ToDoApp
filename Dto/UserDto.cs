using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Dto
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }

        // Empty constructor required by EF
        public UserDto() { }

        public UserDto(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
