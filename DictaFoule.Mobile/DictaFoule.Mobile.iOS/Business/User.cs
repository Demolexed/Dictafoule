using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using AVFoundation;
using DictaFoule.Mobile.iOS.API;
using Foundation;
using Newtonsoft.Json;
using UIKit;

namespace DictaFoule.Mobile.iOS.Business
{
    public class User : Base
    {
        #region attribut

        /// <summary>
        /// List of sounds
        /// </summary>
        /// <value>The sounds.</value>
        public List<Sound> Sounds { get; set; }

        /// <summary>
        /// Client service
        /// </summary>
        /// <value>The client service.</value>
        private HttpClientService clientService { get; set; }

        #endregion

        public User() : base()
        {
            this.Sounds = new List<Sound>();
            this.clientService = new HttpClientService();

            this.GetFiles();
        }

        public User(List<Sound> sounds, string guid)
        {
            this.Sounds = sounds;
            this.Guid = guid;
            this.clientService = new HttpClientService();
        }

        public async void GetUser()
        {

            var response = await clientService.GetService<string>("User/GetUser?guidElements=" + this.Guid);
        }

        public async void CreateUser()
        {

            var userModel = new UserModel
            {
                Guid = this.Guid
            };

            var query = JsonConvert.SerializeObject(userModel);
            await this.clientService.PostService<bool>("User/CreateUser", new StringContent(query, Encoding.Unicode, "application/json"));
        }

        public void GetFiles()
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var files = Directory.GetFiles(path);
            NSError err = new NSError();
            foreach (var file in files)
            {
                var audiopathfile = NSUrl.FromFilename(file);
                var filename = Path.GetFileName(file);
                var audiofile = AudioToolbox.AudioFile.Open(audiopathfile, AudioToolbox.AudioFilePermission.Read, AudioToolbox.AudioFileType.WAVE);
                if (audiofile != null)
                {
                    var fileaudio = new AVAudioPlayer(audiopathfile, "wav", out err);
                    FileInfo info = new FileInfo(file);
                    Sounds.Add(new Sound(filename)
                    {
                        Date = info.CreationTime.ToString("dd/MM") + " - ",
                        Time = TimeSpan.FromSeconds(fileaudio.Duration).ToString(@"hh\:mm\:ss"),
                        Pathfile = audiopathfile
                    });
                }

            }
        }
    }
}
