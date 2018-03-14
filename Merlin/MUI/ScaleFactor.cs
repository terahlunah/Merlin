using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Merlin.MUI
{
    /**
     <summary>   An Object that is used to easily do scaling operations for MComponents. </summary>
        
     <seealso cref="MComponent"/>
    **/
    public class ScaleFactor
    {
        private float vertical;

        private float horizontal;

        /**
        <summary>   Gets or sets the vertical scale. </summary>
        
        <value> The vertical. </value>
        **/

        public float Vertical { get { return vertical; } set { ScaleableOject.VerticalScale = value; vertical = value;  } }

        /**
        <summary>   Gets or sets the horizontal scale. </summary>
        
        <value> The horizontal. </value>
        **/

        public float Horizontal { get { return horizontal; } set { ScaleableOject.HorizontalScale = value; horizontal = value; } }

        /**
        <summary>   Shortcut for changing both the horizontal and the vertical scale. </summary>
        
        <seealso cref="Horizontal"/>
        <seealso cref="Vertical"/>
        
        <value> scale to set. </value>
        **/

        public float All { set { Vertical = value; Horizontal = value; } }

        /**
        <summary>   Gets or sets the Object that is scaled. </summary>
        
        <seealso cref="MComponent"/>

        <value> The scaleable oject. </value>
        **/

        public MComponent ScaleableOject { get; set; }

        /**
        <summary>   Constructor of ScaleFactor. </summary>
        
        <param name="scaleableObject">  The object to scale. </param>
        <param name="horizontal">       (Optional) The horizontal scale. </param>
        <param name="vertical">         (Optional) The vertical scale. </param>

        <seealso cref="MComponent"/>
        **/

        public ScaleFactor(MComponent scaleableObject, float horizontal = 1f, float vertical = 1f)
        {
            ScaleableOject = scaleableObject;
            Horizontal = horizontal;
            Vertical = vertical;
        }

        /**
        <summary>   Returns a string that represents the current object. </summary>
        
        <returns>   A string that represents the current object. </returns>
        
        <seealso cref="M:System.Object.ToString()"/>
        **/

        public override string ToString()
        {
            return "Scale Factor - Vertical: " + Vertical + " Horizontal: " + Horizontal;
        }

        /**
        <summary>   Addition operator. </summary>
        
        <param name="scale">    The scale object. </param>
        <param name="modifer">  The modifer. (applied to horizontal and vertical) </param>
        
        <returns>   The result of the operation. </returns>
        **/

        public static ScaleFactor operator+(ScaleFactor scale, float modifer)
        {
            scale.Horizontal += modifer;
            scale.Vertical += modifer;
            return scale;
        }

        /**
        <summary>   Subtraction operator. </summary>
        
        <param name="scale">    The scale object. </param>
        <param name="modifer">  The modifer. (applied to horizontal and vertical) </param>
        
        <returns>   The result of the operation.  </returns>
        **/

        public static ScaleFactor operator -(ScaleFactor scale, float modifer)
        {
            scale.Horizontal -= modifer;
            scale.Vertical -= modifer;
            return scale;
        }

        /**
        <summary>   Multiplication operator. </summary>
        
        <param name="scale">    The scale object. </param>
        <param name="modifer">  The modifer. (applied to horizontal and vertical) </param>
        
        <returns>   The result of the operation. </returns>
        **/

        public static ScaleFactor operator *(ScaleFactor scale, float modifer)
        {
            scale.Horizontal *= modifer;
            scale.Vertical *= modifer;
            return scale;
        }

        /**
        <summary>   Division operator. </summary>
        
        <param name="scale">    The scale object. </param>
        <param name="modifer">  The modifer. (applied to horizontal and vertical) </param>
        
        <returns>   The result of the operation. </returns>
        **/

        public static ScaleFactor operator /(ScaleFactor scale, float modifer)
        {
            scale.Horizontal /= modifer;
            scale.Vertical /= modifer;
            return scale;
        }
    }
}
