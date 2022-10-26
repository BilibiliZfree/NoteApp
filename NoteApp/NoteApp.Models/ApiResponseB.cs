using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace NoteApp.Models
{
    /// <summary>
    /// 响应博客信息返回体
    /// </summary>
    public class ApiResponseB
    {
        //  "message": "数据库中有2篇笔记.",
        //  "status": true,
        //  "object": [
        //    {
        //      "title": "string",
        //      "context": "string",
        //      "userEntityID": 1,
        //      "id": 1,
        //      "createTime": "2022-09-01T01:31:05.222",
        //      "updateTime": "2022-09-01T01:31:05.222"
        //    },
        //    {
        //      "title": "string",
        //      "context": "string",
        //      "userEntityID": 6,
        //      "id": 2,
        //      "createTime": "2022-10-08T03:29:51.699",
        //      "updateTime": "2022-10-08T03:29:51.699"
        //    }
        //  ]
        //}
        public string? Message { get; set; }

        public bool Status { get; set; }

        public ObservableCollection<BlogEntity>? Object { get; set; }
    }
}