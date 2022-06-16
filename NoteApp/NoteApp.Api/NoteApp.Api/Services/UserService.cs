using Microsoft.EntityFrameworkCore;
using NoteApp.Api.Data;
using NoteApp.Api.Models;
using NoteApp.Api.Services.Interfaces;

namespace NoteApp.Api.Services
{
    public class UserService : UserImpl
    {
        private readonly NoteAppApiContext _context;

        public UserService(NoteAppApiContext context)
        {
            _context = context;
        }

        public void AddBlog(int UserId, Blog blog)
        {
            throw new NotImplementedException();
        }

        public User? Create(User newUser)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.AsNoTracking().ToList();
        }

        public User? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
