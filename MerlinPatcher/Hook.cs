using Mono.Cecil;

namespace MerlinPatcher
{
    class HookData
    {
        public MethodDefinition Host { get; set; }
        public MethodDefinition Virus { get; set; }

        public string Type { get; set; }
        public string Namespace { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }

        public string TargetFullName => Namespace + "." + Class + "." + Method;
    }
}
