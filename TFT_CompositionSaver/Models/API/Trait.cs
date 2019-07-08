using System.Drawing;

namespace TFT_CompositionSaver.Models.API
{
    public class Trait : IResourceModel
    {
        public string name { get; set; }
        public string desc { get; set; }
        public string icon { get; set; }

        public Image image { get; set; }
    }
}
