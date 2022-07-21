using System;
using System.Collections.Generic;
using System.Text;

namespace NoteApp.Services.Interfaces
{
    /// <summary>
    /// 密文处理服务接口
    /// </summary>
    public interface ICryptogramService
    {
        public byte[] StringToByte(string input);

        public string ByteToString(byte[] input);

        public string HashSHA256(string s);
    }
}
