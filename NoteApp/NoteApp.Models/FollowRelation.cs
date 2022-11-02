using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Models
{
    /// <summary>
    /// 关注关系表
    /// </summary>
    public class FollowRelation : BaseEntity
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { get; set; }
        /// <summary>
        /// 被关注者ID
        /// </summary>
        public int FollowID { get; set; }
        /// <summary>
        /// 关注时间
        /// </summary>
        public DateTime FollowTime { get; set; }
    }
}
