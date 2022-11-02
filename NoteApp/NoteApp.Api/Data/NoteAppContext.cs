using Microsoft.EntityFrameworkCore;
using NoteApp.Models;

namespace NoteApp.Api.Data
{
    public class NoteAppContext : DbContext
    {
        public NoteAppContext(DbContextOptions<NoteAppContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<BlogEntity> Blogs => Set<BlogEntity>();
        public DbSet<FollowRelation> Follows => Set<FollowRelation>();
        public DbSet<CollectionRelation> Collections => Set<CollectionRelation>();
        public DbSet<BlogsRelation> BlogsRelations => Set<BlogsRelation>();
        public DbSet<CommentEntity> Comments => Set<CommentEntity>();
    }
}
