using NoteApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace NoteApp.Services
{
    /// <summary>
    /// 密文处理服务
    /// </summary>
    public class CryptogramService : ICryptogramService
    {
        public byte[] StringToByte(string input)
        {
            return Encoding.UTF8.GetBytes(input);
        }

        public string ByteToString(byte[] input)
        {
            return Convert.ToBase64String(input);
        }

        public string HashSHA256(string s)
        {
            SHA256 cryptogram = SHA256.Create();
            byte[] bytes = cryptogram.ComputeHash(StringToByte(s));
            return ByteToString(bytes);
        }
    }
}
