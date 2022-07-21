using NoteApp.Api.Data;
using NoteApp.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using NoteApp.Models;

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

        public async Task<ApiResponse> GetsResponseAsync()
        {
            var context = await _appContext.Blogs.ToListAsync();
            if (context is null || context.Count == 0)
                return new ApiResponse("数据库中并没有笔记.");
            return new ApiResponse($"数据库中有{context.Count}篇笔记.", true, context);
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
    }
}
