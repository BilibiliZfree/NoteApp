using NoteApp.Services.Interfaces;

namespace NoteApp.Services
{
    /// <summary>
    /// 消息服务
    /// </summary>
    public class MessageService : IMessageService
    {
        public string GetMessage()
        {
            return "Hello from the Message Service";
        }
    }
}
