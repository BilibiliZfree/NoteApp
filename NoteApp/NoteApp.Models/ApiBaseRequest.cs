using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Models
{
    public class ApiBaseRequest
    {
        /// <summary>
        /// 应用函数
        /// </summary>
        public Method Method { get; set; }
        /// <summary>
        /// 路由地址
        /// </summary>
        public string? Route { get; set; }
        /// <summary>
        /// 数据格式
        /// </summary>
        public string ContentType { get; set; } = "applicatio/json";
        /// <summary>
        /// 传入数据
        /// </summary>
        public object? Parameter { get; set; }
    }
}
