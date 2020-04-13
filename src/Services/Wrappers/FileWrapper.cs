using System.IO;

namespace Services.Wrappers
{
    public class FileWrapper
    {
        public bool Exists(string path)
        {
            return File.Exists(path);
        }

        public void Delete(string path)
        {
            File.Delete(path);
        }
    }
}
