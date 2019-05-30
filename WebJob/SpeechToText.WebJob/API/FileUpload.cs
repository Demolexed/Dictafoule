using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Text;
using System.Net;

namespace SpeechToText.WebJob
{
    class FileUpload
    {
        public static String UploadFileForTranscription(Uri uploadUri, string filename, Stream fileStream, string lang, NameValueCollection values, bool diarise)
        {
            WebRequest request = WebRequest.Create(uploadUri);
            request.Method = "POST";
            String boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x", NumberFormatInfo.InvariantInfo);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            boundary = "--" + boundary;

            using (var requestStream = request.GetRequestStream())
            {
                byte[] buffer;

                foreach (string name in values.Keys)
                {
                    buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", name, Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(values[name] + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                if (!diarise)
                {
                    buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", "diarisation", Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                    buffer = Encoding.ASCII.GetBytes(string.Format("false" + Environment.NewLine));
                    requestStream.Write(buffer, 0, buffer.Length);
                }

                buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"{1}{1}", "model", Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.ASCII.GetBytes(string.Format(lang + Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);

                buffer = Encoding.ASCII.GetBytes(boundary + Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"{2}", "data_file", filename, Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);
                buffer = Encoding.ASCII.GetBytes(string.Format("Content-Type: {0}{1}{1}", "application/octet-stream", Environment.NewLine));
                requestStream.Write(buffer, 0, buffer.Length);


                fileStream.CopyTo(requestStream);
                buffer = Encoding.ASCII.GetBytes(Environment.NewLine);
                requestStream.Write(buffer, 0, buffer.Length);

                buffer = Encoding.ASCII.GetBytes(boundary + "--");
                requestStream.Write(buffer, 0, buffer.Length);
            }
            try
            {
                using (WebResponse response = request.GetResponse())
                using (Stream responseStream = response.GetResponseStream())
                using (MemoryStream stream = new MemoryStream())
                {
                    responseStream.CopyTo(stream);
                    return Encoding.UTF8.GetString(stream.ToArray());
                }
            }
            catch (WebException)
            {
                return null;
            }
        }
    }
}
