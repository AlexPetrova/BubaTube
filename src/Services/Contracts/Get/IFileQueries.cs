namespace Services.Contracts.Get
{
    public interface IFileQueries
    {
        PathInfo GenerateVideoPath(string fileExtension);

        string GetVideoPath(string fileName);
    }
}
