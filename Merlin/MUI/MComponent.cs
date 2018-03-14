using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Merlin.MUI
{
    /**
    <summary>   An general definition for an object that is used in MUI. It is usually not used directly, 
        but rather as a base class to define a more customized objects for a specific purpose.
        </summary>
    
    <seealso cref="T:TestMod.MUI.IScaleable"/>
    **/

    public class MComponent
    {
        /**
        <summary>   Gets or sets the contained <see cref="GameObject"/>. </summary>
        
        <value> The gameobject. </value>
        **/

        public GameObject gameobject { get; internal set; }

        /**
        <summary>   Gets or sets the rectangle transform of the <see cref="gameobject"/>. </summary>
        
        <value> The rectangle transform. </value>
        **/

        public RectTransform RectTransform { get; internal set; }

        private MContainer parent;
        /**
        <summary>   Gets or sets the Parent <see cref="MContainer"/> of this <see cref="MComponent"/>. </summary>
        
        <value> The parent <see cref="MContainer"/>. </value>
        **/

        public MContainer ParentContainer {
            get { return parent; }
            set {
                if (parent != null)
                {
                    MContainer backup = parent;
                    parent = value;
                    if (parent != backup)
                    {
                        backup.RemoveChildren(this);
                        parent.AddChildren(this);
                    }
                }
                else
                {
                    parent = value;
                }
            } }

        private Offset offset;
        /**
        <summary>   Gets or sets the offset value. </summary>
        
        <value> The offset. </value>
        **/

        public Offset Offset {
            get { return offset; }
            set {
                offset = value;
                if(ParentContainer != null)
                {
                    ParentContainer.RearrangeChildren();
                }
            }
        }

        private ScaleFactor scale;

        /**
        <summary>   Gets or sets the scale object of the MComponent. </summary>
        
        <value> The scale factor. </value>
        **/

        public ScaleFactor ScaleFactor {
            get { return scale; }
            set {
                if (scale != null)
                {
                    ScaleFactor backup = scale;
                    scale = value;
                    float factorHorizontal = scale.Horizontal / backup.Horizontal;
                    float factorVertical = scale.Vertical / backup.Vertical;
                    RectWidth *= factorHorizontal;
                    RectHeight *= factorVertical;
                    ScaleFactor.ScaleableOject = this;
                }
                else
                {
                    scale = value;
                    ScaleFactor.ScaleableOject = this;
                }
            } }

        /**
        <summary>   Gets or sets the height of the <see cref="RectTransform"/>. (in screen units) </summary>
        
        <value> The height of the <see cref="RectTransform"/>. (in screen units) </value>
        **/

        public virtual float RectHeight {
            get { return RectTransform.rect.height; }
            set { RectTransform.SetHeight(value);
            } }

        /**
        <summary>   Gets or sets the width of the <see cref="RectTransform"/>. (in screen units) </summary>
        
        <value> The width of the <see cref="RectTransform"/>. (in screen units) </value>
        **/

        public virtual float RectWidth {
            get { return RectTransform.rect.width; }
            set { RectTransform.SetWidth(value);
            } }

        /**
        <summary>   Gets the height in world units. </summary>
        
        <value> The height in world units. </value>
        **/

        public virtual float WorldHeight {
            get { return RectTransform.GetWorldHeight(); }
            //internal set { //RectTransform.SetWorldWidth(value);}
        }

        /**
        <summary>   Gets the width in world units. </summary>
        
        <value> The width in world units. </value>
        **/

        public virtual float WorldWidth {
            get { return RectTransform.GetWorldWidth(); }
            //internal set { //RectTransform.SetWorldHeight(value);}
        }

        /**
        <summary>   Gets or sets the vertical scale. </summary>
        
        <value> The vertical scale. </value>
        **/

        internal float VerticalScale
        {
            get { return ScaleFactor.Vertical; }
            set {
                if (scale != null)
                {
                    float backup = scale.Vertical;
                    float newV = value;
                    float factorVertical = newV / backup;
                    RectHeight *= factorVertical;
                }
                else
                {
                    RectHeight *= value;
                }
        } }

        /**
        <summary>   Gets or sets the horizontal scale. </summary>
        
        <value> The horizontal scale. </value>
        **/

        internal float HorizontalScale
        {
            get => ScaleFactor.Horizontal;
            set {
                if (scale != null)
                {
                    float backup = scale.Horizontal;
                    float newH = value;
                    float factorHorizontal = newH / backup;
                    RectWidth *= factorHorizontal;
                }
                else
                {
                    RectWidth *= value;
                }
            } }

        internal bool DoReadjust
        {
            get; set;
        }

        /** <summary>   Default constructor. </summary> */
        public MComponent() {
            DoReadjust = true;
            Offset = new Offset(0);
            gameobject = new GameObject("MComponent", typeof(RectTransform));
            RectTransform = gameobject.GetComponent<RectTransform>();
            ScaleFactor = new ScaleFactor(this);
        }

        /** <summary>   Makes the <see cref="gameobject"/> in the scene visible. </summary> */
        public virtual void Show()
        {
            gameobject.SetActive(true);
        }

        /** <summary>   Hides the <see cref="gameobject"/> in the scene. </summary> */
        public virtual void Hide()
        {
            gameobject.SetActive(false);
        }

        internal virtual void ReadjustSize()
        {

        }

    }
}