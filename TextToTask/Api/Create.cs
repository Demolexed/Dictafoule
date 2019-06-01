using FouleFactoryApi.PCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DictaFoule.Common.DAL;
using DictaFoule.Common.Tools;
using DictaFoule.Common.Enum;
using FouleFactoryApi.PCL.Models;
using Newtonsoft.Json;
using TextToTask.Models;

namespace TextToTask.WebJob.Api
{
    class Create
    {
        public static int? CreateTemplate(FouleFactoryApiClient fouleFactory)
        {
            try
            {
                var instructions = new List<TemplateInstructionWriterServiceModel>();

                instructions.Add(new TemplateInstructionWriterServiceModel
                {
                    Order = 1,
                    Instruction = "Bonjour,\nLe but de ce projet est de corriger une transcription audio. Voici la retranscription de la séquence audio ci-dessous.\nMerci d'effectuer un copier/coller dans le champs de texte et de corriger les erreurs ainsi que les fautes de frappe et d'orthographe (il est parfois nécessaire de transcrire complètement une séquence).\nN'hésitez pas à réécouter plusieurs fois la séquence audio si nécessaire.\nSeuls les travaux sérieux seront validés.\n"
                });

                var columns = new List<TemplateColumnWriterServiceModel>();

                columns.Add(new TemplateColumnWriterServiceModel
                {
                    Column = 1,
                    Order = 2,
                    IdTemplateColumnType = 2
                });

                columns.Add(new TemplateColumnWriterServiceModel
                {
                    Column = 2,
                    Order = 3,
                    IdTemplateColumnType = 1
                });

                var questions = new List<TemplateQuestionWriterServiceModel>();

                questions.Add(new TemplateQuestionWriterServiceModel
                {
                    Title = "Transcription à corriger",
                    Option = " ",
                    IdTemplateObjectQuestionType = 2,
                    Require = true,
                    Order = 4
                });

                var template = new TemplateNewWriterServiceModel
                {
                    Title = "Relecture",
                    Description = "DictaFoule",
                    IdProjectType = 2,
                    Instructions = instructions,
                    Columns = columns,
                    Questions = questions
                };

                var idTemplate = fouleFactory.Templates.CreateTemplatesCreateTemplate(template);
                return (int)idTemplate;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static int? CreateProject(FouleFactoryApiClient fouleFactory, int idTemplate)
        {
            try
            {
                ProjectWriterServiceModel project = new ProjectWriterServiceModel
                {
                    Title = "Relecture",
                    IdTemplate = (int)idTemplate,
                    MaxEndDate = DateTime.UtcNow.AddDays(15),
                    NbSupplierPerTask = 1,
                    EstimatedTimePerTask = "00:01:00",
                    AmountWithoutTaxPerTask = 25,
                    AutomaticValidation = false,
                    UrlNotification = "http://ff-prod-dictafoule.azurewebsites.net/Project/Notification",
                    IdCertification = 3
                };
                var projectJson = fouleFactory.Projects.CreateProjectsCreateProject(project);
                FouleFactoryProjectModel projectInfos = JsonConvert.DeserializeObject<FouleFactoryProjectModel>(projectJson.ToString());
                return (projectInfos.IdProject);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static int? AddTaskLines(FouleFactoryApiClient fouleFactory, string filename, string text, int idProject)
        {
            try
            {
                if (String.IsNullOrEmpty(text))
                    text = "Il n'y a aucun texte à retranscrire";
                TaskLinesWriterServiceModel taskLine = new TaskLinesWriterServiceModel
                {
                    IdProject = idProject,
                    TaskColumns = new List<string> { AzureBlobStorage.Get(filename, "soundline"), text }
                };
                var taskLineJson = fouleFactory.TaskLines.CreateTaskLinesCreateTaskLine(taskLine);
                List<FouleFactoryTaskModel> taskLineInfos = JsonConvert.DeserializeObject<List<FouleFactoryTaskModel>>(taskLineJson.ToString());
                return taskLineInfos.FirstOrDefault().IdTaskLine;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public static int? AddcsvFile(FouleFactoryApiClient fouleFactory, List<sound_line> soundlines, int idProject )
        {
            var csv = string.Empty;

            foreach (var sound in soundlines)
            {
                csv += AzureBlobStorage.Get(sound.name, "soundline") + ";" + sound.transcript;
            }


            var csvEncodeAsBytes = Encoding.UTF8.GetBytes(csv);
            var csvBase64String = Convert.ToBase64String(csvEncodeAsBytes);

            CsvFileWriterServiceModel csvFile = new CsvFileWriterServiceModel
            {
                IdProject = idProject,
                File = csvBase64String,
                Header = false,
                Separator = ";"
            };


            var csvresultJson = fouleFactory.CsvFiles.CreateCsvFilesCreateCsvFile(csvFile);
            var csvresult = JsonConvert.DeserializeObject<FouleFactoryCsvModel>(csvresultJson.ToString());
            return csvresult.IdCsvFile;
        }

        public static int GetWallet(FouleFactoryApiClient fouleFactory)
        {
            var jsonWallet = fouleFactory.Account.GetAccountGetWallet();
            FouleFactoryWalletModel wallet = JsonConvert.DeserializeObject<FouleFactoryWalletModel>(jsonWallet.ToString());
            return (wallet.AmountWithoutTax);
        }

        public static bool IsEnoughtMoney(int count_cv, FouleFactoryApiClient fouleFactory)
        {
            try
            {
                var project_amount = 0.25 * 1.2 * count_cv;
                var wallet = GetWallet(fouleFactory);
                if (wallet > project_amount)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }
    }
}
