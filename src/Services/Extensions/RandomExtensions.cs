using System;

namespace Services.Extensions
{
    public static class RandomExtensions
    {
        public static string ShortString(this Random _)
        {
            return (DateTime.Now.Ticks - new DateTime(2016, 1, 1).Ticks).ToString("x");
        }
    }
}
