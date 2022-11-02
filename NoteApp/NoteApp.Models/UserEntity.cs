using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Text;

namespace NoteApp.Models
{
    /// <summary>
    /// 用户类
    /// </summary>
    public class UserEntity :SecondEntity
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
        /// 头像
        /// </summary>
        public string? Avatar { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [Column(TypeName="Date")]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 绑定手机号码
        /// </summary>
        public string? Telphone { get; set; }
        /// <summary>
        /// 绑定邮箱
        /// </summary>
        public string? Mail { get; set; }
        /// <summary>
        /// 关注数
        /// </summary>
        public int Follows { get; set; }
        /// <summary>
        /// 粉丝数
        /// </summary>
        public int Fans { get; set; }
    }
}
