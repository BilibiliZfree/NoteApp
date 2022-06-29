using NoteApp.Api.Models;

namespace NoteApp.Api.Services.Interfaces
{
    public interface UserImpl
    {

        public IEnumerable<User> GetAll();

        public User? GetById(int id);

        public User? Create(User newUser);

        public void AddBlog(int UserId, Blog blog);

        public void Update(User user);

        public void DeleteById(int id);
    }
}
