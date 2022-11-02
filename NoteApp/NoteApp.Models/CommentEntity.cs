using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Models
{
    /// <summary>
    /// 用户评论
    /// </summary>
    public class CommentEntity : SecondEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 博客ID
        /// </summary>
        public int BlogID { get; set; }
        /// <summary>
        /// 评论内容
        /// </summary>
        public string? Comment { get; set; }
        /// <summary>
        /// 上层评论ID
        /// </summary>
        public int CommentID { get; set; }
    }
}
