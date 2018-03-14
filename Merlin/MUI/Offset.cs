using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merlin.MUI
{
    /** 
    <summary>  Object for storing the Offset values for an Unity GameObject. Useful for fine adjustments. </summary> 
     **/
    public class Offset
    {
        /**
        <summary>   Sets the vertical value. </summary>
        
        <value> The vertical value. </value>
        **/

        public float Vertical { get; set; }

        /**
        <summary>   Sets the horizontal value. </summary>
        
        <value> The horizontal value. </value>
        **/

        public float Horizontal { get; set; }

        /**
        <summary>   Shortcut for setting values for all directions. </summary>
        
        <value> value to set. </value>
        **/

        public float All { set { Vertical = value; Horizontal = value; } }

        /**
        <summary>   Default Constructor. </summary>
        
        <param name="all">  (Optional) All. Default is zero. </param>
        **/

        public Offset(float all=0)
        {
            Vertical = all;
            Horizontal = all;
        }

        /**
        <summary>   Alternate Constructor. </summary>
        
        <param name="horizontal">   The horizontal value. </param>
        <param name="vertical">     The vertical value. </param>
        **/

        public Offset(float horizontal, float vertical)
        {
            Vertical = vertical;
            Horizontal = Horizontal;
        }

        /**
        <summary>   Returns a string that represents the current object. </summary>
        
        <returns>   A string that represents the current object. </returns>
        
        <seealso cref="M:System.Object.ToString()"/>
        **/

        public override string ToString()
        {
            return "Offset - Horizontal: " + Horizontal + " Vertical: " + Vertical;
        }
    }
}
