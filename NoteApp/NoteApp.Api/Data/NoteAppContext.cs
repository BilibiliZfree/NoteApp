using Microsoft.EntityFrameworkCore;
using NoteApp.Api.Models;

namespace NoteApp.Api.Data
{
    public class NoteAppContext : DbContext
    {
        public NoteAppContext(DbContextOptions<NoteAppContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<BlogEntity> Blogs => Set<BlogEntity>();
    }
}
