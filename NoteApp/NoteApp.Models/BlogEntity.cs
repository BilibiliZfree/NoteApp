using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NoteApp.Models
{
    /// <summary>
    /// 博客类
    /// </summary>
    public class BlogEntity : SecondEntity
    {
        /// <summary>
        /// 文章标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string? PictrueLink { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string? Context { get; set; }
        /// <summary>
        /// 分类
        /// </summary>
        public string? Classification { get; set; }
        /// <summary>
        /// 点赞数
        /// </summary>
        public int Likes { get; set; }
        /// <summary>
        /// 被收藏数
        /// </summary>
        public int Collections { get; set; }
        /// <summary>
        /// 被浏览次数
        /// </summary>
        public int Hits { get; set; }
        /// <summary>
        /// 评论状态
        /// </summary>
        public bool Comment_Status { get; set; }
    }
}
