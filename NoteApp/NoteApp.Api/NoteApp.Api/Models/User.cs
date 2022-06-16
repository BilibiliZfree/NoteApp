namespace NoteApp.Api.Models
{
    public class User : BaseEntity
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? TelphoneNumber { get; set; }

        public ICollection<Blog>? Blogs { get; set; }
    }
}
