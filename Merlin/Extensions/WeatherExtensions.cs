using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merlin.Extensions
{
    public static class WeatherExtensions
    {
        public static void EnableFog(this Weather weather)
        {
            weather.fogSummer = new MinMax(4f, 54f);
            weather.fogWinter = new MinMax(0f, 45f);
            weather.fogMapSelect = new MinMax(8f, 70f);
            weather.fogNormalRain = new MinMax(1f, 50f);
            weather.fogHeavyRain = new MinMax(0f, 45f);
        }

        public static void DisableFog(this Weather weather)
        {
            weather.fogSummer = new MinMax(100, 100);
            weather.fogWinter = new MinMax(100, 100);
            weather.fogMapSelect = new MinMax(100, 100);
            weather.fogHeavyRain = new MinMax(100, 100);
        }
    }
}
