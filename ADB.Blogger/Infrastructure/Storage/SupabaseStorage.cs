
namespace ADB.Blogger.Infrastructure.Storage
{
    public class SupabaseStorage : IBloggerStorage
    {
        public Task<bool> DeleteFile(string path)
        {
            throw new NotImplementedException();
        }

        public Task<bool[]> DeleteFiles(string[] paths)
        {
            throw new NotImplementedException();
        }

        public Task DownloadFile(string path, string content, string contentType)
        {
            throw new NotImplementedException();
        }

        public Task<Uri> GetFilePublicUri(string path)
        {
            throw new NotImplementedException();
        }

        public Task InitialiseStorage()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<string>> ListFiles()
        {
            throw new NotImplementedException();
        }

        public Task UploadFile(string path, string content, string contentType)
        {
            throw new NotImplementedException();
        }
    }
}
