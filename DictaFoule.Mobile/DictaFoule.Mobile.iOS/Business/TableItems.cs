using System;
using System.Threading.Tasks;
using DictaFoule.Mobile.iOS.API;
using DictaFoule.Mobile.iOS.Business;
using Foundation;

namespace DictaFoule.Mobile.iOS
{
    public class Sound : Base
    {
        #region attribut

        public string Name { get; set; }

        public string Date { get; set; }
        public string Time { get; set; }
        public int IdProject { get; set; }
        public SoundState State { get; set; }
        public NSUrl Pathfile { get; set; }
        public string Transcription { get; set; }
        private HttpClientService ClientService { get; set; }

        #endregion

        public Sound() { }

        public Sound(string heading) : base()
        { 
            Name = heading;
            State = SoundState.New;
            this.ClientService = new HttpClientService();
        }

        public async void GetIdProject(string name, string guid)
        {
            try
            {
                var response = await ClientService.GetService<int>("Project/GetIdProject?nameFile=" + name + "&guidElements=" + guid);
                this.IdProject = response;
            }
            catch (RequestException ex)
            {
                
            }

        }

        public async void GetStateProject(string guid)
        {
            try
            {
                var response = await ClientService.GetService<int>("Project/GetStateProject?id_project=" + IdProject.ToString() + "&guidElements=" + guid);
                this.State = (SoundState)(response);
            }
            catch(RequestException ex)
            {

            }
            
        }

        public async void GetTranscriptProject(string guid)
        {
            var response = await ClientService.GetService<string>("Project/GetTranscrib?id_project=" + IdProject.ToString() + "&guidElements=" + guid);
            this.Transcription = response;
        }

        public bool IsWaiting
        {
            get {
                return this.State == SoundState.Upload || this.State == SoundState.SpeechToText
               || this.State == SoundState.TextToTask;
            }
            
        }


        public bool IsError
        {
            get {
                return this.State == SoundState.ErrorSoundCut || this.State == SoundState.ErrorSpeechToText
               || this.State == SoundState.ErrorTextToTask;
            }

        }

        public void Update()
        {
            this.GetIdProject(this.Name, this.Guid);
            this.GetStateProject(this.Guid);
        }
    }
}