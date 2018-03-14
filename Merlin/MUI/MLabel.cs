using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using TMPro;

namespace Merlin.MUI
{
    /**
    <summary>   A label. </summary>
    
    <seealso cref="T:TestMod.MUI.MComponent"/>
    **/

    public class MLabel : MComponent
    {
        /**
        <summary>   Gets or sets the text. </summary>
        
        <value> The text. </value>
        **/

        public string Text
        {
            get { return TextMesh.text; }
            set { TextMesh.text = value; }
        }

        /**
        <summary>   Gets the text mesh. </summary>
        
        <value> The text mesh. </value>
        **/

        public TextMeshProUGUI TextMesh{ get; }

        /**
        <summary>   Default constructor. </summary>
        
        <param name="text">     (Optional) The text. </param>
        <param name="prefab">   (Optional) The prefab. </param>
        **/

        public MLabel( string text = "", TextMeshProUGUI prefab = null)
        {
            if (prefab == null)
            {
                TextMesh = TextMeshProUGUI.Instantiate(GameAccess.Prefabs.Label1);
            }
            else
            {
                TextMesh = TextMeshProUGUI.Instantiate(prefab);
            }
            gameobject = TextMesh.gameObject;
            Text = text;
            gameobject.name = "MLabel";
            RectTransform = gameobject.GetComponentInChildren<RectTransform>();
        }
    }
}
