using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merlin.Hooks
{
    public static class WorldHooks
    {
        [Hook("ForwardSelf", "", "World", "Generate")]
        public static void OnWorldGenerate(World world)
        {
            //Merlin.Dispatch(mod => mod.OnWorldGenerate(world));
        }
    }
}
