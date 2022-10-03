using ECommerceAPI.Application.Abstractions.Storage;
using Microsoft.AspNetCore.Http;

namespace ECommerceAPI.Infrastructure.Services.Storage
{
    public class StorageService : IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public string StorageName { get => _storage.GetType().Name; }

        public async Task DeleteAsync(string pathOrContainer, string fileName)
        {
            await _storage.DeleteAsync(pathOrContainer,fileName);
        }

        public List<string> GetFiles(string pathOrContainer)
        {
            return _storage.GetFiles(pathOrContainer);    
        }

        public bool HasFile(string pathOrContainer, string fileName)
        {
            return _storage.HasFile(pathOrContainer,fileName);
        }

        public async Task<List<(string fileName, string pathOrContainer)>> UploadAsync(string pathOrContainer, IFormFileCollection files)
        {
            return await _storage.UploadAsync(pathOrContainer, files);
        }
    }
}
