using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Merlin.MUI
{
    /**
    <summary>   Container with vertical arrangment of children. </summary>
    
    <seealso cref="T:TestMod.MUI.MContainer"/>
    **/

    public class MVbox : MContainer
    {

        /** <summary>   Default constructor. </summary> */
        public MVbox()
        {
            Spacing = 0f;
            gameobject.name = "MVbox";
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

        private void FillAbsolute(float customspacing=0f)
        {
            foreach(MComponent child in Children)
            {
                child.ReadjustSize();
            }
            if (DoReadjust)
            {
                ReadjustSize();
            }

            Vector3 pos = GetAlignmentPosition();

            if (customspacing == 0f)
            {
                float offsetY = 0f + Offset.Vertical;

                for (int i = 0; i < Children.Count; i++)
                {
                    MComponent child = Children[i];
                    offsetY += child.Offset.Vertical - child.WorldHeight/2;
                    child.gameobject.transform.position = new Vector3(pos.x + child.Offset.Horizontal, pos.y + offsetY, pos.z);
                    offsetY = offsetY - child.WorldHeight/2 - Spacing;
                }
            }
            else
            {
                float offsetY = 0f + Offset.Vertical;

                for (int i = 0; i < Children.Count; i++)
                {
                    MComponent child = Children[i];
                    child.gameobject.transform.position = new Vector3(pos.x + child.Offset.Horizontal, pos.y + offsetY, pos.z);
                    offsetY -= customspacing;
                }
            }
        }

        private void FillParent()
        {
            float spaceAdjustment = WorldHeight / (Children.Count);
            FillAbsolute(spaceAdjustment);
        }

        internal float GetCombinedHeight()
        {
            float height = 0f;
            foreach (MComponent child in Children)
            {
                height += child.RectHeight;
            }
            return height;
        }

        internal float GetMaxWidth()
        {
            float max = 0f;
            foreach (MComponent child in Children)
            {
                float childWidth = child.RectWidth;
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
                RectTransform.SetHeight(GetCombinedHeight() * ScaleFactor.Vertical);
                RectTransform.SetWidth(GetMaxWidth() * ScaleFactor.Horizontal);
            }
        }
    }
}
