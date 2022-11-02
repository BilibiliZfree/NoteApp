using Microsoft.EntityFrameworkCore;
using NoteApp.Api.Data;
using NoteApp.Api.Services.Interfaces;
using NoteApp.Models;
using System.Reflection.Metadata;

namespace NoteApp.Api.Services
{
    public class BlogsRelationService : GenericInterface<ApiResponse, BlogsRelation>
    {
        private readonly NoteAppContext _noteAppContext;

        public BlogsRelationService(NoteAppContext noteAppContext)
        {
            _noteAppContext = noteAppContext;
        }

        public Task<bool> DataIsNotDuplicateAsync(BlogsRelation e, int arg)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> DeleteResponseAsync(int id)
        {
            try
            {
                
                BlogsRelation relation = await FindEntityByIdAsync(id);

                if (relation != null)
                {
                    _noteAppContext.BlogsRelations.Remove(relation);
                    if (await _noteAppContext.SaveChangesAsync() > 0)
                    {
                        return new ApiResponse("成功删除该记录", true);
                    }
                    else
                        throw new Exception("数据删除失败.");
                }
                else
                    throw new Exception($"搜索不到ID为{id}的记录.");
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-BlogsRelationService-DeleteResponseAsync-{ex.Message}");
            }
        }

        public Task<bool> FindAnyAsync(object obj, string arg)
        {
            throw new NotImplementedException();
        }

        public async Task<BlogsRelation> FindEntityByIdAsync(int id)
        {
            try
            {
                BlogsRelation? relation = await _noteAppContext.BlogsRelations.FindAsync(id);
                if (relation is not null)
                    return relation;
                else
                    throw new Exception($"FindEntityByIdAsync-找不到ID为{id}的记录.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<ApiResponse> GetResponseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetsResponseAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> GetsResponseAsync(int blogid, int userid)
        {
            try
            {
                BlogsRelation? relation = await _noteAppContext.BlogsRelations.FirstAsync(o => o.BlogID == blogid && o.UserID == userid );
                if (relation is not null)
                    return new ApiResponse("找到关联数据.",true,relation);
                else
                    throw new Exception($"找不到该记录.");
            }
            catch (Exception  ex)
            {
                return new ApiResponse($"Api-BlogsRelationService-GetsResponseAsync-{ex.Message}");
            }
        }

        public async Task<ApiResponse> GetsResponseAsync(int data, string key)
        {
            try
            {
                switch (key)
                {
                    case "UserId":
                        ICollection<BlogsRelation> relation1 = await _noteAppContext.BlogsRelations.Where(o => o.UserID == data).ToListAsync();
                        if (relation1.Count > 0)
                            return new ApiResponse($"找到{relation1.Count}条关联数据.", true, relation1);
                        else
                            throw new Exception($"找不到相关记录.");
                    case "BlogId":
                        ICollection<BlogsRelation> relation2 = await _noteAppContext.BlogsRelations.Where(o => o.BlogID == data).ToListAsync();
                        if (relation2.Count > 0)
                            return new ApiResponse($"找到{relation2.Count}条关联数据.", true, relation2);
                        else
                            throw new Exception($"找不到相关记录.");
                    default:
                        throw new Exception($"输入值错误.");
                }
            }
            catch (Exception ex)
            {

                return new ApiResponse($"Api-BlogsRelationService-GetsResponseAsync-{ex.Message}.");
            }
        }
        /// <summary>
        /// 空
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ApiResponse> GetsResponseAsync(string data, string key)
        {
            throw new NotImplementedException();
        }

        public bool MemberIsNotNull(BlogsRelation e, int key)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiResponse> PostResponseAsync(BlogsRelation e)
        {
            try
            {
                if (e is null)
                {
                    throw new Exception("绑定的数据输入有误.");
                }
                else
                {
                    await _noteAppContext.BlogsRelations.AddAsync(e);
                    if (await _noteAppContext.SaveChangesAsync() > 0)
                    {
                        return new ApiResponse("已成功过关联用户-博客.", true, e);
                    }
                    else
                        throw new Exception("关联博客失败.");
                }
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-BlogsRelationService-PostResponseAsync-{ex.Message}");
            }
            throw new NotImplementedException();
        }
        /// <summary>
        /// 空
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ApiResponse> PutResponseAsync(BlogsRelation e)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// 空
        /// </summary>
        /// <param name="data"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task<ICollection<BlogsRelation>> SearchEntityAsync(string data, string key)
        {
            throw new NotImplementedException();
        }
    }
}
