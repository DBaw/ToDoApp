using Avalonia.Media;
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
        public string Background { get; }
        public string Foreground { get; } 

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
