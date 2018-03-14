using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.Events;

namespace Merlin.MUI
{
    /**
    <summary>   Interface for toggleable objects. (e.g. switches or checkboxes) </summary> 
    **/
    public interface IToggleable
    {
        /**
        <summary>   Gets a value indicating whether this object is enabled. </summary>
        
        <value> True if enabled, false if not. </value>
        **/

        bool Enabled { get;}

        /**
        <summary>   Sets the toggle to enabled. </summary> 
        **/
        void SetEnabled();

        /**
         <summary>   Sets the toggle to disabled. </summary>
        **/
        void SetDisabled();

        /**
        <summary>   Toggles the state of the toggle. </summary> 
        **/
        void ToggleState();

        /**
        <summary>   Gets trigger event. </summary>
        
        <returns>   The trigger event. </returns>
        **/

        UnityEvent GetTriggerEvent();
    }
}
