using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Models
{
    public class ApiResponseR
    {
        //Content = "{
        //              \"message\":\"登录成功\",
        //              \"status\":true,
        //              \"object\":
        //              {
        //                  \"userName\":\"string\",
        //                  \"password\":null,
        //                  \"telphone\":\"string\",
        //                  \"blogs\":null,
        //                  \"id\":1,
        //                  \"createTime\":\"2022-09-01T01:31:05.222\",
        //                  \"updateTime\":\"2022-09-01T01:31:05.222\"
        //                  }
        //              }"

        public string? Message { get; set; }

        public bool Status { get; set; }

        public UserEntity? Object { get; set; }

    }
}
