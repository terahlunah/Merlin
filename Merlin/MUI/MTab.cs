using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

namespace Merlin.MUI
{
    /**
    <summary>   Similar to a button with a toggle feature. </summary>
    
    <seealso cref="T:TestMod.MUI.MComponent"/>
    <seealso cref="T:TestMod.MUI.IToggleable"/>
    **/

    public class MTab : MComponent, IToggleable
    {
        /**
        <summary>   Gets or sets the text. </summary>
        
        <value> The text. </value>
        **/

        public string Text { get { return TextMesh.text; } set { TextMesh.text = value; } }

        /**
        <summary>   Gets the text mesh. </summary>
        
        <value> The text mesh. </value>
        **/

        public TextMeshProUGUI TextMesh { get; }

        /**
        <summary>   Gets the button. </summary>
        
        <value> The button. </value>
        **/

        public Button Button { get; }

        private bool isOn;

        /**
        <summary>   Gets a value indicating whether this object is enabled. </summary>
        
        <value> True if enabled, false if not. </value>
        
        <seealso cref="P:TestMod.MUI.IToggleable.Enabled"/>
        **/

        public bool Enabled { get { return isOn; } }

        /**
        <summary>   Default constructor. </summary>
        
        <param name="text">     (Optional) The text. </param>
        <param name="prefab">   (Optional) The prefab. </param>
        **/

        public MTab(string text = "", GameObject prefab = null)
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
            Button.onClick = new Button.ButtonClickedEvent();
            Button.onClick.AddListener(() => ToggleState());

            SetDisabled();
            gameobject.name = "MTab";
            TextMesh = gameobject.GetComponentInChildren<TextMeshProUGUI>();
            Text = text;
            RectTransform = gameobject.GetComponentInChildren<RectTransform>();
        }

        /** <summary>   Resets the event handler. </summary> */
        public void ResetEventHandler()
        {
            Button.onClick = new Button.ButtonClickedEvent();
            Button.onClick.AddListener(() => ToggleState());
        }

        /**
        <summary>   Sets the toggle to enabled. </summary>
        
        <seealso cref="M:TestMod.MUI.IToggleable.SetEnabled()"/>
        **/

        public void SetEnabled()
        {
            isOn = true;
            Button.image.SetAlpha(1f);
        }

        /**
        <summary>   Sets the toggle to disabled. </summary>
        
        <seealso cref="M:TestMod.MUI.IToggleable.SetDisabled()"/>
        **/

        public void SetDisabled()
        {
            isOn = false;
            Button.image.SetAlpha(0.8f);
        }

        /**
        <summary>   Toggles the state of the toggle. </summary>
        
        <seealso cref="M:TestMod.MUI.IToggleable.Toggle()"/>
        **/

        public void ToggleState()
        {
            if (Enabled) SetDisabled(); else SetEnabled();
        }

        /**
        <summary>   Gets trigger event. </summary>
        
        <returns>   The trigger event. </returns>
        
        <seealso cref="M:TestMod.MUI.IToggleable.GetTriggerEvent()"/>
        **/

        public UnityEvent GetTriggerEvent()
        {
            return Button.onClick;
        }
    }
}
