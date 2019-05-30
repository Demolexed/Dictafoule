using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
using NLayer;
using System.IO;
using DictaFoule.Common.Tools;
using DictaFoule.Common.DAL;
using DictaFoule.Common.Enum;

namespace SoundCut.WebJob
{
    class CutSound
    {
        public static void CutMp3(Stream stream, project project)
        {
            Mp3FileReader reader = new Mp3FileReader(stream, new Mp3FileReader.FrameDecompressorBuilder(waveFormat => new NLayer.NAudioSupport.Mp3FrameDecompressor(waveFormat)));
            Mp3Frame mp3Frame = reader.ReadNextFrame();
            var entities = new Entities();

            int i = 1;
            int countByte;
            int timeToCut = 59;
            int nbByteInMin = mp3Frame.BitRate / 8 * timeToCut;
            int id_project = 0;

            while (mp3Frame != null)
            {
                countByte = 0;
                var fileNameOutput = string.Format("project-{0}-{1}.mp3", project.id_project, i);
                FileStream fileStream = new FileStream(fileNameOutput, FileMode.Create, FileAccess.ReadWrite);
                while (mp3Frame != null && countByte < nbByteInMin)
                {
                    fileStream.Write(mp3Frame.RawData, 0, mp3Frame.RawData.Length);
                    countByte += mp3Frame.FrameLength;
                    mp3Frame = reader.ReadNextFrame();
                }
                fileStream.Position = 0;
                AzureBlobStorage.Upload(fileStream, "audio/mpeg", fileNameOutput, "soundline");
                fileStream.Close();
                var soundline = new sound_line
                {
                    id_project = project.id_project,
                    sound_file_name = fileNameOutput,
                    sound_file_uri = project.import_sound_file_uri,
                    creation_date = DateTime.UtcNow,
                    state = (int)SoundLineState.Create,
                };
                id_project = soundline.id_project;
                entities.sound_line.Add(soundline);
                entities.SaveChanges();
                i++;
            }
            
            AzureQueueStorage.QueueProject(id_project, "speechtotext");
        }

        public static void CutWav(Stream stream, project project)
        {
            int i = 1;
            TimeSpan cutFromStart = new TimeSpan(0, 0, 0);
            TimeSpan cutFromEnd = new TimeSpan(0, 0, 59);
            var entities = new Entities();
            int position = 0;
            int id_project = 0;
            
            using (WaveFileReader reader = new WaveFileReader(stream))
            {
                int bytesPerMillisecond = reader.WaveFormat.AverageBytesPerSecond / 1000;
                while (position < reader.Length)
                {
                    var fileNameOutput = string.Format("project-{0}-{1}.wav", project.id_project, i);
                    using (WaveFileWriter writer = new WaveFileWriter(fileNameOutput, reader.WaveFormat))
                    {
                        int startPos = (int)cutFromStart.TotalMilliseconds * bytesPerMillisecond;
                        startPos = startPos - startPos % reader.WaveFormat.BlockAlign;

                        int endBytes = (int)cutFromEnd.TotalMilliseconds * bytesPerMillisecond;
                        endBytes = endBytes - endBytes % reader.WaveFormat.BlockAlign;
                        int endPos = (int)reader.Position + endBytes;

                        position = TrimWavFile(reader, writer, startPos, endPos);
                        
                    }
                    FileStream ms = new FileStream(fileNameOutput, FileMode.Open);
                    AzureBlobStorage.Upload(ms, "audio/mpeg", fileNameOutput, "soundline");
                    var soundline = new sound_line
                    {
                        id_project = project.id_project,
                        sound_file_name = fileNameOutput,
                        sound_file_uri = project.import_sound_file_uri,
                        creation_date = DateTime.UtcNow,
                        state = (int)SoundLineState.Create,
                    };
                    id_project = soundline.id_project;
                    entities.sound_line.Add(soundline);
                    entities.SaveChanges();
                    i++;
                    cutFromStart = cutFromStart.Add(cutFromEnd);
                }
            }
            AzureQueueStorage.QueueProject(id_project, "speechtotext");
        }

        private static int TrimWavFile(WaveFileReader reader, WaveFileWriter writer, int startPos, int endPos)
        {
            reader.Position = startPos;
            byte[] buffer = new byte[1024];
            while (reader.Position < reader.Length && reader.Position < endPos)
            {
                int bytesRequired = (int)(endPos - reader.Position);
                if (bytesRequired > 0)
                {
                    int bytesToRead = Math.Min(bytesRequired, buffer.Length);
                    int bytesRead = reader.Read(buffer, 0, bytesToRead);
                    if (bytesRead > 0)
                    {
                        writer.Write(buffer, 0, bytesRead);
                    }
                    Console.WriteLine(reader.Position);
                }
            }
            return (int)reader.Position;
        }
    }
}
