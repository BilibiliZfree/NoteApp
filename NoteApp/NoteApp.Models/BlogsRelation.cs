using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Models
{
    /// <summary>
    /// 博客所属关系表
    /// </summary>
    public class BlogsRelation : BaseEntity
    {
        /// <summary>
        /// 博客ID
        /// </summary>
        public int BlogID { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
    }
}
