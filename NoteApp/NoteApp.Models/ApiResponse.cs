using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Models
{
    /// <summary>
    /// WebApi结果实体
    /// </summary>
    public class ApiResponse
    {
        public ApiResponse()
        {
        }

        public ApiResponse(string message, bool status = false)
        {
            Message = message;
            Status = status;
        }

        public ApiResponse(object @object)
        {
            Object = @object;
        }

        public ApiResponse(bool status, object @object)
        {
            Status = status;
            Object = @object;
        }
        /// <summary>
        /// ApiResponse构造函数
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="status">返回状态</param>
        /// <param name="object">代入实体</param>
        public ApiResponse(string message, bool status, object @object) : this(message, status)
        {
            Object = @object;
        }


        /// <summary>
        /// 提示信息
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// 返回状态
        /// </summary>
        public bool Status { get; set; }
        /// <summary>
        /// 代入实体
        /// </summary>
        public object? Object { get; set; }
    }
}
