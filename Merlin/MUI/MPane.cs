using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Merlin.MUI
{
    /**
    <summary>   Pane (background) for components. It acts as a starting point for every UI. 
        It always has a top-level container </summary>
    
    <seealso cref="T:TestMod.MUI.MComponent"/>
    **/

    public class MPane : MComponent
    {
        /**
        <summary>   Gets the top-level-container. </summary>
        
        <value> The top-level-container. </value>
        **/

        public MContainer TopContainer { get; }

        /**
        <summary>   Gets the image. (background) </summary>
        
        <value> The image. </value>
        **/

        public Image Image { get;}

        /**
        <summary>   Default constructor. </summary>
        
        <param name="topContainer"> The top-level-container. </param>
        <param name="prefab">       (Optional) The prefab. </param>
        **/

        public MPane(MContainer topContainer, GameObject prefab = null)
        {
            if (prefab == null)
            {
                gameobject = GameObject.Instantiate(GameAccess.Prefabs.Pane1, GameAccess.MainMenuMode.topLevelUI.transform, true);
            }
            else
            {
                gameobject = GameObject.Instantiate(prefab, GameAccess.MainMenuMode.topLevelUI.transform, true);
            }
            RectTransform.SetParent(null, true);
            gameobject.SetActive(false);
            TopContainer = topContainer;
            Image = gameobject.GetComponentInChildren<Image>();
            RectTransform = Image.rectTransform;

            TopContainer.gameobject.transform.SetParent(RectTransform, false);
            gameobject.name = "MPane";
            TopContainer.RectTransform = RectTransform;

            TopContainer.RectWidth = RectWidth;
            TopContainer.RectHeight = RectHeight;

            this.DoReadjust = false;
            TopContainer.DoReadjust = false;
        }

        /**
        <summary>   Makes the gameobject in the scene visible. Also affects the top-level-container. </summary>
        
        <seealso cref="M:TestMod.MUI.MComponent.Show()"/>
        **/

        public override void Show()
        {
            base.Show();
            TopContainer.Show();
        }
    }
}
