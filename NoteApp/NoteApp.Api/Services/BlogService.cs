using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NoteApp.Api.Data;
using NoteApp.Api.Services.Interfaces;
using NoteApp.Models;

namespace NoteApp.Api.Services
{
    public class BlogService : GenericInterface<ApiResponse, BlogEntity>
    {
        private readonly NoteAppContext _noteAppContext;

        public BlogService(NoteAppContext noteAppContext)
        {
            _noteAppContext = noteAppContext;
        }

        public async Task<ApiResponse> DeleteResponseAsync(int id)
        {
            try
            {
                BlogEntity blog = await FindEntityByIdAsync(id);

                if (blog != null)
                {
                    _noteAppContext.Blogs.Remove(blog);
                    if (await _noteAppContext.SaveChangesAsync() > 0)
                    {
                        return new ApiResponse("成功删除该用户", true);
                    }
                    else
                        throw new Exception("数据删除失败.");
                }
                else
                    throw new Exception($"搜索不到ID为{id}的博客.");
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-BlogService-DeleteResponseAsync-{ex.Message}");
            }
        }

        public async Task<ApiResponse> GetResponseAsync(int id)
        {
            try
            {
                BlogEntity blog = await FindEntityByIdAsync(id);
                if (blog != null)
                {
                    return new ApiResponse("成功搜索到用户", true, blog);
                }
                else
                    throw new Exception($"搜索不到ID为{id}的博客.");
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-BlogService-GetResponseAsync-{ex.Message}");
            }
        }

        public async Task<ApiResponse> GetsResponseAsync()
        {
            try
            {
                if (!_noteAppContext.Blogs.Any())
                    throw new Exception("数据库用并没有博客.");
                ICollection<BlogEntity> blogs = await _noteAppContext.Blogs.ToListAsync();
                return new ApiResponse($"数据库中有{blogs.Count}篇博客.", true, blogs);
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-BlogService-GetsResponseAsync-{ex.Message}");
            }
        }

        public async Task<ApiResponse> GetsResponseAsync(string arg, string key)
        {
            try
            {
                return new ApiResponse(true, await SearchEntityAsync(arg, key));
            }
            catch (Exception ex)
            {

                return new ApiResponse($"Api-BlogService-GetsResponseAsync-{ex.Message}");
            }
        }

        public async Task<ApiResponse> PostResponseAsync(BlogEntity e)
        {
            try
            {
                if (e is null)
                {
                    throw new Exception("传入博客内容不能为空!");
                }
                if (MemberIsNotNull(e,2345))
                {
                    await _noteAppContext.Blogs.AddAsync(e);
                    if(await _noteAppContext.SaveChangesAsync() > 0)
                    {
                        return new ApiResponse("博客已成功上传.", true, e);
                    }
                    else
                        throw new Exception("保存数据失败.");
                }else
                    throw new Exception("传入的数据可能为空");
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-BlogService-PostResponseAsync-{ex.Message}.");
            }
        }

        public async Task<ApiResponse> PutResponseAsync(BlogEntity e)
        {
            try
            {
                if (e is null)
                {
                    throw new Exception("传入博客容为空!");
                }
                if (MemberIsNotNull(e, 2345))
                {
                    _noteAppContext.Blogs.Update(e);
                    if (await _noteAppContext.SaveChangesAsync() > 0)
                    {
                        return new ApiResponse("博客已成功修改.", true, e);
                    }
                    else
                        throw new Exception("保存数据失败.");
                }
                else
                    throw new Exception("传入的数据可能为空");
            }
            catch (Exception ex)
            {
                return new ApiResponse($"Api-BlogService-PutResponseAsync-{ex.Message}.");
            }
        }

        public async Task<BlogEntity> FindEntityByIdAsync(int id)
        {
            try
            {
                BlogEntity? blog = await _noteAppContext.Blogs.FindAsync(id);
                if (blog is not null)
                    return blog;
                else
                    throw new Exception($"FindEntityByIdAsync-找不到ID为{id}的博客.");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<bool> FindAnyAsync(object obj, string arg)
        {
            throw new NotImplementedException();
        }

        public bool MemberIsNotNull(BlogEntity e, int key)
        {
            switch (key)
            {
                case 2345:
                    if (string.IsNullOrWhiteSpace(e.Title))
                        throw new Exception("MemberIsNotNull-标题不能为空.");
                    if (string.IsNullOrWhiteSpace(e.PictrueLink))
                        throw new Exception("MemberIsNotNull-图片链接不能为空.");
                    if (string.IsNullOrWhiteSpace(e.Context))
                        throw new Exception("MemberIsNotNull-博客正文不能为空.");
                    if (string.IsNullOrWhiteSpace(e.Classification))
                        throw new Exception("MemberIsNotNull-博客分类不能为空.");
                    return true;
                default:
                    return false;
            }
        }

        public Task<bool> DataIsNotDuplicateAsync(BlogEntity e, int arg)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<BlogEntity>> SearchEntityAsync(string arg, string key)
        {
            try
            {
                if (string.IsNullOrEmpty(arg))
                    throw new Exception($"传入{key}数据为空值.");
                switch (key)
                {
                    case "Title":
                        ICollection<BlogEntity> blogs_Title = await _noteAppContext.Blogs.Where(o => o.Title == arg).ToListAsync();
                        if (blogs_Title.Count > 0)
                        {
                            return blogs_Title;
                        }
                        else
                            throw new Exception($"找不到{key}为{arg}的博客.");
                    case "Enums":
                        ICollection<BlogEntity> users_Classification = await _noteAppContext.Blogs.Where(o => o.Classification == arg).ToListAsync();
                        if (users_Classification.Count > 0)
                        {
                            return users_Classification;
                        }
                        else
                            throw new Exception($"找不到{key}为{arg}的博客.");
                    default:
                        throw new Exception("获取数据失败.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"SearchEntityAsync-{ex.Message}");
            }
        }
    }
}
