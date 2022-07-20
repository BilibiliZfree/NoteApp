namespace NoteApp.Api.Models
{
    public class BlogEntity : BaseEntity
    {
        /// <summary>
        /// 文章标题
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// 文章内容
        /// </summary>
        public string? Context { get; set; }
        /*
        /// <summary>
        /// 文章作者
        /// </summary>
        public UserEntity? Author { get; set; }
        */
        /// <summary>
        /// 关联文章序号
        /// </summary>
        public int UserEntityID { get; set; }
    }
}
