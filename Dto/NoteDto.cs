using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Dto
{
    public class NoteDto
    {
        [Key]
        public int Id { get; }
        public int UserId {  get; }
        public string? Title { get; set; }
        public string? Content { get; set; }

        public NoteDto(int userId, string? title, string? content)
        {
            UserId = userId;
            Title = title;
            Content = content;
        }
    }
}
