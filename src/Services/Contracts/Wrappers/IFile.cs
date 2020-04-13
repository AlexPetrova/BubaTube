namespace Services.Contracts.Wrappers
{
    public interface IFile
    {
        bool Exists(string path);

        void Delete(string path);
    }
}
