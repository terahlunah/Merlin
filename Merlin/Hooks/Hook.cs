using System;

namespace Merlin.Hooks
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Hook : Attribute
    {
        public Hook(string Type, string Namespace, string Class, string Method)
        {
            this.Type = Type;
            this.Namespace = Namespace;
            this.Class = Class;
            this.Method = Method;
        }

        public string Type { get; set; }
        public string Namespace { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
    }
}
