namespace NoteApp.Api.Models
{
    public class Blog : BaseEntity
    {
        public string? Title { get; set; }
        public string? Context { get; set; }
        public User? User { get; set; }
    }
}
