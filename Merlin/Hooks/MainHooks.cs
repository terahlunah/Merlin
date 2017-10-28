using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merlin.Hooks
{
    public static class MainHooks
    {
        [Hook("Before", "", "GameState", "Start")]
        public static void GameStateStart(GameState self)
        {
            Merlin.LoadMods();
        }

        [Hook("Before", "", "GameState", "Update")]
        public static void GameStateUpdate(GameState self)
        {
            
        }
    }
}
