using System.Collections.Generic;

namespace TFT_CompositionSaver.Models
{
    public class ChampionSet
    {
        public int championId { get; set; }
        public List<int> itemIds { get; set; } = new List<int>();
        public bool swapChecked { get; set; }
        public int stars { get; set; }
    }
}
