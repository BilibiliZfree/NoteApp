using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NoteApp.Api.Models;

namespace NoteApp.Api.Data
{
    public class NoteAppApiContext : DbContext
    {
        public NoteAppApiContext(DbContextOptions<NoteAppApiContext> options)
            : base(options)
        {
        }

        public DbSet<User>? Users => Set<User>();
    }
}
