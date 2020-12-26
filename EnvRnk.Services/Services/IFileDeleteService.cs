namespace EnvRnk.Services.Services
{
    public interface IFileDeleteService
    {
        FileDeleteResult DeleteFile(string url);
    }
}