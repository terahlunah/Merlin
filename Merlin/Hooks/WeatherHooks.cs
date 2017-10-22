using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Merlin.Hooks
{
    public static class WeatherHooks
    {
        [Hook("ForwardSelf", "", "Weather", "Start")]
        public static void OnWeatherStart(Weather weather)
        {
            Merlin.Weather = weather;
        }
    }
}
