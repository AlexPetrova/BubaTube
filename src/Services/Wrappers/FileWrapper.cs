using Services.Contracts.Wrappers;
using System.IO;

namespace Services.Wrappers
{
    public class FileWrapper : IFile
    {
        public bool Exists(string path) => File.Exists(path);

        public void Delete(string path) => File.Delete(path);
    }
}
