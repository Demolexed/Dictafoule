using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Collections.Specialized;
using System.Runtime.Serialization;
using System.IO;
using System.Threading;
using System.Configuration;


namespace SpeechToText.WebJob
{
    class SpeechmaticsClient
    {
        private WebClient wc;
        private Uri baseUri;
        private int userId;
        private string authToken;

        public SpeechmaticsClient()
        {
            wc = new WebClient();
            baseUri = new Uri(ConfigurationManager.AppSettings["SPEECHMATICS_API_URL"]);
            userId = Int32.Parse(ConfigurationManager.AppSettings["SPEECHMATICS_LOGIN"]);
            authToken = ConfigurationManager.AppSettings["SPEECHMATICS_API_PASSWORD"];
        }

        public CreateJobResponse CreateTranscriptionJob(string audioFilename, Stream stream, bool diarise)
        {
            try
            {
                Uri uploadUri = createUserRelativeUri("/jobs/");
                string jsonResponse = FileUpload.UploadFileForTranscription(uploadUri, Path.GetFileName(audioFilename), stream, ConfigurationManager.AppSettings["SPEECHMATICS_LANGUAGE"], new NameValueCollection(), diarise);
                dynamic jobJson = JsonConvert.DeserializeObject(jsonResponse);
                return new CreateJobResponse((int)jobJson.id, (int)jobJson.cost, (int)jobJson.balance);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Answer UpdateJobStatus(Answer answer)
        {
            Uri uploadUri = createUserRelativeUri(String.Format("/jobs/{0}/", answer.Id));
            dynamic jobJson = getJson(uploadUri);
            if (jobJson != null)
            {
                answer.Name = jobJson.job.name;
                answer.Status = jobJson.job.job_status;
                return answer;
            }
            else
            {
                return null;
            }
        }
        public string getTranscript(Answer answer, string format)
        {
            NameValueCollection reqParams = new NameValueCollection();
            reqParams.Add("format", format);
            Uri uploadUri = createUserRelativeUri(String.Format("/jobs/{0}/transcript", answer.Id), reqParams);

            return getString(uploadUri);
        }

        private Uri createUserRelativeUri(String path, NameValueCollection requestParams = null)
        {
            if (requestParams == null)
            {
                requestParams = new NameValueCollection();
            }
            requestParams.Add("auth_token", authToken);
            String paramString = "?";
            foreach (string name in requestParams.Keys)
            {
                paramString += name + "=" + requestParams[name] + "&";
            }
            return new Uri(baseUri, String.Format("/v1.0/user/{0}{1}{2}", userId.ToString(), path, paramString));
        }
        public string getSpeech(Answer answer)
        {
            string result = String.Empty;
            while ((answer = UpdateJobStatus(answer)) != null)
            {
                if (answer.Status == "done")
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(getTranscript(answer, "txt"));
                    result = Encoding.UTF8.GetString(buffer);
                    break;
                }
                Thread.Sleep(5000);
            }
            return result;
        }

        private dynamic getJson(Uri uri)
        {
            string resp = getString(uri);
            return resp == null ? null : JsonConvert.DeserializeObject(resp);
        }

        private string getString(Uri uri)
        {
            try
            {
                WebRequest request = WebRequest.Create(uri);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    return sr.ReadToEnd();
                }
            }
            catch (WebException)
            {
                return null;
            }
        }

        public User GetUser()
        {
            Uri userUri = createUserRelativeUri("/");
            dynamic userJson = getJson(userUri);
            return new User((int)userJson.user.id, (string)userJson.user.email, (int)userJson.user.balance);
        }
    }
}
