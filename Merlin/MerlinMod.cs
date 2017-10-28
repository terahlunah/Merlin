using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Merlin
{
    public class MerlinMod
    {
        public Weather Weather => Merlin.GetField<Weather>("inst") as Weather;
        public World World => Merlin.GetField<World>("inst") as World;

        public virtual void OnLoad() { }

        public virtual void OnGenerateWater(TerrainGen terrain) { }
        public virtual void OnGenerateFertileTiles(TerrainGen terrain) { }

        public virtual void OnGenerateTrees(TreeGrowth treeGrowth) { }
    }
}
