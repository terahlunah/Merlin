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
    <summary>   A check box that is toggleable. </summary>
    
    <seealso cref="T:TestMod.MUI.MComponent"/>
    <seealso cref="T:TestMod.MUI.IToggleable"/>
    **/

    public class MCheckBox : MComponent, IToggleable
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
        <summary>   Gets the toggle object. </summary>
        
        <value> The toggle object. </value>
        **/

        public Toggle Toggle { get; }

        /**
        <summary>   Gets a value indicating whether this object is enabled. </summary>
        
        <value> True if enabled, false if not. </value>
        
        <seealso cref="P:TestMod.MUI.IToggleable.Enabled"/>
        **/

        public bool Enabled { get { return Toggle.isOn; } }

        private UnityEvent toggled;

        /**
        <summary>   Default constructor. </summary>
        
        <param name="text">     (Optional) The text. </param>
        <param name="prefab">   (Optional) The prefab. </param>
        **/

        public MCheckBox(string text = "", GameObject prefab = null)
        {
            toggled = new UnityEvent();
            if (prefab == null)
            {
                gameobject = GameObject.Instantiate(GameAccess.Prefabs.CheckBox1);
            }
            else
            {
                gameobject = GameObject.Instantiate(prefab);
            }
            gameobject.name = "MCheckbox";
            TextMesh = gameobject.GetComponentInChildren<TextMeshProUGUI>();
            Text = text;
            Toggle = gameobject.GetComponentInChildren<Toggle>();
            RectTransform = gameobject.GetComponentInChildren<RectTransform>();
            Toggle.onValueChanged.AddListener(delegate { toggled.Invoke(); });
        }

        /**
        <summary>   Sets the toggle to enabled. </summary>
        
        <seealso cref="M:TestMod.MUI.IToggleable.SetEnabled()"/>
        **/

        public void SetEnabled()
        {
            Toggle.isOn = true;
        }

        /**
        <summary>   Sets the toggle to disabled. </summary>
        
        <seealso cref="M:TestMod.MUI.IToggleable.SetDisabled()"/>
        **/

        public void SetDisabled()
        {
            Toggle.isOn = false;
        }

        /**
        <summary>   Toggles the state of the toggle. </summary>
        
        <seealso cref="M:TestMod.MUI.IToggleable.ToggleState()"/>
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
            return toggled;
        }
    }
}
