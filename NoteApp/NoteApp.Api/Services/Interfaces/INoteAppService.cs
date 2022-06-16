using NoteApp.Api.Models;

namespace NoteApp.Api.Services.Interfaces
{
    public interface INoteAppService
    {
        Task<ApiResponse> DeleteResponse(int id);
        Task<ApiResponse> GetResponse(int id);
        Task<ApiResponse> GetsResponse();
        Task<ApiResponse> PostResponse(UserEntity newUser);
        Task<ApiResponse> PutResponse(UserEntity newUser);
    }
}
