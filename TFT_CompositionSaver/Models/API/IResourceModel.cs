using System.Drawing;

namespace TFT_CompositionSaver.Models.API
{
    public interface IResourceModel
    {
        string name { get; set; }
        Image image { get; set; }
    }
}
