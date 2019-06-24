using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Dictafoule.Web.Test
{
    public class MyTestPostedFileBase : HttpPostedFileBase
    {
        Stream stream;
        string contentType;
        string fileName;

        public override string FileName
        {
            get { return fileName; }
        }
        public override int ContentLength
        {
            get { return (int)stream.Length; }
        }

        public override string ContentType
        {
            get { return contentType; }
        }

        public override Stream InputStream
        {
            get { return stream; }
        }

        public void SetFileName(string name)
        {
            this.fileName = name;
        }

        public MyTestPostedFileBase(Stream stream, string contentType, string fileName)
        {
            this.stream = stream;
            this.contentType = contentType;
            this.fileName = fileName;
        }
    }
}
