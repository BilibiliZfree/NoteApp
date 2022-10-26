using NoteApp.Api.Data;
using NoteApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NoteApp.Models;
using System.Collections.Immutable;

namespace NoteApp.Api.Services
{
    public class BlogsService : GenericInterface<ApiResponse,BlogEntity>
    {
        public readonly NoteAppContext _appContext;

        public BlogsService(NoteAppContext appContext)
        {
            _appContext = appContext;
        }

        public async Task<ApiResponse> DeleteResponseAsync(int id)
        {
            var Blog = await _appContext.Blogs.FindAsync(id);
            if (Blog == null) return new ApiResponse($"找不到序号为{id}的笔记.");
            _appContext.Blogs.Remove(Blog);
            await _appContext.SaveChangesAsync();
            return new ApiResponse("笔记已删除！", true);
        }

        public async Task<ApiResponse> GetResponseAsync(int id)
        {
            var Blog = await _appContext.Blogs.FindAsync(id);
            if (Blog == null) return new ApiResponse($"找不到序号为{id}的笔记.");
            return new ApiResponse($"找到序号为{id}的笔记《{Blog}》", true, Blog);
        }

        public async Task<ApiResponse> GetMyBlogsResponseAsync(int id)
        {
            var Blogs = await _appContext.Blogs.Where(o => o.UserEntityID == id).ToListAsync();
            if (Blogs == null) return new ApiResponse($"找不到该用户 ID：{id} 发表的笔记.");
            return new ApiResponse($"找到用户 ID：{id} 发表的笔记共{Blogs.Count}篇", true, Blogs);
        }
        /// <summary>
        /// 输入参数获取列表
        /// </summary>
        /// <param name="arg">GetAllBlogs</param>
        /// <param name="arg">GetBlogsSortByCreateTimeAsc</param>
        /// <param name="arg">GetBlogsSortByCreateTimeDesc</param>
        /// <param name="arg">GetBlogsSortByUpdateTimeAsc</param>
        /// <param name="arg">GetBlogsSortByUpdateTimeDesc</param>
        /// <returns></returns>
        public async Task<ApiResponse> GetsResponseAsync(string arg)
        {
            var context = await _appContext.Blogs.ToListAsync();
            if (context is null || context.Count == 0) 
            { 
                return new ApiResponse("数据库中并没有笔记.");
            }
            else
            {
                List<BlogEntity>? blogs = new List<BlogEntity>(); ;
                switch (arg)
                {
                    case "GetAllBlogs":
                        blogs = context;
                        break;
                    case "GetBlogsSortByCreateTimeAsc":
                        blogs = context;
                        break;
                    case "GetBlogsSortByCreateTimeDesc":
                        context.Reverse();
                        blogs = context;
                        break;
                    case "GetBlogsSortByUpdateTimeAsc":
                        blogs = context.OrderBy(o => o.CreateTime).ToList();
                        break;
                    case "GetBlogsSortByUpdateTimeDesc":
                        blogs = context.OrderBy(o => o.CreateTime).Reverse().ToList();
                        break;
                    default:
                        break;
                }
                return new ApiResponse($"已搜索到{blogs.Count}篇笔记.", true, blogs);
            }
            
        }

        /// <summary>
        /// 根据UserEntityID排序
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse> GetAllBlogsAndSortByUserEntityIDAsync()
        {
            var context = await _appContext.Blogs.ToListAsync();
            if (context is null || context.Count == 0)
                return new ApiResponse("数据库中并没有笔记.");
            List<BlogEntity> blogs = context.OrderBy(o => o.UserEntityID).ToList();
            return new ApiResponse($"已搜索到{blogs.Count}篇笔记.", true, blogs);
        }

        public async Task<ApiResponse> PostResponseAsync(BlogEntity e)
        {
            try
            {
                if (e == null)
                    return new ApiResponse("传入笔记不能为空.");
                _appContext.Blogs.Add(e);
                if (await _appContext.SaveChangesAsync() > 0)
                    return new ApiResponse("添加笔记成功！", true, e);
                return new ApiResponse("添加笔记失败！");
            }
            catch (Exception)
            {

                return new ApiResponse("发生错误，添加笔记失败");
            }
        }

        public async Task<ApiResponse> PutResponseAsync(BlogEntity e)
        {
            if (e == null) return new ApiResponse("传入笔记不能为空.");
            _appContext.Blogs.Update(e);
            await _appContext.SaveChangesAsync();
            return new ApiResponse("笔记已成功修改！", true, e);
        }

        public Task<ApiResponse> GetsResponseAsync()
        {
            throw new NotImplementedException();
        }
    }
}
