
using NoteApp.Models;

namespace NoteApp.Api.Services.Interfaces
{
    /// <summary>
    /// 通用泛型接口
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <typeparam name="E">泛型参数</typeparam>
    public interface GenericInterface<T,E>
    {
        /// <summary>
        /// 根据ID删除数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> DeleteResponseAsync(int id);
        /// <summary>
        /// 根据ID获取数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> GetResponseAsync(int id);
        /// <summary>
        /// 获取所有数据
        /// </summary>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> GetsResponseAsync();
        /// <summary>
        /// 根据字段获取数据
        /// </summary>
        /// <param name="arg">字段</param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> GetsResponseAsync(string arg, string switch_on);
        /// <summary>
        /// 发送数据实体：用于注册(Put不覆盖前一个请求)
        /// </summary>
        /// <param name="e">数据实体</param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> PostResponseAsync(E e);
        /// <summary>
        /// 推送数据实体：用于数据请求(Post覆盖前一个请求))
        /// </summary>
        /// <param name="e">数据实体</param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> PutResponseAsync(E e);
        /// <summary>
        /// 使用ID查询数据
        /// </summary>
        /// <param name="id">用于搜索的ID</param>
        /// <returns><typeparamref name="E"/></returns>
        Task<E> FindEntityByIdAsync(int id);
        /// <summary>
        /// 查询是否存在数据
        /// </summary>
        /// <param name="obj">传入数据</param>
        /// <param name="arg">对应属性</param>
        /// <returns><typeparamref name="bool"/></returns>
        Task<bool> FindAnyAsync(object obj,string arg);
        /// <summary>
        /// 判断实体成员是否为空
        /// </summary>
        /// <param name="e">数据实体</param>
        /// <param name="key"></param>
        /// <returns></returns>
        bool MemberIsNotNull(E e, int key);

        Task<bool> DataIsNotDuplicateAsync(E e, int arg);
        /// <summary>
        /// 根据对应数据查找实体
        /// </summary>
        /// <param name="arg">传入数据</param>
        /// <param name="key">查询字段</param>
        /// <returns></returns>
        Task<ICollection<E>> SearchEntityAsync(string arg, string key);
    }
}
