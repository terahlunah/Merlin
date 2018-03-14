using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Merlin.MUI
{
    /**
    <summary>   A slider. </summary>
    
    <seealso cref="T:TestMod.MUI.MComponent"/>
    **/

    public class MSlider : MComponent
    {
        /**
        <summary>   Gets the slider component. </summary>
        
        <value> The slider. </value>
        **/

        public Slider Slider { get; }

        /**
        <summary>   Default constructor. </summary>
        
        <param name="prefab">   (Optional) The prefab. </param>
        **/

        public MSlider(GameObject prefab = null)
        {
            if (prefab == null)
            {
                gameobject = GameObject.Instantiate(GameAccess.Prefabs.Slider1);
            }
            else
            {
                gameobject = GameObject.Instantiate(prefab);
            }
            gameobject.name = "MSlider";
            Slider = gameobject.GetComponentInChildren<Slider>();
            RectTransform = gameobject.GetComponentInChildren<RectTransform>();
        }
    }
}
