
namespace NoteApp.Api.Services.Interfaces
{
    /// <summary>
    /// 通用泛型接口
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <typeparam name="E">泛型参数</typeparam>
    public interface GenericInterface<T,E>
    {
        Task<T> DeleteResponseAsync(int id);
        Task<T> GetResponseAsync(int id);
        Task<T> GetsResponseAsync();
        Task<T> GetsResponseAsync(string arg);
        Task<T> PostResponseAsync(E e);
        Task<T> PutResponseAsync(E e);
    }
}
