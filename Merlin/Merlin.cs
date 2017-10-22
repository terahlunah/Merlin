using System;
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


        public static Weather Weather { get; set; }


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
                var gob = new GameObject().AddComponent(type);
                var mod = gob.GetComponent<MerlinMod>();
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
    }
}
