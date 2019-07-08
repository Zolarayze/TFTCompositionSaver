using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using TFT_CompositionSaver.Helper;
using TFT_CompositionSaver.Models;

namespace TFT_CompositionSaver.DAL
{
    public class Data
    {
        public List<Composition> Compositions { get; set; }

        private static string dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "TFTCompositionSaver", "Data");
        private static string fileName = "Compositions.json";

        public Data()
        {
            Compositions = new List<Composition>();
        }

        public void SaveData()
        {
            try
            {
                ApiDataHelper.CreateFolder(dataPath);
                var json = JsonConvert.SerializeObject(Compositions, Formatting.Indented);
                string dir = Path.Combine(dataPath, fileName);
                
                File.WriteAllText(dir, json);

                Console.WriteLine("Converted to json");
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool LoadData()
        {
            try
            {
                string dir = Path.Combine(dataPath, fileName);

                if (!File.Exists(dir))
                {
                    return false;
                }
                
                using (StreamReader r = new StreamReader(dir))
                {
                    string json = r.ReadToEnd();
                    Compositions = JsonConvert.DeserializeObject<List<Composition>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }
    }
}
