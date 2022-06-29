using System;

namespace NoteApp.Api.Models
{
    public class BaseEntity
    {
        public int ID { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
