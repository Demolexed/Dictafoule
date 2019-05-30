using System.Security.Cryptography;
using System.Text;

namespace DictaFoule.Common.Tools
{
    public static class Encrypt
    {
        public static string GetMd5Hash(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var md5Hasher = MD5.Create();
                var data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(input));
                var sBuilder = new StringBuilder();
                for (var i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }
                return sBuilder.ToString();
            }
            return string.Empty;
        }
    }
}
