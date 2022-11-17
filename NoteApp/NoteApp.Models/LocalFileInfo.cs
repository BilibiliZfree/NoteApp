using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NoteApp.Models
{
    public class LocalFileInfo
    {
        public LocalFileInfo(bool isDirectory, string? fullName, string? name, DirectoryInfo? parent, string? icon)
        {
            IsDirectory = isDirectory;
            FullName = fullName;
            Name = name;
            Parent = parent;
            Icon = icon;
        }

        public bool IsDirectory { get; set; }
        public string? FullName { get; set; }
        public string? Name { get; set; }
        public DirectoryInfo? Parent { get; set; }
        public string? Icon { get; set; }
    }
}
