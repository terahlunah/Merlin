using Merlin;
using UnityEngine;

namespace NoFog
{
    public class NoFogMod : MerlinMod
    {
        public void Start()
        {
            Weather.fogSummer = new MinMax(100, 100);
            Weather.fogWinter = new MinMax(100, 100);
            Weather.fogMapSelect = new MinMax(100, 100);
            Weather.fogHeavyRain = new MinMax(100, 100);
        }
    }
}
