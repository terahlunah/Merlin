using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Merlin.MUI
{
    /**
    <summary>   Container with horizontal arrangment of children. </summary>
    
    <seealso cref="T:TestMod.MUI.MContainer"/>
    **/

    public class MHbox : MContainer
    {

        /** <summary>   Default constructor. </summary> */
        public MHbox()
        {
            Spacing = 0f;
            gameobject.name = "MHbox";
        }

        /**
        <summary>   Repositioning of children. </summary>
        
        <seealso cref="M:TestMod.MUI.MContainer.RearrangeChildren()"/>
        **/

        internal override void RearrangeChildren()
        {
            switch (positioning)
            {
                case Positioning.Absolute:
                    FillAbsolute();
                    break;
                case Positioning.Fill:
                    FillParent();
                    break;
                default:
                    break;
            }
        }

        private void FillParent()
        {
            float spaceAdjustment = WorldWidth / (Children.Count);
            FillAbsolute(spaceAdjustment);

        }

        private void FillAbsolute(float customSpacing=0f)
        {
            foreach (MComponent child in Children)
            {
                child.ReadjustSize();
            }
            if (DoReadjust)
            {
                ReadjustSize();
            }

            Vector3 pos = GetAlignmentPosition();

            if (customSpacing == 0f)
            {
                float offsetX = 0f + Offset.Horizontal;

                for (int i = 0; i < Children.Count; i++)
                {
                    MComponent child = Children[i];
                    offsetX += child.Offset.Horizontal + child.WorldWidth/2;
                    child.gameobject.transform.position = new Vector3(pos.x + offsetX, pos.y + child.Offset.Vertical, pos.z);
                    offsetX += child.WorldWidth/2 + Spacing;
                }
            }
            else
            {
                float offsetX = 0f + Offset.Horizontal;

                for (int i = 0; i < Children.Count; i++)
                {
                    MComponent child = Children[i];
                    child.gameobject.transform.position = new Vector3(pos.x + offsetX, pos.y + child.Offset.Vertical, pos.z);
                    offsetX += customSpacing;
                }
            }
        }

        internal float GetCombinedWidth()
        {
            float width = 0f;
            foreach (MComponent child in Children)
            {
                width += child.RectWidth;
            }
            return width;
        }

        internal float GetMaxHeight()
        {
            float max = 0f;
            foreach (MComponent child in Children)
            {
                float childWidth = child.RectHeight;
                if (childWidth > max)
                {
                    max = childWidth;
                }
            }
            return max;
        }

        internal override void ReadjustSize()
        {
            if (Children.Count > 0)
            {
                RectTransform.SetWidth(GetCombinedWidth() * ScaleFactor.Vertical);
                RectTransform.SetHeight(GetMaxHeight() * ScaleFactor.Horizontal);
            }
        }
    }
}
