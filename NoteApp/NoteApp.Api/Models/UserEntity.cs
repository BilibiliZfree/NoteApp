namespace NoteApp.Api.Models
{
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string? UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        public string? Password { get; set; }
        /// <summary>
        /// 绑定手机号码
        /// </summary>
        public string? Telphone { get; set; }
        /// <summary>
        /// 用户发布的博客
        /// </summary>
        public ICollection<BlogEntity>? Blogs { get; set; }
    }
}
