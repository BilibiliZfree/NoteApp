using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Models
{
    /// <summary>
    /// 收藏关系表
    /// </summary>
    public class CollectionRelation : BaseEntity
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
        /// 收藏时间
        /// </summary>
        public DateTime CollectionTime { get; set; }
    }
}
