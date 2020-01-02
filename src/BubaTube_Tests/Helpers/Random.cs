using System.Linq;

namespace BubaTube_Tests.Helpers
{
    public static class Random
    {
        private static readonly System.Random random = new System.Random();

        public static string String(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable
                .Repeat(chars, length)              
                .Select(s => s[random.Next(s.Length)])
                .ToArray());
        }

        public static int Number()
        {
            return random.Next();
        }
    }
}
