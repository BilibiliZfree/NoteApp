using NoteApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Core
{
    public static class AppSession
    {
        public static UserEntity user { get; private set; }
        public static UserEntity session(UserEntity updateUser) => user = updateUser;
    }
}
