
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Merlin.MUI
{
    /**
      <summary>    Contains methods and extension methods to help with frequently required tasks for unity objects. </summary> 
    **/
    public static class UnityExtentions
    {

        /**
          <summary>    An Image extension method that sets the alpha value of the Color. (You can not do this directly by accessing the Color object) </summary>
         
          <param name="image"> The image to act on. </param>
          <param name="alpha"> The alpha. </param>
        **/

        public static void SetAlpha(this Image image, float alpha)
        {
            Color c = image.color;
            c.a = alpha;
            image.color = c;
        }

        /**
          <summary>    Gets a camera from the unity scene by name. </summary>
         
          <param name="name">  The name. </param>
         
          <returns>    The camera by name. </returns>
        **/

        public static Camera GetCameraByName(string name)
        {
            Camera camera = null;

            foreach (Camera c in Camera.allCameras)
            {
                if (c.name.Equals(name))
                {
                    camera = c;
                }
            }

            return camera;
        }

        /**
        <summary>   A RectTransform extension method that anchor to the corners of the rectangle. </summary>
        
        <exception cref="ArgumentNullException">    Thrown when transform is null. </exception>
        
        <param name="transform">    The Recttransform to act on. </param>
        **/

        public static void AnchorToCorners(this RectTransform transform)
        {
            if (transform == null)
                throw new ArgumentNullException("transform");

            if (transform.parent == null)
                return;

            var parent = transform.parent.GetComponent<RectTransform>();

            Vector2 newAnchorsMin = new Vector2(transform.anchorMin.x + transform.offsetMin.x / parent.rect.width,
                              transform.anchorMin.y + transform.offsetMin.y / parent.rect.height);

            Vector2 newAnchorsMax = new Vector2(transform.anchorMax.x + transform.offsetMax.x / parent.rect.width,
                              transform.anchorMax.y + transform.offsetMax.y / parent.rect.height);

            transform.anchorMin = newAnchorsMin;
            transform.anchorMax = newAnchorsMax;
            transform.offsetMin = transform.offsetMax = new Vector2(0, 0);
        }

        /**
        <summary>   A RectTransform extension method that sets pivot and anchors to the given position. </summary>
        
        <param name="trans">    The Recttransform to act on. </param>
        <param name="aVec">     The vector. </param>
        **/

        public static void SetPivotAndAnchors(this RectTransform trans, Vector2 aVec)
        {
            trans.pivot = aVec;
            trans.anchorMin = aVec;
            trans.anchorMax = aVec;
        }

        /**
        <summary>   A RectTransform extension method that sets the size of the Recttransform. </summary>
        
        <param name="trans">    The Recttransform to act on. </param>
        <param name="newSize">  Size of the new. </param>
        **/

        public static void SetSize(this RectTransform trans, Vector2 newSize)
        {
            Vector2 oldSize = trans.rect.size;
            Vector2 deltaSize = newSize - oldSize;
            trans.offsetMin = trans.offsetMin - new Vector2(deltaSize.x * trans.pivot.x, deltaSize.y * trans.pivot.y);
            trans.offsetMax = trans.offsetMax + new Vector2(deltaSize.x * (1f - trans.pivot.x), deltaSize.y * (1f - trans.pivot.y));
        }

        /**
          <summary>    A RectTransform extension method that sets the width of the RectTransform. </summary>
         
          <param name="rectTransform"> The rectTransform to act on. </param>
          <param name="width">         The width. </param>
        **/

        public static void SetWidth(this RectTransform rectTransform, float width)
        {
            SetSize(rectTransform, new Vector2(width, rectTransform.rect.size.y));

        }

        /**
          <summary>    A RectTransform extension method that sets the height of the RectTransform. </summary>
         
          <param name="rectTransform"> The rectTransform to act on. </param>
          <param name="height">         The width. </param>
        **/

        public static void SetHeight(this RectTransform rectTransform, float height)
        {
            SetSize(rectTransform, new Vector2(rectTransform.rect.size.x, height));
        }

        /**
        <summary>   A RectTransform extension method that sets the bottom left position. </summary>
        
        <param name="trans">    The Recttransform to act on. </param>
        <param name="newPos">   The new position. </param>
        **/

        public static void SetBottomLeftPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
        }

        /**
        <summary>   A RectTransform extension method that sets the top left position. </summary>
        
        <param name="trans">    The Recttransform to act on. </param>
        <param name="newPos">   The new position. </param>
        **/

        public static void SetTopLeftPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x + (trans.pivot.x * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
        }

        /**
        <summary>   A RectTransform extension method that sets the bottom right position. </summary>
        
        <param name="trans">    The Recttransform to act on. </param>
        <param name="newPos">   The new position. </param>
        **/

        public static void SetBottomRightPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y + (trans.pivot.y * trans.rect.height), trans.localPosition.z);
        }

        /**
        <summary>   A RectTransform extension method that sets the right top position. </summary>
        
        <param name="trans">    The Recttransform to act on. </param>
        <param name="newPos">   The new position. </param>
        **/

        public static void SetRightTopPosition(this RectTransform trans, Vector2 newPos)
        {
            trans.localPosition = new Vector3(newPos.x - ((1f - trans.pivot.x) * trans.rect.width), newPos.y - ((1f - trans.pivot.y) * trans.rect.height), trans.localPosition.z);
        }

        /**
        <summary>   A RectTransform extension method that sets the width in world units. </summary>
        
        <param name="rectTransform">    The rectTransform to act on. </param>
        <param name="width">            The width. </param>
        **/

        public static void SetWorldWidth(this RectTransform rectTransform, float width)
        {
            float newRectWidth = width / rectTransform.GetWorldWidth() * rectTransform.rect.width;
            rectTransform.SetWidth(newRectWidth);
        }

        /**
        <summary>   A RectTransform extension method that sets the width in world units. </summary>
        
        <param name="rectTransform">    The rectTransform to act on. </param>
        <param name="height">           The width. </param>
        **/

        public static void SetWorldHeight(this RectTransform rectTransform, float height)
        {
            float newRectHeight = height / rectTransform.GetWorldHeight() * rectTransform.rect.height;
            rectTransform.SetHeight(newRectHeight);
        }

        /**
          <summary>   A RectTransform extension method that gets the world width. </summary>
         
          <param name="rectTransform"> The rectTransform to act on. </param>
         
          <returns>    The world width. </returns>
        **/

        public static float GetWorldWidth(this RectTransform rectTransform)
        {
            return rectTransform.GetWorldCornersDirect()[2].x - rectTransform.GetWorldCornersDirect()[1].x;
        }

        /**
          <summary>   A RectTransform extension method that gets the world height. </summary>
         
          <param name="rectTransform"> The rectTransform to act on. </param>
         
          <returns>    The world height. </returns>
        **/

        public static float GetWorldHeight(this RectTransform rectTransform)
        {
            return rectTransform.GetWorldCornersDirect()[1].y - rectTransform.GetWorldCornersDirect()[0].y;
        }

        /**
          <summary>     A RectTransform extension method returns the Vector3 array directly without the requirement to pass an array. </summary>
         
          <param name="rectTransform"> The rectTransform to act on. </param>
         
          <returns>    An array of vector 3. </returns>
        **/

        public static Vector3[] GetWorldCornersDirect(this RectTransform rectTransform)
        {
            Vector3[] vector = new Vector3[4];
            rectTransform.GetWorldCorners(vector);
            return vector;
        }

        /**
          <summary>
          A GameObject extension method that destroys all children of a GameObject.
          </summary>
         
          <param name="gameObject">    The gameObject to act on. </param>
        **/

        public static void DestroyAllChildren(this GameObject gameObject)
        {
            foreach (Transform transform in gameObject.transform)
            {
                UnityEngine.Object.Destroy(transform.gameObject);
            }
        }

        /**
          <summary>    A GameObject extension method that gets a child of the <see cref="GameObject"/> by its name. </summary>
         
          <param name="parent">    The parent to act on. </param>
          <param name="name">      The name. </param>
         
          <returns>    The child. </returns>
        **/

        public static GameObject GetChild(this GameObject parent, string name)
        {
            foreach (Transform transform in parent.transform)
            {
                if (transform.gameObject.name.Equals(name))
                {
                    return transform.gameObject;
                }
            }

            return null;
        }

        /**
          <summary>
          Gets a <see cref="GameObject"/> by its path inside the <see cref="UnityEngine.SceneManagement.Scene"/>. Each GameObject has to be seperated by a "/".
          For example GameObjectfromScene("MainMenuUI/MainMenu/TopLevel/WindowContainer/Body/ButtonContainer")
          returns the <see cref="GameObject"/> with the name "ButtonContainer", "MainMenuUI" would be one of the
          Scene root <see cref="GameObject"/>.
          </summary>
         
          <param name="pathInScene">   The path in scene. </param>
         
          <returns>    A GameObject. </returns>
        **/

        public static GameObject GameObjectfromScene(string pathInScene)
        {
            //GameObject[] root = GameState.inst.mainMenuMode.topLevelUI.scene.GetRootGameObjects();
            GameObject[] root = Camera.main.gameObject.scene.GetRootGameObjects();

            foreach (GameObject rootgameobject in root)
            {
                if (!pathInScene.Contains('/') && rootgameobject.name.Equals(pathInScene))
                {
                    return rootgameobject;
                }
                else
                {
                    if (rootgameobject.name.Equals(pathInScene.Substring(0, pathInScene.IndexOf('/'))))
                    {
                        GameObject gameobject = rootgameobject;
                        string currpath = pathInScene.Substring(pathInScene.IndexOf('/') + 1);
                        for (int i = 1; i < pathInScene.Split('/').Length; i++)
                        {
                            foreach (Transform t in gameobject.transform)
                            {
                                if (t.gameObject.name.Equals(currpath.Split('/')[0]))
                                {
                                    gameobject = t.gameObject;
                                }
                            }
                            currpath = currpath.Substring(currpath.IndexOf('/') + 1);
                        }
                        return gameobject;
                    }
                }

            }

            return null;
        }

    }
}
