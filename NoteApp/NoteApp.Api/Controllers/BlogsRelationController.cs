using Microsoft.AspNetCore.Mvc;
using NoteApp.Api.Services;
using NoteApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NoteApp.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BlogsRelationController : ControllerBase
    {
        private readonly BlogsRelationService _blogsRelationService;

        public BlogsRelationController(BlogsRelationService blogsRelationService)
        {
            _blogsRelationService = blogsRelationService;
        }

        [HttpDelete]
        [Tags("博客归属管理")]
        public async Task<ApiResponse> DeleteBlogsRelationByIdAsync([FromQuery]int id) => await _blogsRelationService.DeleteResponseAsync(id);

        [HttpGet]
        [Tags("博客归属管理")]
        public async Task<ApiResponse> GetBlogRelationResponseAsync([FromQuery]int blogid, [FromQuery]int userid) => await _blogsRelationService.GetsResponseAsync(blogid, userid);

        [HttpGet]
        [Tags("博客归属管理")]
        public async Task<ApiResponse> GetBlogsRelationResponseAsync([FromQuery]int data, [FromQuery]string key) => await _blogsRelationService.GetsResponseAsync(data, key);

        [HttpPost]
        [Tags("博客归属管理")]
        public async Task<ApiResponse> PostBlogsRelationResponseAsync([FromBody]BlogsRelation e) => await _blogsRelationService.PostResponseAsync(e);
    }
}
