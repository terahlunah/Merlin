﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace Merlin
{
    static class Merlin
    {
        public static List<MerlinMod> Mods = new List<MerlinMod>();

        public static void LoadMods()
        {
            string modsPath = GetModsPath();

            foreach (var file in Directory.GetFiles(modsPath))
            {
                if (file.EndsWith(".dll"))
                {
                    try
                    {
                        LoadMod(file);
                    }
                    catch (Exception e)
                    {
                        Debug.Log(e);
                    }
                }
                    
            }
        }

        private static string GetModsPath()
        {
            var executingDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var rootDir = Directory.GetParent(executingDir).Parent;

            var modDir = Path.Combine(rootDir.FullName, "Mods");

            if (!Directory.Exists(modDir))
                Directory.CreateDirectory(modDir);

            return modDir;
        }

        private static void LoadMod(string path)
        {
            var assembly = Assembly.LoadFrom(path);
            foreach (var type in FindModTypes(assembly))
            {
                var mod = Activator.CreateInstance(type) as MerlinMod;
                mod.OnLoad();
                Mods.Add(mod);
            }
        }

        private static IEnumerable<Type> FindModTypes(Assembly assembly)
        {
            return assembly.GetTypes().Where(t => typeof(MerlinMod).IsAssignableFrom(t));
        }

        public static void Dispatch(Action<MerlinMod> action)
        {
            Mods.ForEach(action);
        }

        public static object GetField<T>(string field, object instance = null)
        {
            return typeof(T).GetField(field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).GetValue(instance);
        }

        public static void SetField<T>(object value, string field, object instance = null)
        {
            typeof(T).GetField(field, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static).SetValue(instance, value);
        }
    }
}
