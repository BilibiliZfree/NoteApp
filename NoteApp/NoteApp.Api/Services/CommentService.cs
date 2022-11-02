using NoteApp.Api.Data;
using NoteApp.Api.Services.Interfaces;
using NoteApp.Models;

namespace NoteApp.Api.Services
{
    public class CommentService : GenericInterface<ApiResponse, CommentEntity>
    {
        private readonly NoteAppContext _noteAppContext;

        public CommentService(NoteAppContext noteAppContext)
        {
            _noteAppContext = noteAppContext;
        }

        public Task<ApiResponse> DeleteResponseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetResponseAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetsResponseAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> GetsResponseAsync(string arg, string switch_on)
        {
            
            throw new NotImplementedException();
        }

        public Task<ApiResponse> PostResponseAsync(CommentEntity e)
        {
            throw new NotImplementedException();
        }

        public Task<ApiResponse> PutResponseAsync(CommentEntity e)
        {
            throw new NotImplementedException();
        }

        public Task<CommentEntity> FindEntityByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FindAnyAsync(object obj, string arg)
        {
            throw new NotImplementedException();
        }

        public bool MemberIsNotNull(CommentEntity e, int key)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DataIsNotDuplicateAsync(CommentEntity e, int arg)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<CommentEntity>> SearchEntityAsync(string arg, string key)
        {
            throw new NotImplementedException();
        }
    }
}
