using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merlin.Hooks
{
    public static class TreeGrowthHooks
    {
        [Hook("Before", "", "TreeGrowth", "GenerateTrees")]
        public static void OnGenerateTrees(TreeGrowth treeGrowth)
        {
            Merlin.Dispatch(mod => mod.OnGenerateTrees(treeGrowth));
        }
    }
}
