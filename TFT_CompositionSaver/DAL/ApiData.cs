using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TFT_CompositionSaver.Helper;
using TFT_CompositionSaver.Models.API;
using TFT_CompositionSaver.Models.API.ChampionData;
using TFT_CompositionSaver.Models.API.ItemData;
using TFT_CompositionSaver.Properties;

namespace TFT_CompositionSaver.DAL
{
    public class ApiData
    {
        private static ApiData instance;

        // API data locations
        private static string dataBasePath = "http://raw.communitydragon.org/latest/game";
        private static string champSquarePath = "https://cdn.communitydragon.org/latest/champion/{0}/square.png";

        // Local data locations
        private static string baseFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TFTCompositionSaver");
        private static string championsPath = Path.Combine(baseFilePath, "Champions");
        private static string itemsPath = Path.Combine(baseFilePath, "Items");
        private static string traitsPath = Path.Combine(baseFilePath, "Traits");

        private GameData data;

        private ApiData()
        {
            this.SetGameData();
            this.WaitForFiles().GetAwaiter().GetResult();
        }

        private async Task WaitForFiles()
        {
            List<Task> tasks = this.SetImages(this.data.champions);
            tasks.AddRange(this.SetImages(this.data.items));
            tasks.AddRange(this.SetImages(this.data.traits));
            await Task.WhenAll(tasks);
        }

        private void SetGameData()
        {
            byte[] jsonFile = Resources.TFT;
            MemoryStream ms = new MemoryStream(jsonFile);
            using (StreamReader r = new StreamReader(ms))
            {
                string json = r.ReadToEnd();
                data = JsonConvert.DeserializeObject<GameData>(json);
            }
        }

        private List<Task> SetImages(IResourceModel[] data)
        {
            List<Task> tasks = new List<Task>();

            for (int i = 0; i < data.Length; i++)
            {
                int index = i;

                string filePath = "";
                string urlPath = "";
                string directoryPath = "";

                switch (data)
                {
                    case Champion[] champs:
                        filePath = Path.Combine(championsPath, champs[index].id + ".png");
                        urlPath = string.Format(champSquarePath, champs[index].id);
                        directoryPath = championsPath;
                        break;
                    case Item[] items:
                        string itemUrlPath = items[index].icon.Replace(".dds", ".png").ToLower();
                        string itemFilePath = itemUrlPath.Substring(itemUrlPath.LastIndexOf('/') + 1);
                        filePath = Path.Combine(itemsPath, itemFilePath);
                        urlPath = string.Join("/", dataBasePath, itemUrlPath).ToLower();
                        directoryPath = itemsPath;
                        break;
                    case Trait[] traits:
                        string traitUrlPath = traits[index].icon.Replace(".dds", ".png").ToLower();
                        string traitFilePath = traitUrlPath.Substring(traitUrlPath.LastIndexOf('/') + 1);
                        filePath = Path.Combine(traitsPath, traitFilePath);
                        urlPath = string.Join("/", dataBasePath, traitUrlPath).ToLower();
                        directoryPath = traitsPath;
                        break;
                }
                Task task = this.TaskToExecute(data[index], filePath, urlPath, directoryPath);
                tasks.Add(task);
            }

            return tasks;
        }

        private Task TaskToExecute(IResourceModel data, string filePath, string urlPath, string directoryPath)
        {
            Task task = Task.Run(() =>
            {
                try
                {
                    // Get the image from the Existing directory, create it new or read it from URL
                    data.image = this.GetImageFromSource(filePath, urlPath, directoryPath);
                }
                catch (Exception ex)
                {
                    // If no internet connection, and file is not available, show text instead
                    data.image = ApiDataHelper.CreateTextImage(data.name);
                }
            });
            return task;
        }

        private Image GetImageFromSource(string filePath, string urlPath, string directoryPath)
        {
            // Use already downloaded file if exist
            if (File.Exists(filePath))
            {
                return Image.FromFile(filePath);
            }

            using (WebClient webClient = new WebClient())
            {
                ApiDataHelper.CreateFolder(directoryPath);
                webClient.DownloadFile(new Uri(urlPath), filePath);

                // After downloading file, use it
                if (File.Exists(filePath))
                {
                    return Image.FromFile(filePath);
                }

                // If downloading file is not allowed (no permissions), read instead.
                using (Stream mem = webClient.OpenRead(urlPath))
                {
                    return Image.FromStream(mem);
                }
            }
        }

        public static ApiData Instance()
        {
            return instance ?? (instance = new ApiData());
        }

        public Champion[] GetChampions()
        {
            return data.champions;
        }

        public Item[] GetItems()
        {
            return data.items;
        }

        public Trait[] GetTraits()
        {
            return data.traits;
        }
    }
}
