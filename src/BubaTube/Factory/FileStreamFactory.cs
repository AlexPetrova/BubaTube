using BubaTube.Factory.Contracts;
using System.IO;

namespace BubaTube.Factory
{
    public class FileStreamFactory : IFileStreamFactory
    {
        public FileStream CreateFileStreamInstance(string path, FileMode fileMode)
        {
            return new FileStream(path, fileMode);
        }
    }
}
