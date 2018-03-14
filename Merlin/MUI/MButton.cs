using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Merlin.MUI
{
    /**
    <summary>   A button. </summary>
    
    <seealso cref="T:TestMod.MUI.MComponent"/>
    **/

    public class MButton : MComponent
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

        public TextMeshProUGUI TextMesh { get; }

        /**
        <summary>   Gets the button object. </summary>
        
        <value> The button object. </value>
        **/

        public Button Button { get; }

        /**
        <summary>   Gets the image object. (background) </summary>
        
        <value> The image object. </value>
        **/

        public Image Image { get; }

        /**
        <summary>   Default constructor. </summary>
        
        <param name="text">     (Optional) The text. </param>
        <param name="prefab">   (Optional) The prefab. </param>
        **/

        public MButton(string text = "", GameObject prefab = null)
        {
            if (prefab == null)
            {
                gameobject = GameObject.Instantiate(GameAccess.Prefabs.Button2);
            }
            else
            {
                gameobject = GameObject.Instantiate(prefab);
            }
            Button = gameobject.GetComponentInChildren<Button>();
            Button.onClick = new Button.ButtonClickedEvent(); //Clean all previous events
            Image = Button.image;
            TextMesh = Button.gameObject.GetComponentInChildren<TextMeshProUGUI>();
            Text = text;
            gameobject.name = "MButton";
            RectTransform = Image.rectTransform;
        }
    }
}
