namespace ADB.Blogger.Infrastructure.Storage
{
    public interface IBloggerStorage
    {
        Task InitialiseStorage();  // check storage is there, and creates if not

        Task UploadFile(string path, string content, string contentType);
        Task DownloadFile(string path, string content, string contentType);
        Task<IEnumerable<string>> ListFiles();
        Task<bool> DeleteFile(string path);
        Task<bool[]> DeleteFiles(string[] paths);
        Task<Uri> GetFilePublicUri(string path);

    }
}
