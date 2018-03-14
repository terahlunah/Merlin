using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Merlin.MUI
{
    /** 
     <summary>   A Class that provides Objects to interact with the game "Kingdoms and Castles". </summary> 
    **/
    public static class GameAccess
    {
        private static GameState game = GameState.inst;

        /**
        <summary>   Gets the Unity Scene of the Game. </summary>
        
        <value> The scene. </value>
        **/

        public static Scene Scene
        {
            get { return game.mainMenuMode.topLevelUI.scene; }
        }

        /**
        <summary>   Gets the World. </summary>
        
        <value> The world. </value>
        **/

        public static World World
        {
            get { return game.world; }
        }

        /**
        <summary>   Gets the PlayingMode. </summary>
        
        <value> The playing mode. </value>
        **/

        public static PlayingMode PlayingMode
        {
            get { return game.playingMode; }
        }

        /**
        <summary>   Gets the MainMenuMode. </summary>
        
        <value> The main menu mode. </value>
        **/

        public static MainMenuMode MainMenuMode
        {
            get { return game.mainMenuMode; }
        }

        /**
        <summary>   Gets a list of all buildings in the game. (Not entirely sure if this is what is seems to be) </summary>
        
        <value> A list of buildings. </value>
        **/

        public static List<Building> PrefabList
        {
            get { return game.internalPrefabs; }
        }

        /**
        <summary>   Gets the player instance. </summary>
        
        <value> The player. </value>
        **/

        public static Player Player
        {
            get { return Player.inst; }
        }

        /**
        <summary>   Gets the build user interface instance. </summary>
        
        <value> The build user interface. </value>
        **/

        public static BuildUI BuildUI
        {
            get { return BuildUI.inst; }
        }

        /**
        <summary>   Gets the graphical user interface camera. </summary>
        
        <value> The graphical user interface camera. </value>
        **/

        public static Camera GUICamera
        {
            get { return UnityExtentions.GetCameraByName("3D GUI Camera"); }
        }

        /** 
         <summary>   A reflection subclass. </summary> 
        **/
        public static class Reflection
        {
            /**
            <summary>   Reflection Method. Gets a field by its name </summary>
            
            <typeparam name="T">    Generic type parameter. </typeparam>
            <param name="field">    The field. </param>
            <param name="instance"> (Optional) The instance. </param>
            
            <returns>   The field. </returns>
            **/

            public static object GetField<T>(string field, object instance = null)
            {
                return typeof(T).GetField(field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).GetValue(instance);
            }

            /**
            <summary>   Reflection method. Sets the value to a field. </summary>
            
            <typeparam name="T">    Generic type parameter. </typeparam>
            <param name="value">    The value. </param>
            <param name="field">    The field. </param>
            <param name="instance"> (Optional) The instance. </param>
            **/

            public static void SetField<T>(object value, string field, object instance = null)
            {
                typeof(T).GetField(field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).SetValue(instance, value);
            }
        }

        /** 
         <summary>   A subclass that contains specific example "pre defined" gameobjects form the game. </summary> 
        **/
        public static class Prefabs
        {
            /**
            <summary>   A pane. </summary>
            
            <value> The pane. </value>
            **/

            public static GameObject Pane1 { get { return UnityExtentions.GameObjectfromScene("MainMenuUI/MainMenu/SettingsMenu/Container/Background"); } }

            /**
            <summary>   A label. </summary>
            
            <value> The label. </value>
            **/

            public static TextMeshProUGUI Label1 { get { return GameAccess.MainMenuMode.saveLoadUI.LoadSaveTitle; } }

            /**
            <summary>   A button. </summary>
            
            <value> The button. </value>
            **/

            public static GameObject Button1 { get { return UnityExtentions.GameObjectfromScene("MainMenuUI/MainMenu/TopLevel/WindowContainer/Body/ButtonContainer/New"); } }

            /**
            <summary>   A button. </summary>
            
            <value> The button. </value>
            **/

            public static GameObject Button2 { get { return UnityExtentions.GameObjectfromScene("MainMenuUI/MainMenu/SettingsMenu/Container/Cancel"); } }

            /**
            <summary>   A checkbox. </summary>
            
            <value> The check box. </value>
            **/

            public static GameObject CheckBox1 { get { return UnityExtentions.GameObjectfromScene("MainMenuUI/MainMenu/SettingsMenu/Container/Preferences Group/LayoutContainer/InvertZoom"); } }

            /**
            <summary>   A slider. </summary>
            
            <value> The slider. </value>
            **/

            public static GameObject Slider1 { get { return UnityExtentions.GameObjectfromScene("MainMenuUI/MainMenu/SettingsMenu/Container/Preferences Group/MusicLabel/musicVolumeSlider"); } }

            /**
            <summary>   A comboBox. (WIP) </summary>
            
            <value> The combo box. </value>
            **/

            public static GameObject ComboBox1 { get { return UnityExtentions.GameObjectfromScene("MainMenuUI/MainMenu/TopLevel/WindowContainer/Body/Language/LanguageButton"); } }

            //Tabs
            //public static GameObject Tab1 { get { return UnityTools.GameObjectfromScene("MainMenuUI/MainMenu/SettingsMenu/Container/Tabs/Preferences"); } }
            
            //Scrollview    --- Not working currently
            //public static GameObject ScrollView1 { get { return UnityExtentions.GameObjectfromScene("MainMenuUI/MainMenu/TopLevel/Scroll View"); } }
            //public static GameObject ScrollView2 { get { return UnityExtentions.GameObjectfromScene("/MainMenu/LoadSaveContainer/LoadSaveUI/window/Scroll View"); } }
            public static GameObject ScrollView3 { get { return MainMenuMode.saveLoadUI.LoadSaveScrollRect.gameObject; } }
        }
    }
}
