using NoteApp.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NoteApp.Services.Interfaces
{
    public interface IRestSharpServerBase<T,E>
    {
        /// <summary>
        /// 删除<typeparamref name="E"/>数据
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> DeleteApiResponseAsync(int id);
        /// <summary>
        /// 查询所有<typeparamref name="E"/>数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Task<T> GetApiResponseAsync();
        /// <summary>
        /// 使用<typeparamref name="id"/>查询<typeparamref name="E"/>数据
        /// </summary>
        /// <param name="url"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> GetApiResponseAsync(int id);
        /// <summary>
        /// 获取指定博客和指定用户关联信息
        /// </summary>
        /// <param name="blogid"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<T> GetApiResponseAsync(int blogid, int userid);
        /// <summary>
        /// 根据数据在指定字段内搜索
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetApiResponseAsync(int data, string key);
        /// <summary>
        /// 使用关键字查询<typeparamref name="E"/>数据
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetApiResponseAsync(string data, string key);
        Task<T> GetRestResponseAsync();
        Task<T> GetRestResponseAsync(Method method);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="password"></param>
        /// <returns><typeparamref name="T"/></returns>
        Task<T> LoginAsync(int id, string password);
        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<T> LoginAsync(string username, string password);
        /// <summary>
        /// 添加<typeparamref name="E"/>数据
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Task<T> PostApiResponseAsync(E e);
        /// <summary>
        /// 修改<typeparamref name="E"/>数据
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        Task<T> PutApiResponseAsync(E e);
    }
}
