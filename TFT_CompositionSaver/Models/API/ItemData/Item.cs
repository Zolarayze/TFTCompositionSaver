using System.Drawing;

namespace TFT_CompositionSaver.Models.API.ItemData
{
    public class Item : IResourceModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public string desc { get; set; }
        public string icon { get; set; }
        public int[] from { get; set; }
        public Effect[] effects { get; set; }

        public Image image { get; set; }
    }
}
