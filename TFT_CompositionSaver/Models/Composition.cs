using System.Collections.Generic;

namespace TFT_CompositionSaver.Models
{
    public class Composition
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<ChampionSet> championSets { get; set; } = new List<ChampionSet>();
    }
}
