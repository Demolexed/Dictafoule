using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictaFoule.Common.Tools
{
    public class DecodeEncodeTo64
    {
        public static string EncodeTo64(string toEncode)
        {
            var toEncodeAsBytes = Encoding.UTF8.GetBytes(toEncode);
            var returnValue = Convert.ToBase64String(toEncodeAsBytes);
            return returnValue;
        }

        public static Stream DecodeFrom64(string encodedData)
        {
            encodedData = encodedData.Replace(" ", "+");
            byte[] bfile = Convert.FromBase64String(encodedData);
            Stream file = new MemoryStream(bfile);
            return file;
        }
    }
}
