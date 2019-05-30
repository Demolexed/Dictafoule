using System.IO;

namespace DictaFoule.Common.Tools
{
    public static class DataValidation
    {
        public static bool IsWav(string FileName)
        {
            return (Path.GetExtension(FileName) == ".wav");
        }

        public static bool IsMp3(string FileName)
        {
            return (Path.GetExtension(FileName) == ".mp3");
        }
    }
}
