using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NoteApp.Api.Services;
using NoteApp.Models;

namespace NoteApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BlogService _blogService;

        public BlogController(BlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpDelete]
        [Tags("博客管理")]
        public async Task<ApiResponse> DeleteBlogByIdAsync([FromQuery]int id) => await _blogService.DeleteResponseAsync(id);

        [HttpGet]
        [Tags("博客管理")]
        public async Task<ApiResponse> GetBlogByIdAsync([FromQuery]int id) => await _blogService.GetResponseAsync(id);

        [HttpGet]
        [Tags("博客管理")]
        public async Task<ApiResponse> GetBlogsAsync() => await _blogService.GetsResponseAsync();

        [HttpGet]
        [Tags("博客管理")]
        public async Task<ApiResponse> SearchBlogsAsync([FromQuery]string arg, [FromQuery]string key) => await _blogService.GetsResponseAsync(arg, key);

        [HttpPost]
        [Tags("博客管理")]
        public async Task<ApiResponse> AddBlogAsync([FromBody]BlogEntity blog) => await _blogService.PostResponseAsync(blog);

        [HttpPut]
        [Tags("博客管理")]
        public async Task<ApiResponse> ModifyBlogAsync([FromBody]BlogEntity blog) => await _blogService.PutResponseAsync(blog);
    }
}
