using System.IO;

namespace BubaTube.Factory.Contracts
{
    public interface IFileStreamFactory
    {
        FileStream CreateFileStreamInstance(string path, FileMode fileMode);
    }
}
