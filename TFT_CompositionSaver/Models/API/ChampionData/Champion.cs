using System.Drawing;

namespace TFT_CompositionSaver.Models.API.ChampionData
{
    public class Champion : IResourceModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public int cost { get; set; }
        public string splash { get; set; }
        public Stats stats { get; set; }
        public string[] traits { get; set; }
        public Ability ability { get; set; }

        public Image image { get; set; }
    }
}
