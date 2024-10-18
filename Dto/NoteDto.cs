using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Dto
{
    public class NoteDto
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public string Background { get; set; }
        public string Foreground { get; set; }

        public NoteDto() { }

        public NoteDto(int userId, string? title, string? content, string background, string foreground)
        {
            UserId = userId;
            Title = title;
            Content = content;
            Background = background;
            Foreground = foreground;
        }
    }
}
