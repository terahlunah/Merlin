using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Merlin.MUI
{
    /**
    <summary>   A general base definition for all containers that are used in MUI. </summary>
    
    <seealso cref="T:TestMod.MUI.MComponent"/>
    <seealso cref="T:System.Collections.Generic.IEnumerable{TestMod.MUI.MComponent}"/>
    **/

    public class MContainer : MComponent, IEnumerable<MComponent>
    {
        /**
        <summary>   Gets or sets the children. </summary>
        
        <value> The children. </value>
        **/

        public List<MComponent> Children { get; }

        /**
        <summary>   Gets the width in world units. </summary>
        
        <value> The width in world units. </value>
        
        <seealso cref="P:TestMod.MUI.MComponent.WorldWidth"/>
        **/

        public override float WorldWidth {
            get => base.WorldWidth;
            /*
            internal set {
                base.WorldWidth = value;
                if (Children != null)
                {
                    RearrangeChildren();
                }
            } */
         }

        /**
        <summary>   Gets the height in world units. </summary>
        
        <value> The height in world units. </value>
        
        <seealso cref="P:TestMod.MUI.MComponent.WorldHeight"/>
        **/

        public override float WorldHeight {
            get => base.WorldHeight;
            /*
            internal set {
                base.WorldHeight = value;
                if (Children != null)
                {
                    RearrangeChildren();
                }
            }
            */
        }

        /**
        <summary>   Gets or sets the width of the <see cref="RectTransform"/>. (in screen units) </summary>
        
        <value> The width of the <see cref="RectTransform"/>. (in screen units) </value>
        
        <seealso cref="P:TestMod.MUI.MComponent.RectWidth"/>
        **/

        public override float RectWidth
        {
            get => base.RectWidth;
            set
            {
                base.RectWidth = value;
                if (Children != null)
                {
                    RearrangeChildren();
                }
            }
        }

        /**
        <summary>   Gets or sets the height of the <see cref="RectTransform"/>. (in screen units) </summary>
        
        <value> The height of the <see cref="RectTransform"/>. (in screen units) </value>
        
        <seealso cref="P:TestMod.MUI.MComponent.RectHeight"/>
        **/

        public override float RectHeight
        {
            get => base.RectHeight;
            set
            {
                base.RectHeight = value;
                if (Children != null)
                {
                    RearrangeChildren();
                }
            }
        }

        /**
        <summary>   Gets or sets the spacing of all children. Spacing is the space between the children when placed inside the scene. 
            Only used when the Positioning is Absolute. </summary>
        
        <seealso cref="positioning"/>
        <seealso cref="Positioning"/>

        <value> The spacing. </value>
        **/

        public float Spacing { get; set; }

        private Alignment align;

        /**
        <summary>   Gets or sets the alignment. The Alignment defines where the container starts to place the first child. </summary>

        <seealso cref="Alignment"/>
        
        <value> The alignment. </value>
        **/

        public Alignment alignment
        {
            get => align;
            set
            {
                align = value;
                RearrangeChildren();
            }
        }

        private Positioning position;

        /**
        <summary>   Gets or sets the positioning. The Positioning defines how children are arranged inside the container. </summary>

        <seealso cref="Positioning"/>
        
        <value> The positioning. </value>
        **/

        public Positioning positioning
        {
            get => position;
            set
            {
                position = value;
                RearrangeChildren();
            }
        }

        /** <summary>   Default constructor. </summary> */
        public MContainer()
        {
            gameobject = new GameObject("MContainer", typeof(RectTransform));
            Children = new List<MComponent>();
            RectTransform = gameobject.GetComponent<RectTransform>();

            alignment = Alignment.TopCenter;
            positioning = Positioning.Absolute;

        }

        /**
        <summary>   Adds one ore multiple children. </summary>
        
        <param name="components">   A variable-length parameters list containing <see cref="MComponent"/>. </param>
        **/

        public virtual void AddChildren(params MComponent[] components)
        {
            AddChildrenRange(components);
        }

        /**
        <summary>   Adds a collection of children. </summary>
        
        <param name="components">   A variable-length parameters list containing <see cref="MComponent"/>. </param>
        **/

        public virtual void AddChildrenRange(IEnumerable<MComponent> components)
        {
            foreach (MComponent component in components)
            {
                if (!Children.Contains(component))
                {
                    Children.Add(component);
                    component.gameobject.transform.SetParent(gameobject.transform, false);
                    if (gameobject.activeSelf)
                    {
                        component.Show();
                    }
                    else
                    {
                        component.Hide();
                    }
                    component.ParentContainer = this;
                }
            }
            RearrangeChildren();
        }

        /** <summary>   Removes all children. </summary> */
        public virtual void RemoveAllChildren()
        {
            foreach(MComponent child in Children)
            {
                RemoveChildren(child);
            }
        }

        /**
        <summary>   Removes one or multiple children. </summary>
        
        <param name="components">   A variable-length parameters list containing <see cref="MComponent"/>. </param>
        **/
        public virtual void RemoveChildren(params MComponent[] components)
        {
            RemoveChildrenRange(components);
        }

        /**
        <summary>   Removes the children range described by components. </summary>
        
        <param name="components">   A variable-length parameters list containing
                                    <see cref="MComponent"/>. </param>
        **/

        public virtual void RemoveChildrenRange(IEnumerable<MComponent> components)
        {
            foreach(MComponent component in components)
            {
                if (Children.Contains(component))
                {
                    Children.Remove(component);
                    component.Hide();
                    //gameobject.GetChild(component.gameobject.name).transform.SetParent(null);
                    component.ParentContainer = null;
                }
            }
            RearrangeChildren();
        }

        /**
        <summary>   Makes the <see cref="MComponent.gameobject"/> in the scene visible. Also affects all children. </summary>
        
        <seealso cref="M:TestMod.MUI.MComponent.Show()"/>
        **/

        public override void Show()
        {
            RearrangeChildren();
            base.Show();
            foreach (MComponent component in Children)
            {
                component.Show();
            }
        }

        /**
        <summary>   Hides the <see cref="MComponent.gameobject"/> in the scene. Also affects all children. </summary>
        
        <seealso cref="M:TestMod.MUI.MComponent.Hide()"/>
        **/

        public override void Hide()
        {
            base.Hide();
            foreach (MComponent component in Children)
            {
                component.Hide();
            }
        }

        /** <summary>   Repositioning of <see cref="Children"/>. </summary> */
        internal virtual void RearrangeChildren()
        {
        }

        /**
        <summary>   Gets the <see cref="Vector3"/> starting position according the <see cref="alignment"/> value. </summary>

        <seealso cref="alignment"/>
        
        <returns>   The position according to the alignment value. </returns>
        **/

        protected Vector3 GetAlignmentPosition()
        {
            Vector3 pos;

            /*
             * Rectangle Corners:
             * 
             * c1               c2
             * __________________
             * |                |
             * |                |
             * |                |
             * |________________|
             * c0               c3
             */

            Vector3 c0 = RectTransform.GetWorldCornersDirect()[0];
            Vector3 c1 = RectTransform.GetWorldCornersDirect()[1];
            Vector3 c2 = RectTransform.GetWorldCornersDirect()[2];
            Vector3 c3 = RectTransform.GetWorldCornersDirect()[3];

            switch (alignment)
            {
                case Alignment.TopLeft:
                    pos = c1;
                    break;
                case Alignment.TopCenter:
                    pos = c1 + (c2 - c1) / 2;
                    break;
                case Alignment.TopRight:
                    pos = c2;
                    break;
                case Alignment.CenterLeft:
                    pos = c0 + (c1 - c0) / 2;
                    break;
                case Alignment.Center:
                    pos = RectTransform.position;
                    break;
                case Alignment.CenterRight:
                    pos = c3 + (c2 - c3) / 2;
                    break;
                case Alignment.BottomLeft:
                    pos = c0;
                    break;
                case Alignment.BottomCenter:
                    pos = c0 + (c3 - c0) / 2;
                    break;
                case Alignment.BottomRight:
                    pos = c3;
                    break;
                // should never happen
                default:
                    pos = RectTransform.position;
                    break;
            }

            return pos;
        } 

        /**
        <summary>   Returns an enumerator that iterates through the collection. </summary>
        
        <returns>
        A <see cref="T:System.Collections.Generic.IEnumerator`1" /> that can be used to iterate through
        the collection.
        </returns>
        
        <seealso cref="M:System.Collections.Generic.IEnumerable{TestMod.MUI.MComponent}.GetEnumerator()"/>
        **/

        public IEnumerator<MComponent> GetEnumerator()
        {
            return Children.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        /** <summary>   Values that represent different positions of an rectangle. Used to define the starting point for placing children. </summary> */
        public enum Alignment
        {
            /** <summary>   An enum constant representing the top left corner. </summary> */
            TopLeft,
            /** <summary>   An enum constant representing the center of the top edge. </summary> */
            TopCenter,
            /** <summary>   An enum constant representing the top right corner. </summary> */
            TopRight,
            /** <summary>   An enum constant representing the center of the left edge. </summary> */
            CenterLeft,
            /** <summary>   An enum constant representing the center of a rectangle. </summary> */
            Center,
            /** <summary>   An enum constant representing the center of the right edge </summary> */
            CenterRight,
            /** <summary>   An enum constant representing the bottom left corner. </summary> */
            BottomLeft,
            /** <summary>   An enum constant representing the center of the bottom edge. </summary> */
            BottomCenter,
            /** <summary>   An enum constant representing the bottom right corner. </summary> */
            BottomRight
        }

        /** <summary>   Values that represent how children are placed inside the container. </summary> */
        public enum Positioning
        {
            /** <summary>   Fills the children inside the container bounds. Calculates spacing so all children fit inside. </summary> */
            Fill,
            /**
             <summary>   Ignores the container bounds. It uses the spacing defined in the <see cref="Spacing"/> property </summary> 
             <seealso cref="Spacing"/>
            **/
            Absolute
        }

    }
}
