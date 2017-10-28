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
            var backupPath = targetAssemblyPath + ".bak";
            if (File.Exists(backupPath))
            {
                File.Delete(targetAssemblyPath);
                File.Copy(backupPath, targetAssemblyPath);
            }
            else
            {
                File.Copy(targetAssemblyPath, backupPath);
            }

            AssemblyDefinition targetAssembly = AssemblyDefinition.ReadAssembly(targetAssemblyPath);
            AssemblyDefinition hookAssembly = AssemblyDefinition.ReadAssembly(hookAssemblyPath);

            bool result = PatchAssembly(targetAssembly, hookAssembly);

            if (result)
            {
                var managedPath = Path.GetDirectoryName(targetAssemblyPath);

                targetAssembly.Write(targetAssemblyPath + ".patched");
                File.Delete(targetAssemblyPath);
                File.Move(targetAssemblyPath + ".patched", targetAssemblyPath);

                var merlinTarget = Path.Combine(managedPath + "/Merlin.dll");
                File.Copy(hookAssemblyPath, merlinTarget, true);
            }

            return result;
        }

        static bool PatchAssembly(AssemblyDefinition targetAssembly, AssemblyDefinition hookAssembly)
        {
            List<HookData> hooks = GetHookList(hookAssembly);

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

        static MethodDefinition FindTargetHook(AssemblyDefinition targetAssembly, HookData hook)
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

        static List<HookData> GetHookList(AssemblyDefinition hookAssembly)
        {
            List<HookData> hooks = new List<HookData>();

            foreach (var module in hookAssembly.Modules)
            {
                foreach (var type in module.Types)
                {
                    foreach (var method in type.Methods)
                    {
                        HookData hook = GetHookAttribute(method);
                        if (hook != null)
                            hooks.Add(hook);
                    }
                }
            }

            return hooks;
        }

        static HookData GetHookAttribute(MethodDefinition method)
        {
            foreach (var attribute in method.CustomAttributes)
            {
                if (attribute.AttributeType.Name == "Hook")
                {
                    return new HookData
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

        static void InstallHook(HookData hook)
        {
            Console.WriteLine(hook.Host.FullName + " => " + hook.Virus.FullName);

            switch (hook.Type)
            {
                case "Before":
                {
                    InstallHookBefore(hook);
                    break;
                }

                case "After":
                {
                    InstallHookAfter(hook);
                    break;
                }

                default:
                {
                    Console.WriteLine("Unknow hook type : " + hook.Type);
                    break;
                }
            }
        }

        static void InstallHookBefore(HookData hook)
        {
            if (!CheckSignatureCompatibility(hook.Host, hook.Virus, false))
            {
                Console.WriteLine("Error : Method signature difference between Hook and Target");
                return;
            }

            if (hook.Virus.ReturnType.FullName != "System.Void")
            {
                Console.WriteLine("Error : Hook must return void");
                return;
            }
            
            foreach (var arg in hook.Host.Parameters)
            {
                Console.WriteLine(arg.Name + " : " + arg.ParameterType);
            }
            foreach (var arg in hook.Virus.Parameters)
            {
                Console.WriteLine(arg.Name + " : " + arg.ParameterType);
            }

            var ilp = hook.Host.Body.GetILProcessor();
            var first = hook.Host.Body.Instructions[0];

            if (hook.Host.HasThis && !hook.Host.ExplicitThis)
                ilp.InsertBefore(first, ilp.Create(OpCodes.Ldarg_0));

            foreach (var param in hook.Host.Parameters)
                ilp.InsertBefore(first, ilp.Create(OpCodes.Ldarg, param));

            ilp.InsertBefore(first, ilp.Create(OpCodes.Call, hook.Host.Module.Import(hook.Virus)));
        }

        static void InstallHookAfter(HookData hook)
        {
            if (!CheckSignatureCompatibility(hook.Host, hook.Virus, true))
            {
                Console.WriteLine("Error : Method signature difference between Hook and Target");
                return;
            }

            foreach (var arg in hook.Host.Parameters)
            {
                Console.WriteLine(arg.Name + " : " + arg.ParameterType);
            }
            foreach (var arg in hook.Virus.Parameters)
            {
                Console.WriteLine(arg.Name + " : " + arg.ParameterType);
            }

            bool hasReturnType = hook.Host.ReturnType.FullName != "System.Void";


            VariableDefinition returnVar = null;
            if (hasReturnType)
            {
                returnVar = new VariableDefinition("returnVar", hook.Host.ReturnType);
                hook.Host.Body.Variables.Add(returnVar);
            }
            

            var ilp = hook.Host.Body.GetILProcessor();
            var last = hook.Host.Body.Instructions[hook.Host.Body.Instructions.Count-1]; // Ret

            // Store original return value
            if (hasReturnType)
                ilp.InsertBefore(last, ilp.Create(OpCodes.Stloc, returnVar));

            // Add this parameter
            if (hook.Host.HasThis && !hook.Host.ExplicitThis)
                ilp.InsertBefore(last, ilp.Create(OpCodes.Ldarg_0));

            // Add parameters
            foreach (var param in hook.Host.Parameters)
                ilp.InsertBefore(last, ilp.Create(OpCodes.Ldarg, param));

            // Load original return value
            if (hasReturnType)
                ilp.InsertBefore(last, ilp.Create(OpCodes.Ldloc, returnVar));

            ilp.InsertBefore(last, ilp.Create(OpCodes.Call, hook.Host.Module.Import(hook.Virus)));
        }

        private static List<TypeReference> GetParametersTypeList(MethodDefinition method, bool withReturnType)
        {
            List<TypeReference> typeList = new List<TypeReference>();

            if (method.HasThis && !method.ExplicitThis)
                typeList.Add(method.DeclaringType);

            typeList.AddRange(method.Parameters.AsEnumerable().Select(param => param.ParameterType));

            if (withReturnType)
                typeList.Add(method.ReturnType);

            return typeList;
        }

        private static bool CheckSignatureCompatibility(MethodDefinition a, MethodDefinition b, bool withReturnType = false)
        {
            var typeListA = GetParametersTypeList(a, withReturnType);
            var typeListB = GetParametersTypeList(b, withReturnType);
            
            if (!withReturnType && typeListA.Count != typeListB.Count)
                return false;

            if (withReturnType && a.ReturnType.FullName != "System.Void" && typeListA.Count + 1 != typeListB.Count)
                return false;

            for (var i = 0; i < typeListA.Count; ++i)
            {
                if (typeListA[i].FullName != typeListB[i].FullName)
                    return false;
            }

            if (withReturnType && a.ReturnType.FullName != "System.Void")
                if (a.ReturnType.FullName != typeListB[typeListB.Count - 1].FullName)
                    return false;

            return true;
        }
    }
}
