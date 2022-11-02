using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Models
{
    /// <summary>
    /// 第二基础类
    /// </summary>
    public class SecondEntity : BaseEntity
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }
    }
}
