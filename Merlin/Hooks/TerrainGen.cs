using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merlin.Hooks
{
    public static class TerrainGenHooks
    {
        [Hook("After", "", "TerrainGen", "GenerateWater")]
        public static void OnGenerateWater(TerrainGen terrain)
        {
            Merlin.Dispatch(mod => mod.OnGenerateWater(terrain));
        }

        [Hook("After", "", "TerrainGen", "GenerateFertileTiles")]
        public static void OnGenerateFertileTiles(TerrainGen terrain)
        {
            Merlin.Dispatch(mod => mod.OnGenerateFertileTiles(terrain));
        }
    }
}
