using TFT_CompositionSaver.Models.API.ChampionData;
using TFT_CompositionSaver.Models.API.ItemData;

namespace TFT_CompositionSaver.Models.API
{
    public class GameData
    {
        public Champion[] champions { get; set; }
        public Item[] items { get; set; }
        public Trait[] traits { get; set; }
    }
}
