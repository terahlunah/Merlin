using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MerlinPatcher
{
    class MerlinPatcher
    {
        [STAThread]
        static void Main(string[] args)
        {
            if(args.Length == 2)
            {
                Console.WriteLine("#####################");
                Console.WriteLine("Target : " + args[0]);
                Console.WriteLine("Hook : " + args[1]);
                Console.WriteLine("#####################");
                Console.WriteLine("");
                Patch(args[0], args[1]);
            }
            else
            {
                OpenForm();
            }
        }

        static void OpenForm()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new PatcherForm());
        }

        public static bool Patch(string targetAssemblyPath, string hookAssemblyPath)
        {
            AssemblyDefinition targetAssembly = AssemblyDefinition.ReadAssembly(targetAssemblyPath);
            AssemblyDefinition hookAssembly = AssemblyDefinition.ReadAssembly(hookAssemblyPath);

            bool result = PatchAssembly(targetAssembly, hookAssembly);

            if (result)
            {
                var managedPath = Path.GetDirectoryName(targetAssemblyPath);

                targetAssembly.Write(targetAssemblyPath + ".patched");

                var merlinTarget = Path.Combine(managedPath + "/Merlin.dll");
                File.Copy(hookAssemblyPath, merlinTarget, true);
            }

            return result;
        }

        static bool PatchAssembly(AssemblyDefinition targetAssembly, AssemblyDefinition hookAssembly)
        {
            List<Hook> hooks = GetHookList(hookAssembly);

            foreach (var hook in hooks)
            {
                MethodDefinition target = FindTargetHook(targetAssembly, hook);
                if (target == null)
                {
                    Console.WriteLine("Could not locate " + hook.TargetFullName);
                    return false;
                }
                else
                {
                    hook.Host = target;
                    InstallHook(hook);
                }
            }

            return true;
        }

        static MethodDefinition FindTargetHook(AssemblyDefinition targetAssembly, Hook hook)
        {
            foreach (var module in targetAssembly.Modules)
            {
                foreach (var type in module.Types)
                {
                    if (!(type.Namespace == hook.Namespace && type.Name == hook.Class))
                        continue;

                    foreach (var method in type.Methods)
                    {
                        if (method.Name == hook.Method)
                            return method;
                    }
                }
            }

            return null;
        }

        static List<Hook> GetHookList(AssemblyDefinition hookAssembly)
        {
            List<Hook> hooks = new List<Hook>();

            foreach (var module in hookAssembly.Modules)
            {
                foreach (var type in module.Types)
                {
                    foreach (var method in type.Methods)
                    {
                        Hook hook = GetHookAttribute(method);
                        if (hook != null)
                            hooks.Add(hook);
                    }
                }
            }

            return hooks;
        }

        static Hook GetHookAttribute(MethodDefinition method)
        {
            foreach (var attribute in method.CustomAttributes)
            {
                if (attribute.AttributeType.Name == "Hook")
                {
                    return new Hook
                    {
                        Virus = method,
                        Type = attribute.ConstructorArguments[0].Value as string,
                        Namespace = attribute.ConstructorArguments[1].Value as string,
                        Class = attribute.ConstructorArguments[2].Value as string,
                        Method = attribute.ConstructorArguments[3].Value as string,
                    };
                }
            }

            return null;
        }

        static void InstallHook(Hook hook)
        {
            Console.WriteLine(hook.Host.FullName + " => " + hook.Virus.FullName);

            switch (hook.Type)
            {
                case "ForwardSelf":
                {
                    InstallForwardHook(hook);
                    break;
                }

                default:
                {
                    Console.WriteLine("Unknow hook type : " + hook.Type);
                    break;
                }
            }
        }

        static void InstallForwardHook(Hook hook)
        {
            var ilp = hook.Host.Body.GetILProcessor();
            var first = hook.Host.Body.Instructions[0];

            ilp.InsertBefore(first, ilp.Create(OpCodes.Ldarg_0));
            ilp.InsertBefore(first, ilp.Create(OpCodes.Call, hook.Host.Module.Import(hook.Virus)));
        }
    }
}
